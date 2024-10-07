using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Northwind.EntityModels;

[Keyless]
public partial class SalesTotalsByAmount
{
    public byte[]? SaleAmount { get; set; }

    [Column("OrderID")]
    public int? OrderId { get; set; }

    public string? CompanyName { get; set; }

    [Column(TypeName = "DATETIME")]
    public DateTime? ShippedDate { get; set; }
}
