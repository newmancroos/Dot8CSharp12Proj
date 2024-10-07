using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
public partial class Invoice
{
    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    [Column("CustomerID")]
    public string? CustomerId { get; set; }

    public string? CustomerName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public int? Salesperson { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? RequiredDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }

    public string? ShipperName { get; set; }

    [Column("ProductID")]
    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? UnitPrice { get; set; }

    public int? Quantity { get; set; }

    public double? Discount { get; set; }

    public double? ExtendedPrice { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? Freight { get; set; }
}
