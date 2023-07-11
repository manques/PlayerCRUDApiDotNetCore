using asp.netCoreWebApi.Data;
using asp.netCoreWebApi.models;
using asp.netCoreWebApi.Repository.IRepository;
using System.Linq.Expressions;

namespace asp.netCoreWebApi.Repository
{
    public class PlayerNumberRepository : Repository<PlayerNumber>, IPlayerNumberRepository
    {
        // constructor

        private readonly ApplicationDbContext _db;
        public PlayerNumberRepository(ApplicationDbContext db)
            :base(db)
        {
            _db = db;
        }


        // update
        public async Task<PlayerNumber> UpdateAsync(PlayerNumber entity)
        {
            entity.UpdatedAt = DateTime.Now;
            _db.PlayerNumbers.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
