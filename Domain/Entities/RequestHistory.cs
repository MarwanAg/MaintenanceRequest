using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RequestHistory
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid RequestId { get; set; }
        [ForeignKey("RequestId")]
        public MaintenanceRequest Request { get; set; }

        public RequestStatus? OldStatus { get; set; }
        public RequestStatus NewStatus { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
