namespace Application.Services.TechnicianService.DTOs
{
    public class AssignCategoryToTechnicianDto
    {
        public Guid Id { get; set; }
        public Guid TechnicianId { get; set; }
        public Guid CategoryId { get; set; }
    }
}
