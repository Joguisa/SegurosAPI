using Microsoft.EntityFrameworkCore;
using SegurosAPI.DTOs;
using SegurosAPI.Models;
using SegurosAPI.Services.Contrato;

namespace SegurosAPI.Services.Implementacion
{
    public class SegurosClienteServicio : ISegurosClientes
    {
        private readonly DBSegurosContext _context;

        public SegurosClienteServicio(DBSegurosContext context)
        {
            this._context = context;
        }

        public async Task<List<SegurosClienteDto>> GetSegurosClientesList()
        {
            var segurosClientes = await _context.SegurosClientes
                .Include(sc => sc.Cliente)
                .Include(sc => sc.Seguro)
                .ToListAsync();

            var segurosClienteDtos = segurosClientes.Select(sc => new SegurosClienteDto
            {
                Id = sc.Id,
                Cliente = new ClienteDto
                {
                    Id = sc.Id,
                    Cedula = sc.Cliente.Cedula,
                    NombreCliente = sc.Cliente.NombreCliente,
                    Telefono = sc.Cliente.Telefono,
                    Edad = sc.Cliente.Edad

                },
                Seguro = new SeguroDto
                {
                    Id = sc.Id,
                    NombreSeguro = sc.Seguro.NombreSeguro,
                    CodigoSeguro = sc.Seguro.CodigoSeguro,
                    SumaAsegurada = sc.Seguro.SumaAsegurada,
                    Prima = sc.Seguro.Prima
                }
            }).ToList();

            return segurosClienteDtos;
        }

        public async Task<SegurosClienteDto?> GetSegurosCliente(int idSegurosCliente)
        {
            try
            {
                var sc = await _context.SegurosClientes
                    .Include(s => s.Cliente)
                    .Include(s => s.Seguro)
                    .Where(a => a.Id == idSegurosCliente)
                    .FirstOrDefaultAsync();
                
                if (sc == null)
                {
                    throw new ArgumentException($"No se encontró un seguro con el ID: {idSegurosCliente}");
                }
                
                return new SegurosClienteDto
                    {
                        Id = idSegurosCliente,
                            Cliente = new ClienteDto
                            {
                                Cedula = sc.Cliente.Cedula,
                                NombreCliente = sc.Cliente.NombreCliente,
                                Telefono = sc.Cliente.Telefono,
                                Edad = sc.Cliente.Edad
                            },
                        Seguro =  new SeguroDto
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

        public async Task<SegurosClienteDto> AddSegurosCliente(CrearAseguradoDto modelo)
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
                
                // Obtener los datos del cliente y seguro después de guardar en la base de datos
                var cliente = await _context.Clientes.FindAsync(asegurado.ClienteId);
                var seguro = await _context.Seguros.FindAsync(asegurado.SeguroId);

                _context.SegurosClientes.Add(asegurado);
                await _context.SaveChangesAsync();

                var resultDto = new SegurosClienteDto
                {
                    Id = asegurado.Id,
                    Cliente = new ClienteDto
                    {
                        Cedula = cliente.Cedula,
                        NombreCliente = cliente.NombreCliente,
                        Telefono = cliente.Telefono,
                        Edad = cliente.Edad
                    },
                    Seguro = new SeguroDto
                    {
                        NombreSeguro = seguro.NombreSeguro,
                        CodigoSeguro = seguro.CodigoSeguro,
                        SumaAsegurada = seguro.SumaAsegurada,
                        Prima = seguro.Prima
                    }
                };

                return resultDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> AseguradoExiste(CrearAseguradoDto modelo)
        {
            // Validar si ya existe un asegurado con la combinación de ClienteId y SeguroId
            return await _context.SegurosClientes.AnyAsync(s =>
                s.ClienteId == modelo.ClienteId && s.SeguroId == modelo.SeguroId);
        }

        public async Task<bool> UpdateSegurosCliente(CrearAseguradoDto modelo, int id)
        {

            // Validar que el SeguroId exista
            var seguroExistente = await _context.Seguros.FindAsync(modelo.SeguroId);
            if (seguroExistente == null)
            {
                throw new ArgumentException($"No se encontró un seguro con el ID: {modelo.SeguroId}");
            }

            // Validar que el ClienteId exista
            var clienteExistente = await _context.Clientes.FindAsync(modelo.ClienteId);
            if (clienteExistente == null)
            {
                throw new ArgumentException($"No se encontró un cliente con el ID: {modelo.ClienteId}");
            }


            var asegurado = await _context.SegurosClientes.FindAsync(id);
            if (asegurado == null)
            {
                throw new ArgumentException($"No se encontró un seguro con el ID: {id}");
            }

            asegurado.SeguroId = modelo.SeguroId;
            asegurado.ClienteId = modelo.ClienteId;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteSegurosCliente(int id)
        {
            try
            {
                var aseguradoExiste = await _context.SegurosClientes.FindAsync(id);
                if (aseguradoExiste == null)
                {
                    throw new ArgumentException($"No se encontró un asegurado con el ID: {id}");
                }

                _context.SegurosClientes.Remove(aseguradoExiste);
                await _context.SaveChangesAsync();
                return true;
            } catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
