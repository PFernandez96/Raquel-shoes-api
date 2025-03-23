namespace RaquelShoesAPI.Domain
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Sexo { get; set; } = string.Empty;
        public string Nacionalidad { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string DireccionHabitacion { get; set; } = string.Empty;
        public bool Status { get; set; }
        public ICollection<Zapato> Zapatos { get; set; } = new List<Zapato>();
    }
}
