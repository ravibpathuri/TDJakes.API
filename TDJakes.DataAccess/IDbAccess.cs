using System.Data;

namespace TDJakes.DataAccess;

    public interface IDbAccess
    {
        Task<IEnumerable<T>> Fetch<T, U>(string storedProcedure, U parameters,
           CommandType commandType = CommandType.Text, string connectionId = "Default");
        Task SavaData<T>(string storedProcedure, T parameters,
            CommandType commandType = CommandType.Text, string connectionId = "Default");
    }

