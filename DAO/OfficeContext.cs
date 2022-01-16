namespace ShipVista.DAO;
using ShipVista.Models;
using Microsoft.EntityFrameworkCore;
public class OfficeContext : DbContext{
    
    public DbSet<Plant> plants {get; set;}

    public DbSet<User> users {get; set;}

    public OfficeContext(DbContextOptions<OfficeContext> options) : base(options)
    {
    }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//             => optionsBuilder.UseNpgsql("Host=my_host;Database=my_db;Username=my_user;Password=}" + Constants.DB_PASSWORD);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Plant>().ToTable("plants");
            modelBuilder.Entity<User>().ToTable("users");
        }
}