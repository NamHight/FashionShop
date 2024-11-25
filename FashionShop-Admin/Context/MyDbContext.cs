﻿using System;
using System.Collections.Generic;
using FashionShop.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace FashionShop.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Cartsdetail> Cartsdetails { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Ordersdetail> Ordersdetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Purchaseorder> Purchaseorders { get; set; }

    public virtual DbSet<Purchaseorderdetail> Purchaseorderdetails { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Stock> Stocks { get; set; }

    public virtual DbSet<Store> Stores { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<View> Views { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySql(Environment.GetEnvironmentVariable("DefaultConnection"), Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'available'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'waiting'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts).HasConstraintName("fk_carts_customer");
        });

        modelBuilder.Entity<Cartsdetail>(entity =>
        {
            entity.HasKey(e => e.CartDetailId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'waiting'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Cart).WithMany(p => p.Cartsdetails).HasConstraintName("fk_cartsdetail_cart");

            entity.HasOne(d => d.Product).WithMany(p => p.Cartsdetails).HasConstraintName("fk_cartsdetail_product");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Email).HasDefaultValueSql("_utf8mb4\\'abc@gmail.com\\'");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Email).HasDefaultValueSql("_utf8mb4\\'abc@gmail.com\\'");
            entity.Property(e => e.EmployeePosition).HasDefaultValueSql("_utf8mb4\\'intern\\'");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees).HasConstraintName("fk_employees_role");

            entity.HasOne(d => d.Store).WithMany(p => p.Employees).HasConstraintName("fk_employees_store");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.FavoriteId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Customer).WithMany(p => p.Favorites).HasConstraintName("fk_favorites_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Favorites).HasConstraintName("fk_favorites_product");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("fk_orders_customer");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders).HasConstraintName("fk_orders_employee");

            entity.HasOne(d => d.Store).WithMany(p => p.Orders).HasConstraintName("fk_orders_store");
        });

        modelBuilder.Entity<Ordersdetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");

            entity.HasOne(d => d.Order).WithMany(p => p.Ordersdetails).HasConstraintName("fk_ordersdetails_order");

            entity.HasOne(d => d.Product).WithMany(p => p.Ordersdetails).HasConstraintName("fk_ordersdetails_product");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'available'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Category).WithMany(p => p.Products).HasConstraintName("fk_products_category");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Purchaseorder>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Store).WithMany(p => p.Purchaseorders).HasConstraintName("fk_purchaseorders_store");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Purchaseorders).HasConstraintName("fk_purchaseorders_supplier");
        });

        modelBuilder.Entity<Purchaseorderdetail>(entity =>
        {
            entity.HasKey(e => e.PurchaseOrderDetailId).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");

            entity.HasOne(d => d.Product).WithMany(p => p.Purchaseorderdetails).HasConstraintName("fk_purchaseorderdetails_product");

            entity.HasOne(d => d.PurchaseOrder).WithMany(p => p.Purchaseorderdetails).HasConstraintName("fk_purchaseorderdetails_purchaseorder");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'pending'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Customer).WithMany(p => p.Reviews).HasConstraintName("fk_reviews_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews).HasConstraintName("fk_reviews_product");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.InventoryId).HasName("PRIMARY");

            entity.Property(e => e.LastUpdated)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'in_stock'");

            entity.HasOne(d => d.Product).WithMany(p => p.Stocks).HasConstraintName("fk_inventory_product");

            entity.HasOne(d => d.Store).WithMany(p => p.Stocks).HasConstraintName("fk_inventory_store");
        });

        modelBuilder.Entity<Store>(entity =>
        {
            entity.HasKey(e => e.StoreId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Email).HasDefaultValueSql("_utf8mb4\\'abc123@gmail.com\\'");
            entity.Property(e => e.Status).HasDefaultValueSql("'active'");
            entity.Property(e => e.UpdatedAt)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<View>(entity =>
        {
            entity.HasKey(e => e.ViewId).HasName("PRIMARY");

            entity.Property(e => e.Status).HasDefaultValueSql("'active'");

            entity.HasOne(d => d.Customer).WithMany(p => p.Views).HasConstraintName("fk_views_customer");

            entity.HasOne(d => d.Product).WithMany(p => p.Views).HasConstraintName("fk_views_product");

            entity.HasOne(d => d.Store).WithMany(p => p.Views).HasConstraintName("fk_views_store");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}