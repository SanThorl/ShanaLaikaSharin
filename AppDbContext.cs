using Microsoft.EntityFrameworkCore;
using Webmvc.Models;

namespace Webmvc;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CustomerModel> Customers { get; set; }
}
