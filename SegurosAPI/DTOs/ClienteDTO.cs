namespace SegurosAPI.DTOs
{
    public class ClienteDTO
    {
        public int id;
        public string Cedula { get; set; } = null!;
        public string NombreCliente { get; set; } = null!;
        public string Telefono { get; set; } = null!;
        public int Edad { get; set; }
    }
}
