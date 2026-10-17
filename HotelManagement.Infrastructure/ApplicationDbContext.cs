using Microsoft.EntityFrameworkCore;
using HotelManagement.Domain.Entities;
using HotelManagement.Domain.StoreProcedureResponse;

namespace HotelManagement.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Company> Companies { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Role> Role { get; set; }
    public DbSet<Users> Users { get; set; }
    public DbSet<Forms> Forms { get; set; }
    public DbSet<FormBusinessRules> FormBusinessRules { get; set; }
    public DbSet<FormRulesPermission> FormRulesPermission { get; set; }
    public DbSet<UsersInRole> UsersInRole { get; set; }
    public DbSet<SP_GetFormBreadCum> SP_GetFormBreadCum { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Ignore<Common>();
        modelBuilder.Entity<SP_GetFormBreadCum>().HasNoKey().ToView(null);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}

