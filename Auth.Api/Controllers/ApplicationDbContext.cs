using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Auth.Api.Controllers
{
    // dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .Property(p => p.Account)
                .HasMaxLength(20);

            base.OnModelCreating(builder);
        }


    }
}
