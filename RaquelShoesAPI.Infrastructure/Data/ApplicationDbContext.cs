using Microsoft.EntityFrameworkCore;
using RaquelShoesAPI.Domain;  // Adjust namespace based on your Domain project structure

namespace RaquelShoesAPI.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities:
        public DbSet<Zapato> Zapatos { get; set; }
        public DbSet<Tienda> Tiendas { get; set; }
        public DbSet<Vendedor> Vendedores { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
    }
}
