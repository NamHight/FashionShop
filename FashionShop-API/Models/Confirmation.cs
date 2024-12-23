using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

public partial class Confirmation
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("employeeId")]
    public long EmployeeId { get; set; }

    [Column("token")]
    [StringLength(255)]
    public string Token { get; set; } = null!;

    [Column("expireAt", TypeName = "datetime")]
    public DateTime ExpireAt { get; set; }

    [Column("isUsed")]
    public bool? IsUsed { get; set; }
}
