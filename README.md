# Entity Framework Core - One To Many Relation

https://github.com/raseldotnetcoder/EFCoreOneToManyRelationship/assets/115859775/ba95124f-f13f-43ed-9181-63fc1710756d

## Instructions
- Change the database connection string from appsettings.json
- Run ADD-MIGRATION MIGRATION_NAME to the package manager console
- Run UPDATE-DATABASE to the package manager console


### Invoice Model
````
public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public float TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
````

### Purchase Model
````
public class Purchase
{
    [Key]
    public int PurchaseId { get; set; }

    [DisplayName("Product Name")]
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public float Price { get; set; }
    public int InvoiceId { get; set; }

    public virtual Invoice Invoice { get; set; } = new Invoice();
}
````

### EF Core DB Context
````
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
````

### Controller
````
public class HomeController : Controller
{
    private readonly TestEFCoreContext _context;

    public HomeController(TestEFCoreContext _context) => this._context = _context;

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(List<Purchase> purchases)
    {
        if (purchases != null)
        {
            float totalAmount = purchases.Sum(p => p.Price * p.Quantity);
            var invoiceNo = Guid.NewGuid().ToString().ToUpper();

            Invoice invoice = new Invoice
            {
                InvoiceNo = invoiceNo.Substring(invoiceNo.Length - 10),
                TotalAmount = totalAmount,
                Purchases = purchases
            };

            await _context.AddAsync(invoice);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }
}
````

### Database Output
![OneToManyDBResult](https://github.com/raseldotnetcoder/EFCoreOneToManyRelationship/assets/115859775/8e99e72c-f35c-453e-8f58-a00c3171acec)
