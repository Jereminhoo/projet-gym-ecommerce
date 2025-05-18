using System.Data;

namespace Projet_salle_de_gym.Core.Infrastructure
{

    public interface IDbConnectionProvider
    {
        Task<IDbConnection> CreateConnection();
    }


}
