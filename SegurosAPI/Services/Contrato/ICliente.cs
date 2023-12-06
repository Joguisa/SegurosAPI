using SegurosAPI.DTOs;
using SegurosAPI.Models;

namespace SegurosAPI.Services.Contrato
{
    public interface ICliente
    {
        Task<List<ClienteDTO>> GetList();
        Task<ClienteDTO> Get(int idCliente);
        Task<ClienteDTO> Add(ClienteDTO modelo);
        Task<bool> Update(ClienteDTO modelo, int id);
        Task<bool> Delete(int id);
    }
}
