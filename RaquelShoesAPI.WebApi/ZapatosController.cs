using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RaquelShoesAPI.Domain;
using System.Collections.Generic;

namespace RaquelShoesAPI.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  // This attribute protects all endpoints in this controller.
    public class ZapatosController : ControllerBase
    {
        // In a real-world scenario, we'd inject a service or repository via dependency injection.
        // For this example, we'll use a hardcoded list for demonstration purposes.
        
        [HttpGet]
        public IActionResult GetAll()
        {
            // Dummy data for demonstration
            var zapatos = new List<Zapato>
            {
                new Zapato 
                {
                    Id = 1,
                    Color = "Black",
                    Talla = 42,
                    Sexo = "Caballeros",
                    TiendaId = 1,
                    VendedorId = 1,
                    ProveedorId = 1,
                    CantidadDisponible = 10,
                    Marca = "BrandX",
                    Precio = 99.99m,
                    // Navigation properties could be left null or initialized as needed
                    Tienda = new Tienda { Id = 1, Nombre = "Main Store", Direccion = "123 Main St" },
                    Vendedor = new Vendedor { Id = 1, Nombre = "John", Apellidos = "Doe", Sexo = "Caballeros", Nacionalidad = "USA", Telefono = "555-1234", DireccionHabitacion = "456 Side St", Status = true },
                    Proveedor = new Proveedor { Id = 1, Nombre = "SupplierOne", Telefono = "555-5678", Direccion = "789 Supplier Rd", TipoZapato = "Caballeros" }
                }
            };

            return Ok(zapatos);
        }

        // Add other endpoints (POST, PUT, DELETE) as needed...
    }
}
