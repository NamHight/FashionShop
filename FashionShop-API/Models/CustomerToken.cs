using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[PrimaryKey("CustomerId", "LoginProvider", "Name")]
[Table("CustomerToken")]
public partial class CustomerToken
{
    [Key]
    [Column("customer_id")]
    public long CustomerId { get; set; }

    [Key]
    [Column("login_provider")]
    public string LoginProvider { get; set; } = null!;

    [Key]
    [Column("name")]
    [StringLength(225)]
    public string Name { get; set; } = null!;

    [Column("token", TypeName = "text")]
    public string? Token { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("CustomerTokens")]
    public virtual Customer Customer { get; set; } = null!;
}
