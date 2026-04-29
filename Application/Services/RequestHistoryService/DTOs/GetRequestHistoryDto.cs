using Domain.Entities;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Services.RequestHistoryService.DTOs
{
    public class GetRequestHistoryDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RequestId { get; set; }
        public RequestStatus? OldStatus { get; set; }
        public RequestStatus NewStatus { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
