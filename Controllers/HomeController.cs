using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestEFCore.Models;

namespace TestEFCore.Controllers
{
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
}
