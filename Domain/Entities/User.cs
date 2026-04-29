using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string? Location { get; set; }
        public Guid RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Role Role { get; set; }

        public virtual ICollection<TechnicianCategory> TechnicianCategories { get; set; } = new List<TechnicianCategory>();

        public virtual ICollection<MaintenanceRequest> CreatedRequests { get; set; } = new List<MaintenanceRequest>();
        public virtual ICollection<MaintenanceRequest> AssignedTasks { get; set; } = new List<MaintenanceRequest>();
    }
}
