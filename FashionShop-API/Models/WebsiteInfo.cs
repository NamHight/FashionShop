using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("WebsiteInfo")]
[Index("Email", Name = "email_UNIQUE", IsUnique = true)]
[Index("SiteName", Name = "siteName_UNIQUE", IsUnique = true)]
public partial class WebsiteInfo
{
    [Key]
    [Column("websiteId")]
    public int WebsiteId { get; set; }

    [Column("siteName")]
    public string SiteName { get; set; } = null!;

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("email")]
    public string Email { get; set; } = null!;

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("address", TypeName = "text")]
    public string? Address { get; set; }

    [Column("logo")]
    [StringLength(255)]
    public string? Logo { get; set; }

    [Column("updateAt", TypeName = "timestamp")]
    public DateTime? UpdateAt { get; set; }

    [Column(TypeName = "enum('active','inactive','deleted')")]
    public string? Status { get; set; }

    [Column("createdAt", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }
}
