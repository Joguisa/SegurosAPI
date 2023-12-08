namespace SegurosAPI.DTOs
{
    public class ClienteDto
    {
        internal int Id;
        public string Cedula { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int Edad { get; set; }
    }
}
