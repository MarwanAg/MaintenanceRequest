namespace Application.Services.MaintenanceRequestService.DTOs
{
    public class UpdateRequestDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Guid CategoryId { get; set; }
    }
}
