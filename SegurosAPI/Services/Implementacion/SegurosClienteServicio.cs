using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding;
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
                Cliente = new ClienteDTO
                {
                    id = sc.Id,
                    Cedula = sc.Cliente.Cedula,
                    NombreCliente = sc.Cliente.NombreCliente,
                    Telefono = sc.Cliente.Telefono,
                    Edad = sc.Cliente.Edad

                },
                Seguro = new SeguroDTO
                {
                    id = sc.Id,
                    NombreSeguro = sc.Seguro.NombreSeguro,
                    CodigoSeguro = sc.Seguro.CodigoSeguro,
                    SumaAsegurada = sc.Seguro.SumaAsegurada,
                    Prima = sc.Seguro.Prima
                }
            }).ToList();

            return segurosClienteDTOs;
        }

        public async Task<SegurosClienteDTO?> GetSegurosCliente(int idSegurosCliente)
        {
            try
            {
                SegurosCliente? sc = await context.SegurosClientes
                    .Include(s => s.Cliente)
                    .Include(s => s.Seguro)
                    .Where(a => a.Id == idSegurosCliente)
                    .FirstOrDefaultAsync();


                return sc == null
                    ? null
                    : new SegurosClienteDTO
                    {
                        Id = idSegurosCliente,
                        Cliente = sc.Cliente == null
                            ? null
                            : new ClienteDTO
                            {
                                Cedula = sc.Cliente.Cedula,
                                NombreCliente = sc.Cliente.NombreCliente,
                                Telefono = sc.Cliente.Telefono,
                                Edad = sc.Cliente.Edad
                            },
                        Seguro = sc.Seguro == null
                            ? null
                            : new SeguroDTO
                            {
                                NombreSeguro = sc.Seguro.NombreSeguro,
                                CodigoSeguro = sc.Seguro.CodigoSeguro,
                                SumaAsegurada = sc.Seguro.SumaAsegurada,
                                Prima = sc.Seguro.Prima
                            }
                    };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SegurosClienteDTO> AddSegurosCliente(CrearAseguradoDTO modelo)
        {
            try
            {
                if (await AseguradoExiste(modelo))
                {
                    throw new InvalidOperationException("Ya existe un asegurado con ese ClienteId y SeguroId");
                }

                var asegurado = new SegurosCliente
                {
                    ClienteId = modelo.ClienteId,
                    SeguroId = modelo.SeguroId,
                };

                context.SegurosClientes.Add(asegurado);
                await context.SaveChangesAsync();

                var resultDTO = new SegurosClienteDTO
                {
                    Cliente = new ClienteDTO { id = asegurado.ClienteId },
                    Seguro = new SeguroDTO { id = asegurado.SeguroId }
                };

                return resultDTO;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> AseguradoExiste(CrearAseguradoDTO modelo)
        {
            // Validar si ya existe un asegurado con la combinación de ClienteId y SeguroId
            return await context.SegurosClientes.AnyAsync(s =>
                s.ClienteId == modelo.ClienteId && s.SeguroId == modelo.SeguroId);
        }

        public async Task<bool> UpdateSegurosCliente(CrearAseguradoDTO modelo, int id)
        {

            // Validar que el SeguroId exista
            var seguroExistente = await context.Seguros.FindAsync(modelo.SeguroId);
            if (seguroExistente == null)
            {
                throw new ArgumentException($"No se encontró un seguro con el ID: {modelo.SeguroId}");
            }

            // Validar que el ClienteId exista
            var clienteExistente = await context.Clientes.FindAsync(modelo.ClienteId);
            if (clienteExistente == null)
            {
                throw new ArgumentException($"No se encontró un cliente con el ID: {modelo.ClienteId}");
            }


            var asegurado = await context.SegurosClientes.FindAsync(id);
            if (asegurado == null)
            {
                throw new ArgumentException($"No se encontró un seguro con el ID: {id}");
            }

            asegurado.SeguroId = modelo.SeguroId;
            asegurado.ClienteId = modelo.ClienteId;

            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSegurosCliente(int id)
        {
            try
            {
                var aseguradoExiste = await context.SegurosClientes.FindAsync(id);
                if (aseguradoExiste == null)
                {
                    throw new ArgumentException($"No se encontró un asegurado con el ID: {id}");
                }

                context.SegurosClientes.Remove(aseguradoExiste);
                await context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
