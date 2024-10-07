using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
public partial class CategorySalesFor1997
{
    public string? CategoryName { get; set; }

    public byte[]? CategorySales { get; set; }
}
