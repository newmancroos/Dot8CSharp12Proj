using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
public partial class AlphabeticalListOfProduct
{
    [Column("ProductID")]
    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    [Column("SupplierID")]
    public int? SupplierId { get; set; }

    [Column("CategoryID")]
    public int? CategoryId { get; set; }

    public string? QuantityPerUnit { get; set; }

    [Column(TypeName = "NUMERIC")]
    public double? UnitPrice { get; set; }

    public int? UnitsInStock { get; set; }

    public int? UnitsOnOrder { get; set; }

    public int? ReorderLevel { get; set; }

    public string? Discontinued { get; set; }

    public string? CategoryName { get; set; }
}
