using Microsoft.AspNetCore.Http;

namespace Application.Services.MaintenanceRequestService.DTOs
{
    public class RequestDetailDto
    {
        public string Location { get; set; }
        public string EmployeeNotes { get; set; }
        public IFormFile? ImageURL { get; set; }
    }
}
