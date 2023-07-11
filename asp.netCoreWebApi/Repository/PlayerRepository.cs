using asp.netCoreWebApi.Data;
using asp.netCoreWebApi.models;
using asp.netCoreWebApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Numerics;

namespace asp.netCoreWebApi.Repository
{
    public class PlayerRepository : Repository<Player>, IPlayerRepository
    {

        // constructor
        private readonly ApplicationDbContext _db;
        public PlayerRepository(ApplicationDbContext db)
            :base(db)
        {
            _db = db;
        }

        // Update
        public async Task<Player> UpdateAsync(Player entity)
        {
            _db.Players.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
