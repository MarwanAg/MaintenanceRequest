namespace Application.Services.MaintenanceRequestService.DTOs
{
    public class CreateRequestDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Guid EmployeeId { get; set; }
        public Guid CategoryId { get; set; }
        public RequestDetailDto RequestDetail { get; set; }
    }
}
