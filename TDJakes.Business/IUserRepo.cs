using TDJakes.Models.ViewModel;

namespace TDJakes.Business;

public interface IUserRepo
{
    Task<IEnumerable<UserProfile>> GetUsers();
    Task<IEnumerable<UserProfile>> GetUserByEmail(string userEmail);
}

