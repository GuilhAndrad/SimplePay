using Microsoft.EntityFrameworkCore;
using SimplePay.Domain.Entities;

namespace SimplePay.Infrastructure.DataAccess;

internal sealed class SimplePayDbContext(DbContextOptions<SimplePayDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; private set; } = null!;
    public DbSet<Wallet> Wallets { get; private set; } = null!;
    public DbSet<Transfer> Transfers { get; private set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Wallet>()
            .Property(wallet => wallet.Version)
            .IsConcurrencyToken();
    }
}
