namespace RaquelShoesAPI.Domain
{
    public class Zapato
    {
        public int Id { get; set; }
        public string Color { get; set; } = string.Empty;
        public int Talla { get; set; }
        public string Sexo { get; set; } = string.Empty; // "Damas" or "Caballeros"
        public int TiendaId { get; set; }
        public int VendedorId { get; set; }
        public int ProveedorId { get; set; }
        public int CantidadDisponible { get; set; }
        public string Marca { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        
        // Navigation properties:
        public Tienda Tienda { get; set; } = null!;  
        public Vendedor Vendedor { get; set; } = null!;
        public Proveedor Proveedor { get; set; } = null!;
    }
}
