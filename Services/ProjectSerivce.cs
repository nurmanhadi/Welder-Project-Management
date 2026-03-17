using Microsoft.EntityFrameworkCore;
using WelderProjectManagement.Contexts;
using WelderProjectManagement.Dtos;
using WelderProjectManagement.Exceptions;
using WelderProjectManagement.Helpers;
using WelderProjectManagement.Models;

namespace WelderProjectManagement.Services;

public class ProjectService
{
    private readonly WelderProjectManagementContext db;

    public ProjectService(WelderProjectManagementContext db)
    {
        this.db = db;
    }

    public async Task AddProject(ProjectAddRequest request)
    {
        using var transaction = await db.Database.BeginTransactionAsync();
        try
        {
            // client
            var client = await db.Clients.FirstOrDefaultAsync(x => x.Phone == request.Client.Phone);
            if (client == null)
            {
                client = new Client
                {
                    Name = request.Client.Name,
                    Phone = request.Client.Phone,
                    Address = request.Client.Address
                };

                db.Clients.Add(client);
                await db.SaveChangesAsync();
            }

            // project
            var project = new Project
            {
                Title = request.Title,
                Description = request.Description,
                Status = Helpers.ProjectStatus.APPROVED,
                ClientId = client.Id
            };
            db.Projects.Add(project);
            await db.SaveChangesAsync();

            var projectId = project.Id;

            // project items
            decimal projectItemPrice = 0;
            if (request.ProjectItems.Count != 0)
            {
                List<ProjectItem> projectItems = [];
                foreach (var x in request.ProjectItems)
                {
                    projectItems.Add(new ProjectItem
                    {
                        Name = x.Name,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        ProjectId = projectId
                    });
                    decimal totalPrice = x.Price * x.Quantity;
                    projectItemPrice += totalPrice;
                }
                db.ProjectItems.AddRange(projectItems);
                await db.SaveChangesAsync();
            }

            // payment
            var payment = new Payment
            {
                Amount = request.Payment.Amount,
                Type = request.Payment.Type,
                ProjectId = projectId
            };
            db.Payments.Add(payment);
            await db.SaveChangesAsync();

            // invoice
            var countToday = await db.Projects.CountAsync(x => x.CreatedAt.Date == DateTime.UtcNow.Date);
            var total = projectItemPrice + request.Invoice.ServiceFee;
            var invoice = new Invoice
            {
                InvoiceNumber = GenerateInvoiceNumber.New(countToday),
                DueDate = request.Invoice.DueDate,
                ServiceFee = request.Invoice.ServiceFee,
                Total = total,
                PaidAmount = payment.Amount,
                Balance = total > payment.Amount ? total - payment.Amount : 0,
                ProjectId = projectId
            };
            db.Invoices.Add(invoice);
            await db.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<WebPagination<List<ProjectResponse>>> GetProjects(int page, int size)
    {
        var projects = await db.Projects
        .OrderByDescending(x => x.Id)
        .Skip((page - 1) * size)
        .Select(x => new ProjectResponse
        {
            Id = x.Id,
            Title = x.Title,
            Description = x.Description,
            Status = x.Status,
            CreatedAt = x.CreatedAt
        })
        .ToListAsync();

        int totalElements = await db.Projects.CountAsync();
        int totalPages = (totalElements + size - 1) / size;

        return new WebPagination<List<ProjectResponse>>
        {
            Contents = projects,
            Page = page,
            Size = size,
            TotalPages = totalPages,
            TotalElements = totalPages
        };
    }

    public async Task<ProjectResponse> GetProjectById(long id)
    {
        var project = await db.Projects
        .Where(x => x.Id == id)
        .Select(p => new ProjectResponse
        {
            Id = p.Id,
            Title = p.Title,
            Description = p.Description,
            Status = p.Status,
            CreatedAt = p.CreatedAt,
            Client = new ClientResponse
            {
                Id = p.Client.Id,
                Name = p.Client.Name,
                Phone = p.Client.Phone,
                Address = p.Client.Address
            },
            ProjectItems = p.ProjectItems.Select(pi => new ProjectItemsResponse
            {
                Id = pi.Id,
                Name = pi.Name,
                Quantity = pi.Quantity,
                Price = pi.Price
            }).ToList(),
            Payment = p.Payments.Select(pp => new PaymentResponse
            {
                Id = pp.Id,
                Amount = pp.Amount,
                Type = pp.Type,
                PaymentDate = pp.PaymentDate
            }).ToList(),
            Invoice = p.Invoice == null ? null : new InvoiceResponse
            {
                Id = p.Invoice.Id,
                InvoiceNumber = p.Invoice.InvoiceNumber,
                DueDate = p.Invoice.DueDate,
                ServiceFee = p.Invoice.ServiceFee,
                Total = p.Invoice.Total,
                PaidAmount = p.Invoice.PaidAmount,
                Balance = p.Invoice.Balance,
                IssueDate = p.Invoice.IssueDate
            }
        })
        .FirstOrDefaultAsync()
        ?? throw new ProjectNotFoundException(id);

        return project;
    }
}
