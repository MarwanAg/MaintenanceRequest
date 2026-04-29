namespace Application.Services.CategoryService.DTOs
{
    public class GetCategoriesDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
