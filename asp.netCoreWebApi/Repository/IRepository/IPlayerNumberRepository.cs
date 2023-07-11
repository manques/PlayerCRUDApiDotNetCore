using asp.netCoreWebApi.models;

namespace asp.netCoreWebApi.Repository.IRepository
{
    public interface IPlayerNumberRepository : IRepository<PlayerNumber>
    {
        Task<PlayerNumber> UpdateAsync(PlayerNumber entity);
    }
}
