using Microsoft.EntityFrameworkCore;
using UserCrud.Models;

namespace UserCrud.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> tbl_user { get; set; }
    }
}
