using System.ComponentModel.DataAnnotations;

namespace TestEFCore.Models;

public class Invoice
{
    [Key]
    public int InvoiceId { get; set; }
    public string InvoiceNo { get; set; } = string.Empty;
    public float TotalAmount { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
