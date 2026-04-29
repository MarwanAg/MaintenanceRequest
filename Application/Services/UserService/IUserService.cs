using Application.Services.UserService.DTOs;

namespace Application.Services.UserService
{
    public interface IUserService
    {
        Task<List<GetAllUsersDto>> GetAllUsers(string? username, string? email);
        Task<GetUserByIdDto> GetUserById(Guid Id);
        Task CreateUser(CreateUserDto input);
        Task UpdateUser(Guid Id ,UpdateUserDto input);
        Task DeleteUser(Guid Id);
    }
}
