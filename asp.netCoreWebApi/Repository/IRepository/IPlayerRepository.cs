using asp.netCoreWebApi.models;
using System.Linq.Expressions;

namespace asp.netCoreWebApi.Repository.IRepository
{
    public interface IPlayerRepository : IRepository<Player>
    {

        Task<Player> UpdateAsync(Player entity);
    }
}
