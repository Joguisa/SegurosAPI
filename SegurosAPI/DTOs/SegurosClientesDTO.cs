using SegurosAPI.Models;

namespace SegurosAPI.DTOs
{
    public class SegurosClienteDTO
    {
        public int Id { get; set; }
        public List<ClienteDTO> Clientes { get; set; } = null!;
        public List<SeguroDTO> Seguros { get; set; } = null!;
    }
}
