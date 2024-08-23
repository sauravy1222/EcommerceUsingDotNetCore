using EcommerceUsingDotNetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceUsingDotNetCore.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) {

        }
        public DbSet<User> users { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Cart> carts { get; set; }

    }
}
