using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVCIdentity.Areas.Identity.Data;
using MVCIdentity.Models;

namespace MVCIdentity.Data;

public class MVCIdentityContext : IdentityDbContext<MVCIdentityUser>
{
    public MVCIdentityContext(DbContextOptions<MVCIdentityContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }

public DbSet<MVCIdentity.Models.Ders> Ders { get; set; } = default!;
public DbSet<OgrenciDers> OgrenciDersler { get; set; } = default!;

}
