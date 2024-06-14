using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Webmvc.Models;

namespace Webmvc;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<CustomerModel> Customers { get; set; }
    public DbSet<UserModel> Users { get; set; }
    public DbSet<LoginModel> Login { get; set; }
}

[Table("Tbl_Login")]
public class LoginModel
{
    [Key]
    public int Id { get; set; }
    public string UserId { get; set; }
    public string SessionId { get; set; }
    public DateTime SessionExpired { get; set; }
}

[Table("Tbl_User")]
public class UserModel
{
    [Key]
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string Role { get; set; }
}
