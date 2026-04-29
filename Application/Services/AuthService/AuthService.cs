using Application.Repositories;
using Application.Services.AuthService.DTOs;
using Application.Services.CurrentUserService;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Application.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IGenericRepository<User> _userRepository;
        private readonly IGenericRepository<Token> _refershTokenRepository;
        private readonly IConfiguration _configuration;
        private readonly ICurrentUserService _currentUserService;
        public AuthService(IGenericRepository<User> userRepository, IConfiguration configuration, IGenericRepository<Token> refershTokenRepository, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _refershTokenRepository = refershTokenRepository;
            _currentUserService = currentUserService;
        }

        public async Task ChangeUserPassword(ChangeUserPasswordDto input)
        {
            var userId = _currentUserService.UserId;
            var user = await _userRepository.GetByIdAsync(userId.Value);

            var passwordHasher = new PasswordHasher<User>();
            var passwordStatus = passwordHasher.VerifyHashedPassword(user, user.Password, input.CurrentPassword);

            if (passwordStatus == PasswordVerificationResult.Failed)
            {
                throw new Exception("Current password invalid");
            }

            if (input.NewPassword != input.ConfirmNewPassword)
            {
                throw new Exception("Confirm password not matches");
            }


            user.Password = passwordHasher.HashPassword(user, input.NewPassword);

            await _userRepository.UpdateAsync(user);
            await _userRepository.SaveChangesAsync();

        }

        public async Task<LoginResponseDto> Login(LoginRequestDto input)
        {
            var user = await _userRepository.GetAll()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == input.Username.ToLower().Trim() || x.Phone == input.Username.Trim() || x.Username == input.Username.Trim());

            if (user == null)
            {
                throw new Exception("Username or password invalid");
            }

            var passwordHasher = new PasswordHasher<User>();
            var passwordStatus = passwordHasher.VerifyHashedPassword(user, user.Password, input.Password);

            if (passwordStatus == PasswordVerificationResult.Failed)
            {
                throw new Exception("Username or password invalid");
            }

            var accessToken = await GenerateAccessToken(user);
            var refershToken = GenerateRefreshToken();

            await _refershTokenRepository.InsertAsync(new Token
            {
                UserId = user.Id,
                TokenStr = refershToken,
                ExpiryDate = DateTime.UtcNow.AddDays(7),
            });

            await _refershTokenRepository.SaveChangesAsync();

            var result = new LoginResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Phone = user.Phone,
                RoleName = user.Role.Name,
                RoleCode = user.Role.Code,
                AccessToken = accessToken,
                RefershToken = refershToken
            };

            return result;

        }

        public async Task LogoutAsync(string? refreshToken = null)
        {
            var userId = _currentUserService.UserId;
            if (userId == null) return;

            var query = _refershTokenRepository.GetAll().Where(x => x.UserId == userId.Value);

            if (!string.IsNullOrEmpty(refreshToken))
            {
                query = query.Where(x => x.TokenStr == refreshToken);
            }

            var tokens = await query.ToListAsync();

            foreach (var token in tokens)
            {
                _refershTokenRepository.Delete(token);
            }
            await _refershTokenRepository.SaveChangesAsync();
        }

        public async Task<string> RefreshToken(RefreshTokenDto input)
        {
            var refershToken = await _refershTokenRepository.GetAll().FirstOrDefaultAsync(x => x.TokenStr == input.Token && x.UserId == _currentUserService.UserId.Value && x.ExpiryDate > DateTime.UtcNow);

            if (refershToken != null)
            {
                var user = await _userRepository.GetAll().Include(x => x.Role).FirstOrDefaultAsync(x => x.Id == _currentUserService.UserId.Value);
                var accessToken = await GenerateAccessToken(user);


                refershToken.TokenStr = GenerateRefreshToken();
                refershToken.ExpiryDate = DateTime.UtcNow.AddDays(7);

                await _refershTokenRepository.UpdateAsync(refershToken);
                await _refershTokenRepository.SaveChangesAsync();

                return accessToken;
            }

            return null;
        }

        private async Task<string> GenerateAccessToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.MobilePhone, user.Phone),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = jwtSection["Issuer"],
                Audience = jwtSection["Audience"],
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            };

            var handler = new JwtSecurityTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);

        }

        private string GenerateRefreshToken()
        {
            var random = new byte[64];
            RandomNumberGenerator.Fill(random);
            return Convert.ToBase64String(random);
        }
    }
}
