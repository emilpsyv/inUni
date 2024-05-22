using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpacDyna.Models;

namespace SpacDyna.DAL
{
    public class SpacContext : IdentityDbContext
    {
        public SpacContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet <Card> Cards { get; set; }
    }
}
