using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using RaquelShoesAPI.Domain;
using RaquelShoesAPI.Infrastructure.Data;
using Xunit;

namespace RaquelShoesAPI.Tests
{
    public class ApplicationDbContextTests
    {
        // Creates a new in-memory database context for each test
        private ApplicationDbContext GetInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Ensures a unique DB for each test
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public void CanInsertAndRetrieveZapato()
        {
            // Arrange: Create the context and add related entities
            using var context = GetInMemoryContext();

            var tienda = new Tienda 
            { 
                Nombre = "Test Store", 
                Direccion = "123 Main St" 
            };
            var vendedor = new Vendedor 
            { 
                Nombre = "John", 
                Apellidos = "Doe", 
                Sexo = "Caballeros", 
                Nacionalidad = "Test", 
                Telefono = "555-1234", 
                DireccionHabitacion = "Test Street", 
                Status = true 
            };
            var proveedor = new Proveedor 
            { 
                Nombre = "Test Supplier", 
                Telefono = "555-5678", 
                Direccion = "Supplier St", 
                TipoZapato = "Caballeros" 
            };

            context.Tiendas.Add(tienda);
            context.Vendedores.Add(vendedor);
            context.Proveedores.Add(proveedor);
            context.SaveChanges();

            var zapato = new Zapato
            {
                Color = "Black",
                Talla = 42,
                Sexo = "Caballeros",
                TiendaId = tienda.Id,
                VendedorId = vendedor.Id,
                ProveedorId = proveedor.Id,
                CantidadDisponible = 10,
                Marca = "TestBrand",
                Precio = 99.99m
            };

            // Act: Add the Zapato and save changes
            context.Zapatos.Add(zapato);
            context.SaveChanges();

            // Retrieve the zapato back from the database
            var retrievedZapato = context.Zapatos.FirstOrDefault(z => z.Id == zapato.Id);

            // Assert: Verify the inserted record
            Assert.NotNull(retrievedZapato);
            Assert.Equal("Black", retrievedZapato.Color);
            Assert.Equal(42, retrievedZapato.Talla);
            Assert.Equal("Caballeros", retrievedZapato.Sexo);
            Assert.Equal(tienda.Id, retrievedZapato.TiendaId);
            Assert.Equal(vendedor.Id, retrievedZapato.VendedorId);
            Assert.Equal(proveedor.Id, retrievedZapato.ProveedorId);
            Assert.Equal(10, retrievedZapato.CantidadDisponible);
            Assert.Equal("TestBrand", retrievedZapato.Marca);
            Assert.Equal(99.99m, retrievedZapato.Precio);
        }

        [Fact]
        public void CanRetrieveNavigationPropertiesForZapato()
        {
            // Arrange: Create the context and add related entities
            using var context = GetInMemoryContext();

            var tienda = new Tienda 
            { 
                Nombre = "Test Store", 
                Direccion = "123 Main St" 
            };
            var vendedor = new Vendedor 
            { 
                Nombre = "Jane", 
                Apellidos = "Doe", 
                Sexo = "Damas", 
                Nacionalidad = "Test", 
                Telefono = "555-0000", 
                DireccionHabitacion = "Another St", 
                Status = true 
            };
            var proveedor = new Proveedor 
            { 
                Nombre = "Test Supplier", 
                Telefono = "555-5678", 
                Direccion = "Supplier St", 
                TipoZapato = "Damas" 
            };

            context.Tiendas.Add(tienda);
            context.Vendedores.Add(vendedor);
            context.Proveedores.Add(proveedor);
            context.SaveChanges();

            var zapato = new Zapato
            {
                Color = "Red",
                Talla = 38,
                Sexo = "Damas",
                TiendaId = tienda.Id,
                VendedorId = vendedor.Id,
                ProveedorId = proveedor.Id,
                CantidadDisponible = 5,
                Marca = "AnotherBrand",
                Precio = 59.99m
            };

            context.Zapatos.Add(zapato);
            context.SaveChanges();

            // Act: Use Include to load the navigation properties
            var retrievedZapato = context.Zapatos
                .Include(z => z.Tienda)
                .Include(z => z.Vendedor)
                .Include(z => z.Proveedor)
                .FirstOrDefault(z => z.Id == zapato.Id);

            // Assert: Verify that navigation properties are loaded correctly
            Assert.NotNull(retrievedZapato);
            Assert.NotNull(retrievedZapato.Tienda);
            Assert.Equal("Test Store", retrievedZapato.Tienda.Nombre);
            Assert.NotNull(retrievedZapato.Vendedor);
            Assert.Equal("Jane", retrievedZapato.Vendedor.Nombre);
            Assert.NotNull(retrievedZapato.Proveedor);
            Assert.Equal("Test Supplier", retrievedZapato.Proveedor.Nombre);
        }
    }
}
