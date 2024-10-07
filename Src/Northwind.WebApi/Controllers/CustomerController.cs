using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Northwind.DataContext.Sqlite;
using Northwind.EntityModels;

namespace Northwind.WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CustomerController:ControllerBase
    {
        private readonly NorthwindContext _context;
        public CustomerController(NorthwindContext context)
        {
            _context = context;

        }

        [HttpGet("customers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Customer>>> Customers(string? country)
        {
            if(string.IsNullOrEmpty(country))
            {
                return await _context.Customers.Where(p => p.Country==country).ToListAsync();
            }
            return await  _context.Customers.ToListAsync();
        }
    }
}
