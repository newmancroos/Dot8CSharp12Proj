using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
public partial class OrdersQry
{
    [Column("OrderID")]
    public int? OrderId { get; set; }

    [Column("CustomerID")]
    public string? CustomerId { get; set; }

    [Column("EmployeeID")]
    public int? EmployeeId { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? OrderDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? RequiredDate { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }

    public int? ShipVia { get; set; }

    [Column(TypeName = "NUMERIC")]
    public int? Freight { get; set; }

    public string? ShipName { get; set; }

    public string? ShipAddress { get; set; }

    public string? ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public string? ShipPostalCode { get; set; }

    public string? ShipCountry { get; set; }

    public string? CompanyName { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }
}
