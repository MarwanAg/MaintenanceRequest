using Domain.Enums;

namespace Application.Services.RoleService.DTOs
{
    public class GetRolesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
