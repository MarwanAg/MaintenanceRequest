using Domain.Enums;

namespace Application.Services.MaintenanceRequestService.DTOs
{
    public class GetAllRequestsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public RequestStatus Status { get; set; } = RequestStatus.New;
        public DateTime CreatedAt { get; set; }

        public string EmployeeName { get; set; }
        public string? TechnicianName { get; set; }
        public string CategoryName { get; set; }
    }
}
