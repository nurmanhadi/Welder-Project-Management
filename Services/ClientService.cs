using Microsoft.EntityFrameworkCore;
using WelderProjectManagement.Contexts;
using WelderProjectManagement.Dtos;
using WelderProjectManagement.Exceptions;
using WelderProjectManagement.Models;

namespace WelderProjectManagement.Services;

public class ClientService
{
    private readonly WelderProjectManagementContext db;

    public ClientService(WelderProjectManagementContext db)
    {
        this.db = db;
    }

    public async Task AddClient(ClientAddRequest request)
    {
        var client = new Client
        {
            Name = request.Name,
            Phone = request.Phone,
            Address = request.Address
        };

        db.Clients.Add(client);
        await db.SaveChangesAsync();
    }

    public async Task UpdateClient(long id, ClientUpdateRequest request)
    {
        var client = await db.Clients.FindAsync(id) ?? throw new ClientNotFoundException(id);
        client.Name = !string.IsNullOrWhiteSpace(request.Name) ? request.Name : client.Name;
        client.Phone = !string.IsNullOrWhiteSpace(request.Phone) ? request.Phone : client.Phone;
        client.Address = !string.IsNullOrWhiteSpace(request.Address) ? request.Address : client.Address;
        db.Clients.Add(client);
        await db.SaveChangesAsync();
    }

    public async Task<ClientResponse> GetClientById(long id)
    {
        var client = await db.Clients.FindAsync(id) ?? throw new ClientNotFoundException(id);
        return new ClientResponse
        {
            Id = client.Id,
            Name = client.Name,
            Phone = client.Phone,
            Address = client.Address
        };
    }

    public async Task<WebPagination<List<ClientResponse>>> GetClients(int page, int size)
    {
        var clients = await db.Clients
        .OrderBy(x => x.Id)
        .Skip((page - 1) * size)
        .Take(size)
        .Select(x => new ClientResponse
        {
            Id = x.Id,
            Name = x.Name,
            Phone = x.Phone,
            Address = x.Address
        })
        .ToListAsync();

        int totalElements = db.Clients.Count();
        int totalPages = (totalElements + size - 1) / size;

        return new WebPagination<List<ClientResponse>>
        {
            Contents = clients,
            Page = page,
            Size = size,
            TotalPages = totalPages,
            TotalElements = totalElements,
        };
    }

    public async Task DeleteClient(long id)
    {
        var client = await db.Clients.FindAsync(id) ?? throw new ClientNotFoundException(id);
        db.Clients.Remove(client);
        await db.SaveChangesAsync();
    }
}
