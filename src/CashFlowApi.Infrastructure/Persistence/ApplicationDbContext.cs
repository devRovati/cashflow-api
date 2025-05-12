using CashFlowApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlowApi.Infrastructure.Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}

    public DbSet<UserEntity> Users { get; set; }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<TransactionEntity> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("pk_users_user_id");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.CreatedBy).HasConstraintName("fk_users_users_created_by");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UpdatedBy).HasConstraintName("fk_users_users_updated_by");

            entity.HasIndex(e => e.Email).HasDatabaseName("ix_users_email");

            entity.Property(p => p.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(p => p.UpdatedAt).HasColumnType("datetime(3)");
        });

        modelBuilder.Entity<CategoryEntity>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("pk_categories_category_id");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.CreatedBy).HasConstraintName("fk_categories_users_created_by");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UpdatedBy).HasConstraintName("fk_categories_users_updated_by");

            entity.HasIndex(e => e.CreatedBy).HasDatabaseName("ix_categories_created_by");
            entity.HasIndex(e => e.UpdatedBy).HasDatabaseName("ix_categories_updated_by");

            entity.Property(p => p.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(p => p.UpdatedAt).HasColumnType("datetime(3)");
        });

        modelBuilder.Entity<TransactionEntity>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("pk_transactions_transaction_id");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.CreatedBy).HasConstraintName("fk_transactions_users_created_by");
            entity.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UpdatedBy).HasConstraintName("fk_transactions_users_updated_by");

            entity.HasIndex(e => e.CreatedBy).HasDatabaseName("ix_transactions_created_by");
            entity.HasIndex(e => e.UpdatedBy).HasDatabaseName("ix_transactions_updated_by");

            entity.Property(p => p.PaymentDate).HasColumnType("datetime(3)");
            entity.Property(p => p.CreatedAt).HasColumnType("datetime(3)");
            entity.Property(p => p.UpdatedAt).HasColumnType("datetime(3)");
        });
    }
}
