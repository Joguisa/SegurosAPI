using Microsoft.EntityFrameworkCore;
using SegurosAPI.DTOs;
using SegurosAPI.Models;
using SegurosAPI.Services.Contrato;
using System.Linq;
namespace SegurosAPI.Services.Implementacion
{
    public class SegurosClienteServicio : ISegurosClientes
    {
        private readonly DBSegurosContext context;

        public SegurosClienteServicio(DBSegurosContext context)
        {
            this.context = context;
        }

        public async Task<List<SegurosClienteDTO>> GetSegurosClientesList()
        {
            var segurosClientes = await context.SegurosClientes
                .Include(sc => sc.Cliente)
                .Include(sc => sc.Seguro)
                .ToListAsync();

            var segurosClienteDTOs = segurosClientes.Select(sc => new SegurosClienteDTO
            {
                Id = sc.Id,
                //Clientes = sc.Cliente.Select(c => new ClienteDTO { id = c.id }).ToList(),
                //Seguros = sc.Seguro.Select(s => new SeguroDTO { id = s.id }).ToList()
            }).ToList();

            return segurosClienteDTOs;
        }


        public Task<SegurosClienteDTO> GetSegurosCliente(int idSegurosCliente)
        {
            throw new NotImplementedException();
        }

        public Task<SegurosClienteDTO> AddSegurosCliente(SegurosClienteDTO modelo)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteSegurosCliente(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateSegurosCliente(SegurosClienteDTO modelo, int id)
        {
            throw new NotImplementedException();
        }
    }
}
