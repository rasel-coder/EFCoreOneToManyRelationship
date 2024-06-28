using Microsoft.EntityFrameworkCore;

namespace TestEFCore.Models;

public class TestEFCoreContext : DbContext
{
    public TestEFCoreContext(DbContextOptions<TestEFCoreContext> options)
            : base(options)
    { }

    public virtual DbSet<Invoice> Invoices { get; set; }
    public virtual DbSet<Purchase> Purchases { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.HasMany(d => d.Purchases)
                .WithOne(p => p.Invoice)
                .HasForeignKey(d => d.InvoiceId)
                .HasConstraintName("FK_Purchase_InvoiceId");
        });

        base.OnModelCreating(modelBuilder);
    }
}
