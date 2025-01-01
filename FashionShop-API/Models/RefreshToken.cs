using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Index("CustomerId", Name = "fk_refreshtokens_users")]
public partial class RefreshToken
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("customer_id")]
    public long CustomerId { get; set; }

    [Column("token", TypeName = "text")]
    public string Token { get; set; } = null!;

    [Column("expiresAt", TypeName = "datetime")]
    public DateTime ExpiresAt { get; set; }

    [Column("createdAt", TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Column("revokedAt", TypeName = "datetime")]
    public DateTime? RevokedAt { get; set; }

    [ForeignKey("CustomerId")]
    [InverseProperty("RefreshTokens")]
    public virtual Customer Customer { get; set; } = null!;
}
