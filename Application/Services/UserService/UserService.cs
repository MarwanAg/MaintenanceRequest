using Application.Repositories;
using Application.Services.UserService.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<User> _genericRepository;
        public UserService(IGenericRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task CreateUser(CreateUserDto input)
        {
            if (await _genericRepository.GetAll().AnyAsync(x => x.Email == input.Email.ToLower().Trim()))
            {
                throw new Exception("This Email Already Exist!");
            }
            if (await _genericRepository.GetAll().AnyAsync(x => x.Username == input.Username.ToLower().Trim()))
            {
                throw new Exception("This Username Already Exist!");
            }
            if (await _genericRepository.GetAll().AnyAsync(x => x.Phone == input.Phone.Trim()))
            {
                throw new Exception("This Phone Number is Used!");
            }

            var data = new User
            {
                Username = input.Username,
                Email = input.Email,
                Phone = input.Phone,
                RoleId = input.RoleId
            };

            var passwordHasher = new PasswordHasher<User>();
            data.Password = passwordHasher.HashPassword(data, input.Password);

            await _genericRepository.InsertAsync(data);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task DeleteUser(Guid Id)
        {
            var data = await _genericRepository.GetByIdAsync(Id);
            _genericRepository.Delete(data);
            await _genericRepository.SaveChangesAsync();
        }

        public async Task<List<GetAllUsersDto>> GetAllUsers(string? username, string? email)
        {
            username = !string.IsNullOrEmpty(username) ? username.ToLower().Trim() : null;
            email = !string.IsNullOrEmpty(email) ? email.ToLower().Trim() : null;

            var data = _genericRepository.GetAll();

            if (username != null)
            {
                data = data.Where(x => x.Username.ToLower().Trim().Contains(username));
            }
            if (email != null)
            {
                data = data.Where(x => x.Email.ToLower().Trim().Contains(email));
            }

            data = data.Include(x => x.Role);

            var result = data.Select(data => new GetAllUsersDto
            {
                Id = data.Id,
                Username = data.Username,
                Email = data.Email,
                Phone = data.Phone,
                RoleName = data.Role.Name
            }).ToList();

            return result;
        }

        public async Task<GetUserByIdDto> GetUserById(Guid Id)
        {
            var data = await _genericRepository.GetByIdAsync(Id);
            var result = new GetUserByIdDto
            {
                Id = data.Id,
                Username = data.Username,
                Email = data.Email,
                Phone = data.Phone,
                RoleId = data.RoleId
            };
            return result;
        }

        public async Task UpdateUser(Guid Id, UpdateUserDto input)
        {
            if (await _genericRepository.GetAll().AnyAsync(x => x.Email == input.Email.ToLower().Trim() && x.Id != Id))
            {
                throw new Exception("This Email Already Exist!");
            }
            if (await _genericRepository.GetAll().AnyAsync(x => x.Phone == input.Phone.Trim() && x.Id != Id))
            {
                throw new Exception("This Phone Number is Used!");
            }

            var data = await _genericRepository.GetByIdAsync(Id);

            data.Username = input.Username;
            data.Email = input.Email;
            data.Phone = input.Phone;
            data.RoleId = input.RoleId;

            await _genericRepository.UpdateAsync(data);
            await _genericRepository.SaveChangesAsync();
        }
    }
}
