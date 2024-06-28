using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestEFCore.Models;

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
