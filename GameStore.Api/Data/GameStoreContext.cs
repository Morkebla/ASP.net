using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;
namespace GameStore.Api.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
{
    public DbSet<Game> games => Set<Game>();
    public DbSet<Genre> genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
            new {id =1, name = "Fighting"},
            new {id =2, name = "RPG"},
            new {id =3, name = "Sports"},
            new {id =4, name = "Racing"},
            new {id =5, name = "Kids and Family"}
        );
    }
}