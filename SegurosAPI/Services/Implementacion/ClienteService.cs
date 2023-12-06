using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SegurosAPI.DTOs;
using SegurosAPI.Models;
using SegurosAPI.Services.Contrato;

namespace SegurosAPI.Services.Implementacion
{
    public class ClienteService : ICliente
    {

        private readonly DBSegurosContext context;

        public ClienteService(DBSegurosContext context)
        {
            this.context = context;
        }

        public async Task<List<ClienteDTO>> GetList()
        {
            try
            {
                var clientes = await context.Clientes.ToListAsync();
                var listaClientes = clientes.Select(c => new ClienteDTO
                {
                    Cedula = c.Cedula,
                    NombreCliente = c.NombreCliente,
                    Telefono = c.Telefono,
                    Edad = c.Edad
                }).ToList();

                return listaClientes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ClienteDTO?> Get(int idCliente)
        {
            try
            {
                Cliente? cliente = await context.Clientes
                .Where(c => c.Id == idCliente)
                .FirstOrDefaultAsync();
                
                return cliente == null
                    ? null
                    : new ClienteDTO
                    {
                        Cedula = cliente.Cedula,
                        NombreCliente = cliente.NombreCliente,
                        Telefono = cliente.Telefono,
                        Edad = cliente.Edad
                    };

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<ClienteDTO> Add(ClienteDTO modelo)
        {
            try
            {
                if (string.IsNullOrEmpty(modelo.NombreCliente)
                    || string.IsNullOrEmpty(modelo.Cedula)
                    || string.IsNullOrEmpty(modelo.Telefono)
                    || modelo.Edad <= 0)
                {
                    throw new ArgumentException("Los campos requeridos deben ser proporcionados.");
                }
                // Verificar si ya existe un cliente con la misma cédula
                if (await ClienteConCedulaExiste(modelo.Cedula))
                {
                    throw new InvalidOperationException("Ya existe un cliente con la misma cédula.");
                }

                var cliente = new Cliente
                {
                    Cedula = modelo.Cedula,
                    NombreCliente = modelo.NombreCliente,
                    Telefono = modelo.Telefono,
                    Edad = modelo.Edad
                };

                context.Clientes.Add(cliente);
                await context.SaveChangesAsync();

                modelo.id = cliente.Id;

                return modelo;
            } catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<bool> ClienteConCedulaExiste(string cedula)
        {
            return await context.Clientes.AnyAsync(c => c.Cedula == cedula);
        }

        public async Task<bool> Update(ClienteDTO modelo, int id)
        {
            try
            {
                if (string.IsNullOrEmpty(modelo.NombreCliente)
                    || string.IsNullOrEmpty(modelo.Cedula)
                    || string.IsNullOrEmpty(modelo.Telefono)
                    || modelo.Edad <= 0)
                {
                    throw new ArgumentException("ID y otros campos requeridos deben ser proporcionados.");
                }

                var clienteExistente = await context.Clientes.FindAsync(id);
                if (clienteExistente == null)
                {
                    throw new ArgumentException($"No se encontró un cliente con el ID: {id}");
                }

                clienteExistente.Cedula = modelo.Cedula;
                clienteExistente.NombreCliente = modelo.NombreCliente;
                clienteExistente.Telefono = modelo.Telefono;
                clienteExistente.Edad = modelo.Edad;

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var clienteExistente = await context.Clientes.FindAsync(id);
                if (clienteExistente == null)
                {
                    throw new ArgumentException($"No se encontró un cliente con el ID: {id}");
                }

                context.Clientes.Remove(clienteExistente);
                await context.SaveChangesAsync();
                return true;
            } catch (Exception ex) 
            {
                throw ex;
            }
        }

    }
}
