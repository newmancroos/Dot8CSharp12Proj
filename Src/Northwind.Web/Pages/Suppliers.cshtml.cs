using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Northwind.DataContext.Sqlite;
using Northwind.EntityModels;

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{

    public IEnumerable<Supplier> Suppliers { get; set; } = null!;
    private readonly NorthwindContext _context;
    public SuppliersModel(NorthwindContext context)
    {
        _context = context;
    }
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";

        Suppliers = _context.Suppliers.OrderBy(o => o.Country)
            .ThenBy(t => t.CompanyName);
    }

    [BindProperty]
    public Supplier Supplier { get; set; }

    public IActionResult OnPost()
    {
        if (Supplier is not null && ModelState.IsValid)
        {
            _context.Suppliers.Add(Supplier);
            _context.SaveChanges();
            return RedirectToPage("/suppliers");
        }
        else
        {
            return Page(); // Return to original page.
        }
    }
}
