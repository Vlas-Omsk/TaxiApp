﻿using Microsoft.EntityFrameworkCore;
using TaxiApp.DAL.Entities;
using TaxiApp.DataTypes;

namespace TaxiApp.DAL
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.HasKey(e => e.Login).HasName("PK_UserLogin");

                entity.ToTable("User");

                entity.Property(e => e.Login)
                    .HasMaxLength(40);
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(32);
                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<TariffEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_TariffId");

                entity.ToTable("Tariff");

                entity.Property(e => e.Name)
                    .HasMaxLength(40)
                    .IsRequired();
                entity.Property(e => e.StartingPrice)
                    .HasColumnType("money")
                    .IsRequired();
            });

            modelBuilder.Entity<DriverEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_DriverId");

                entity.ToTable("Driver");

                entity.Property(e => e.LastName)
                    .HasMaxLength(40)
                    .IsRequired();
                entity.Property(e => e.FirstName)
                    .HasMaxLength(40)
                    .IsRequired();
                entity.Property(e => e.Patronymic)
                    .IsRequired();
                entity.Property(e => e.TariffId)
                    .IsRequired();
                entity.Property(e => e.CarId)
                    .IsRequired();
                entity.Property(e => e.State)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(40)
                    .HasDefaultValue(DriverState.Inactive);

                entity.HasOne(d => d.Tariff).WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.TariffId)
                    .HasConstraintName("FK_DriverTariffId_TariffId");

                entity.HasOne(d => d.Car).WithMany(p => p.Drivers)
                    .HasForeignKey(d => d.CarId)
                    .HasConstraintName("FK_DriverCarId_CarId");
            });

            modelBuilder.Entity<CarEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_CarId");

                entity.ToTable("Car");

                entity.Property(e => e.Brand)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.Property(e => e.Number)
                    .HasMaxLength(20)
                    .IsRequired();
                entity.Property(e => e.Color)
                    .HasMaxLength(40)
                    .IsRequired();
            });

            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_OrderId");

                entity.ToTable("Order");

                entity.Property(e => e.DriverId)
                    .IsRequired();
                entity.Property(e => e.AddressFrom)
                    .IsRequired();
                entity.Property(e => e.AddressTo)
                    .IsRequired();
                entity.Property(e => e.Cost)
                    .HasColumnType("money")
                    .IsRequired();
                entity.Property(e => e.CreatedAt)
                    .HasColumnType("date")
                    .IsRequired();
                entity.Property(e => e.CompleatedAt)
                    .HasColumnType("date");
                entity.Property(e => e.ClientId)
                    .IsRequired();

                entity.HasOne(d => d.Driver).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK_OrderDriverId_DriverId");

                entity.HasOne(d => d.Client).WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_OrderClientId_ClientId");
            });

            modelBuilder.Entity<ClientEntity>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_ClientId");

                entity.ToTable("Client");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsRequired();
                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsRequired();
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
                return;

            // For designer
            optionsBuilder.UseSqlServer();
        }
    }
}