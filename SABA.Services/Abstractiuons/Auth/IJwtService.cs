namespace SABA.Services.Abstractiuons.Auth
{
    public interface IJwtService
    {
        string GenerateToken(string userId, string role);
    }
}
