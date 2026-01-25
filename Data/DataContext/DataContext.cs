using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.DataContext;

public partial class DataContext : DbContext
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TodoItem> TodoItems { get; set; }

    public virtual DbSet<TodoMasterStatus> TodoMasterStatuses { get; set; }

    public virtual DbSet<TodoUser> TodoUsers { get; set; }
    
    public virtual DbSet<ProductManagement> ProductManagements { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TIG3R\\SQLEXPRESS;Database=TODO_LIST;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.ToTable("TODO_ITEMS");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Owner)
                .HasMaxLength(5)
                .HasColumnName("owner");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
        });

        modelBuilder.Entity<TodoMasterStatus>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("TODO_MASTER_STATUS");

            entity.Property(e => e.StatusDesc)
                .HasMaxLength(50)
                .HasColumnName("status_desc");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
        });

        modelBuilder.Entity<TodoUser>(entity =>
        {
            entity.HasKey(e => e.UserId);

            entity.ToTable("TODO_USER");

            entity.Property(e => e.UserId)
                .HasMaxLength(5)
                .HasColumnName("user_id");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .HasColumnName("password");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("user_name");
        });

        modelBuilder.Entity<ProductManagement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_ProductManagement_1");

            entity.ToTable("ProductManagement");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.ProductName).HasMaxLength(50);
            entity.Property(e => e.Sku)
                .HasMaxLength(10)
                .HasColumnName("SKU");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
