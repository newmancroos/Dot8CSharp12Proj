using Microsoft.EntityFrameworkCore;
using Northwind.EntityModels;
namespace Northwind.DataContext.Sqlite;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AlphabeticalListOfProduct> AlphabeticalListOfProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategorySalesFor1997> CategorySalesFor1997s { get; set; }

    public virtual DbSet<CurrentProductList> CurrentProductLists { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAndSuppliersByCity> CustomerAndSuppliersByCities { get; set; }

    public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Invoice> Invoices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<OrderDetailsExtended> OrderDetailsExtendeds { get; set; }

    public virtual DbSet<OrderSubtotal> OrderSubtotals { get; set; }

    public virtual DbSet<OrdersQry> OrdersQries { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDetailsV> ProductDetailsVs { get; set; }

    public virtual DbSet<ProductSalesFor1997> ProductSalesFor1997s { get; set; }

    public virtual DbSet<ProductsAboveAveragePrice> ProductsAboveAveragePrices { get; set; }

    public virtual DbSet<ProductsByCategory> ProductsByCategories { get; set; }

    public virtual DbSet<QuarterlyOrder> QuarterlyOrders { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<SalesByCategory> SalesByCategories { get; set; }

    public virtual DbSet<SalesTotalsByAmount> SalesTotalsByAmounts { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<SummaryOfSalesByQuarter> SummaryOfSalesByQuarters { get; set; }

    public virtual DbSet<SummaryOfSalesByYear> SummaryOfSalesByYears { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string database = "Northwind.db";
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;

            if (dir.EndsWith("not8.0"))
            {
                path = Path.Combine("..", "..", "..", "..", database);
            }
            else
            {
                path = Path.Combine("..", database);
            }

            path = Path.GetFullPath(path);

            NorthwindContextLogger.WriteLine($"Database path: {path}");

            if (!File.Exists(path))
            { 
                throw new FileNotFoundException(message: $"{path} not found", fileName: path);  
            }

            optionsBuilder.UseSqlite($"Data Source ={path}");

            optionsBuilder.LogTo(NorthwindContextLogger.WriteLine, new[] { Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting });
        }
    }
    //=> optionsBuilder.UseSqlite("Data Source=Northwind.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AlphabeticalListOfProduct>(entity =>
        {
            entity.ToView("Alphabetical list of products");
        });

        modelBuilder.Entity<CategorySalesFor1997>(entity =>
        {
            entity.ToView("Category Sales for 1997");
        });

        modelBuilder.Entity<CurrentProductList>(entity =>
        {
            entity.ToView("Current Product List");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasMany(d => d.CustomerTypes).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerCustomerDemo",
                    r => r.HasOne<CustomerDemographic>().WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("CustomerId", "CustomerTypeId");
                        j.ToTable("CustomerCustomerDemo");
                        j.IndexerProperty<string>("CustomerId").HasColumnName("CustomerID");
                        j.IndexerProperty<string>("CustomerTypeId").HasColumnName("CustomerTypeID");
                    });
        });

        modelBuilder.Entity<CustomerAndSuppliersByCity>(entity =>
        {
            entity.ToView("Customer and Suppliers by City");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeTerritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("EmployeeId", "TerritoryId");
                        j.ToTable("EmployeeTerritories");
                        j.IndexerProperty<int>("EmployeeId").HasColumnName("EmployeeID");
                        j.IndexerProperty<string>("TerritoryId").HasColumnName("TerritoryID");
                    });
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToView("Invoices");
        });

        //modelBuilder.Entity<Order>(entity =>
        //{
        //    entity.Property(e => e.Freight).HasDefaultValue(0);
        //});

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderDetailsExtended>(entity =>
        {
            entity.ToView("Order Details Extended");
        });

        modelBuilder.Entity<OrderSubtotal>(entity =>
        {
            entity.ToView("Order Subtotals");
        });

        modelBuilder.Entity<OrdersQry>(entity =>
        {
            entity.ToView("Orders Qry");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Discontinued).HasDefaultValue("0");
            entity.Property(e => e.ReorderLevel).HasDefaultValue(0);
            entity.Property(e => e.UnitPrice).HasConversion<double>().HasDefaultValue(0.0);
            entity.Property(e => e.UnitsInStock).HasDefaultValue(0);
            entity.Property(e => e.UnitsOnOrder).HasDefaultValue(0);
        });

        modelBuilder.Entity<ProductDetailsV>(entity =>
        {
            entity.ToView("ProductDetails_V");
        });

        modelBuilder.Entity<ProductSalesFor1997>(entity =>
        {
            entity.ToView("Product Sales for 1997");
        });

        modelBuilder.Entity<ProductsAboveAveragePrice>(entity =>
        {
            entity.ToView("Products Above Average Price");
        });

        modelBuilder.Entity<ProductsByCategory>(entity =>
        {
            entity.ToView("Products by Category");
        });

        modelBuilder.Entity<QuarterlyOrder>(entity =>
        {
            entity.ToView("Quarterly Orders");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.Property(e => e.RegionId).ValueGeneratedNever();
        });

        modelBuilder.Entity<SalesByCategory>(entity =>
        {
            entity.ToView("Sales by Category");
        });

        modelBuilder.Entity<SalesTotalsByAmount>(entity =>
        {
            entity.ToView("Sales Totals by Amount");
        });

        modelBuilder.Entity<SummaryOfSalesByQuarter>(entity =>
        {
            entity.ToView("Summary of Sales by Quarter");
        });

        modelBuilder.Entity<SummaryOfSalesByYear>(entity =>
        {
            entity.ToView("Summary of Sales by Year");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasOne(d => d.Region).WithMany(p => p.Territories).OnDelete(DeleteBehavior.ClientSetNull);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
