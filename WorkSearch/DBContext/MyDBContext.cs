using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkSearch.Models;

namespace WorkSearch.DBContext
{
    public class MyDBContext : IdentityDbContext<User, IdentityRole<long>, long>
    {
        public DbSet<Citizenship> Citizenships { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Gender> Genders { get; set; }

        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options) { }
    }
}
