using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class TechnicianCategory
    {
        public Guid Id { get; set; }
        public Guid TechnicianId { get; set; }
        [ForeignKey("TechnicianId")]
        public User Technician { get; set; }

        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
    }
}
