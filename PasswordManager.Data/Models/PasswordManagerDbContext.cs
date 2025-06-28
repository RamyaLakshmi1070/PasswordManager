using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace PasswordManager.Data.Models;

public partial class PasswordManagerDbContext : DbContext
{
    public PasswordManagerDbContext()
    {
    }

    public PasswordManagerDbContext(DbContextOptions<PasswordManagerDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Password> Passwords { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        
        if (!optionsBuilder.IsConfigured)
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json");
            var config = builder.Build();//have ascension to json
            var connectionString = config.GetConnectionString("QuickKartDBConnectionString");

            // #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
            optionsBuilder.UseSqlServer(connectionString);
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Password>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Password__3214EC070B7C8149");

            entity.Property(e => e.App).HasMaxLength(100);
            entity.Property(e => e.Category).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(150);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
