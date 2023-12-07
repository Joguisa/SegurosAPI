using SegurosAPI.Models;

namespace SegurosAPI.DTOs
{
    public class SegurosClienteDTO
    {
        public int Id { get; set; }
        public ClienteDTO? Cliente { get; set; } = null!;
        public SeguroDTO? Seguro { get; set; } = null!;
    }
}
