using CashFlowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}

    public DbSet<TransactionEntity> Transactions { get; set; }

}
