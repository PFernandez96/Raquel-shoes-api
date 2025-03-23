namespace RaquelShoesAPI.Domain
{
    public class Tienda
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public ICollection<Zapato> Zapatos { get; set; } = new List<Zapato>();
    }
}
