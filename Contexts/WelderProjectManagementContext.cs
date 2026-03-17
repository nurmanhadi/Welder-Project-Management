using Microsoft.EntityFrameworkCore;
using WelderProjectManagement.Models;

namespace WelderProjectManagement.Contexts;

public class WelderProjectManagementContext : DbContext
{
    public WelderProjectManagementContext(DbContextOptions<WelderProjectManagementContext> options) : base(options)
    {
    }

    public DbSet<Client> Clients { get; set; } = null!;
    public DbSet<Project> Projects { get; set; } = null!;
    public DbSet<ProjectItem> ProjectItems { get; set; } = null!;
    public DbSet<Payment> Payments { get; set; } = null!;
    public DbSet<Invoice> Invoices { get; set; } = null!;
}
