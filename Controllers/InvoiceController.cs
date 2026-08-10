using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using test_2_debugging_code.Data;
using test_2_debugging_code.Models;

namespace test_2_debugging_code.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceDbContext _db;

        public InvoiceController(InvoiceDbContext db)
        {
            _db = db;
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetInvoice(int id)
        {
            var invoice = await _db.Invoices
                .Include(i => i.Items)
                .FirstOrDefaultAsync(i => i.InvoiceID == id);

            if (invoice == null)
                return NotFound($"No invoice found with ID {id}");

            return Ok(ToDto(invoice));
        }

        [HttpGet]
        public async Task<IActionResult> GetLatestInvoice()
        {
            var invoice = await _db.Invoices
                .Include(i => i.Items)
                .OrderByDescending(i => i.InvoiceID)
                .FirstOrDefaultAsync();

            if (invoice == null)
                return NotFound("No invoices found");

            return Ok(ToDto(invoice));
        }

        private static object ToDto(Invoice invoice) => new
        {
            invoice.InvoiceID,
            invoice.CustomerName,
            Items = invoice.Items.Select(i => new { i.Name, i.Price }),
            Total = invoice.Items.Sum(i => i.Price)
        };
    }
}
