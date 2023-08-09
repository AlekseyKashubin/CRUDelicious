#pragma warning disable CS8618

using Microsoft.EntityFrameworkCore;
namespace CRUDelicious.Models;
public class MyContext : DbContext 
{   
    public MyContext(DbContextOptions options) : base(options) { }    
    
    public DbSet<Dish> Dishes { get; set; } 


// sets a defult val 
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     modelBuilder.Entity<Dish>()
    //         .Property(b => b.Description)
    //         .HasDefaultValue("");
    // }
}
