using SegurosAPI.DTOs;

namespace SegurosAPI.Services.Contrato
{
    public interface ISegurosClientes
    {
        Task<List<SegurosClienteDTO>> GetSegurosClientesList();
        Task<SegurosClienteDTO> GetSegurosCliente(int idSegurosCliente);
        Task<SegurosClienteDTO> AddSegurosCliente(SegurosClienteDTO modelo);
        Task<bool> UpdateSegurosCliente(SegurosClienteDTO modelo, int id);
        Task<bool> DeleteSegurosCliente(int id);
    }
}
