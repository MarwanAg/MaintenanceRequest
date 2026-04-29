namespace Application.Services.CurrentUserService
{
    public interface ICurrentUserService
    {
        Guid? UserId { get; }
        string? Name { get; }
        string? Email { get; }
        string? Phone { get; }
        string? Role { get; }
    }
}
