using Microsoft.EntityFrameworkCore;
using test_2_debugging_code.Models;

namespace test_2_debugging_code.Data
{
    public class InvoiceDbContext : DbContext
    {
        public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options) : base(options)
        {
        }

        public DbSet<Invoice> Invoices => Set<Invoice>();
        public DbSet<InvoiceItem> InvoiceItems => Set<InvoiceItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoices");
                entity.HasKey(i => i.InvoiceID);
                entity.Property(i => i.CustomerName).HasMaxLength(100);
            });

            modelBuilder.Entity<InvoiceItem>(entity =>
            {
                entity.ToTable("InvoiceItems");
                entity.HasKey(i => i.ItemID);
                entity.Property(i => i.Name).HasMaxLength(100);
                entity.Property(i => i.Price).HasColumnType("decimal(10,2)");

                entity.HasOne(i => i.Invoice)
                      .WithMany(inv => inv.Items)
                      .HasForeignKey(i => i.InvoiceID);
            });
        }
    }
}
