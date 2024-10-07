using Microsoft.EntityFrameworkCore;
using Northwind.Balazer.Services;
using Northwind.DataContext.Sqlite;
using Northwind.EntityModels;

namespace Northwind.Blazor.Services;
public class NorthwindServiceServerSide : INorthwindService
{
    private readonly NorthwindContext _db;

    public NorthwindServiceServerSide(NorthwindContext db)
    {
        _db = db;
    }

    public async Task<Customer> CreateCustomerAsync(Customer customer)
    {
        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();
        return customer;
    }

    public async Task DeleteCustomerAsync(string id)
    {
        Customer? customer =await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);

        if (customer is null) return;

        _db.Customers.Remove(customer); 
        await _db.SaveChangesAsync();
    }

    public async Task<Customer?> GetCustomerAsync(string id)
    {
        return await _db.Customers.FirstOrDefaultAsync(x => x.CustomerId == id);
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        return await _db.Customers.ToListAsync();
    }

    public async Task<List<Customer>> GetCustomersAsync(string country)
    {
        return await _db.Customers.Where(x=>x.Country == country).ToListAsync();
    }

    public async Task<Customer> UpdateCustomerAsync(Customer customer)
    {
        _db.Entry(customer).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return customer;
    }
}
