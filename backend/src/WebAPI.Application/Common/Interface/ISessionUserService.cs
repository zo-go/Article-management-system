namespace WebAPI.Application.Common.Interface
{
    public interface ISessionUserService
    {
        Guid? UserId { get; }
        string? Username { get; }
    }
}