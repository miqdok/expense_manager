using Microsoft.EntityFrameworkCore;
using ExpenseManager.Repositories.Models;

namespace ExpenseManager.Repositories.Data;

public class AppDbContext : DbContext
{
    public DbSet<WalletEntity> Wallets => Set<WalletEntity>();
    public DbSet<TransactionEntity> Transactions => Set<TransactionEntity>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WalletEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Currency).HasConversion<string>();
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Category).HasConversion<string>();
            entity.HasOne(e => e.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(e => e.WalletId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
