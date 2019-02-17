namespace ModDownloads.Server.Controllers
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);

        bool ValidateToken(string token);
    }

}