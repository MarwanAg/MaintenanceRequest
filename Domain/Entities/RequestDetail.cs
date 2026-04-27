using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Index(nameof(RequestId), IsUnique = true)]

    public class RequestDetail
    {
        public Guid Id { get; set; }
        public string Location { get; set; }
        public string EmployeeNotes { get; set; }
        public string TechnicianNotes { get; set; }
        public string? ImageURL { get; set; }

        public Guid RequestId { get; set; }
        [ForeignKey("RequestId")]
        public MaintenanceRequest Request { get; set; }
    }
}
