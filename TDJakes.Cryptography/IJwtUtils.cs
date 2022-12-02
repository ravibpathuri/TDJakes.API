using TDJakes.Models.ViewModel;

namespace TDJakes.Cryptography;
public interface IJwtUtils
{
    public string GenerateJwtToken(UserToken user);
    public string ValidateJwtToken(string token);
    public RefreshToken GenerateRefreshToken(string ipAddress);
}
