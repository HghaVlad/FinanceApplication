using FinanceAccountLibrary.models;
using Microsoft.EntityFrameworkCore;

namespace FinanceAccountLibrary.repository

{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Operation> Operations { get; set; }

        public FinanceDbContext(DbContextOptions<FinanceDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Category entity
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Type).IsRequired();
            });

            // Configure BankAccount entity
            modelBuilder.Entity<BankAccount>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
                entity.Property(e => e.Balance).IsRequired();
            });

            // Configure Operation entity
            modelBuilder.Entity<Operation>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.BankAccountId).IsRequired();
                entity.Property(e => e.Amount).IsRequired();
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.Date).IsRequired();
            });
        }
    }
}