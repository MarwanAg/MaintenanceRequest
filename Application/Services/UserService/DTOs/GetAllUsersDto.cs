using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace Application.Services.UserService.DTOs
{
    public class GetAllUsersDto
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string RoleName { get; set; }
    }
}
