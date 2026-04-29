using Domain.Enums;

namespace Application.Services.MaintenanceRequestService.DTOs
{
    public class GetRequestByIdDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.New;
        public DateTime CreatedAt { get; set; }

        public Guid EmployeeId { get; set; }
        public Guid? TechnicianId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
