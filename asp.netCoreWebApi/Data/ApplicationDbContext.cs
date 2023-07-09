using asp.netCoreWebApi.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace asp.netCoreWebApi.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        public DbSet<Player> Players { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().HasData(
                new Player()
                {
                    Id = 1,
                    Name = "anup",
                    CreatedAt = DateTime.Now
                },
                new Player()
                {
                    Id = 2,
                    Name = "vivek",
                    CreatedAt = DateTime.Now
                },
                new Player()
                {
                    Id = 3,
                    Name = "abhishek",
                    CreatedAt = DateTime.Now
                },
                new Player()
                {
                    Id = 4,
                    Name = "sushil",
                    CreatedAt = DateTime.Now
                }
            );
        }
    }
}
