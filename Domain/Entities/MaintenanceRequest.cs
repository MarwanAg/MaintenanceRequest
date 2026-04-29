using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class MaintenanceRequest
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.New;

        public Guid EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public User Employee { get; set; }
        public Guid? TechnicianId { get; set; }
        [ForeignKey("TechnicianId")]
        public User Technician { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual RequestDetail RequestDetail { get; set; }
    }
}
