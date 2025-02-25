﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace FashionShop_API.Models;

[Table("contacts")]
public partial class Contact
{
    [Key]
    [Column("contact_id")]
    public long ContactId { get; set; }

    [Column("fullname")]
    [StringLength(255)]
    public string? Fullname { get; set; }

    [Column("email")]
    [StringLength(255)]
    public string? Email { get; set; }

    [Column("description", TypeName = "text")]
    public string? Description { get; set; }

    [Column("phone")]
    [StringLength(15)]
    public string? Phone { get; set; }

    [Column("status", TypeName = "enum('pending','resovle','importain')")]
    public string? Status { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp")]
    public DateTime UpdatedAt { get; set; }
}
