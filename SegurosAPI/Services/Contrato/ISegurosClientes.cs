using SegurosAPI.DTOs;

namespace SegurosAPI.Services.Contrato
{
    public interface ISegurosClientes
    {
        Task<List<SegurosClienteDTO>> GetSegurosClientesList();
        Task<SegurosClienteDTO?> GetSegurosCliente(int idSegurosCliente);
        Task<SegurosClienteDTO> AddSegurosCliente(CrearAseguradoDTO modelo);
        Task<bool> UpdateSegurosCliente(CrearAseguradoDTO modelo, int id);
        Task<bool> DeleteSegurosCliente(int id);
    }
}
