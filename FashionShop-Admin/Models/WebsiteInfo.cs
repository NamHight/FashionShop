using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop.Models;

[Table("WebsiteInfo")]
public partial class WebsiteInfo
{
    [Key]
    [Column("websiteId")]
    public int WebsiteId { get; set; }

    [Column("websiteKey")]
    [StringLength(255)]
    public string WebsiteKey { get; set; } = null!;

    [Column("webisteValue", TypeName = "text")]
    public string WebisteValue { get; set; } = null!;

    [Column("created_at", TypeName = "timestamp")]
    public DateTime? CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime? UpdatedAt { get; set; }
}
