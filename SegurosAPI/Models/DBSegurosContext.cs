using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SegurosAPI.Models
{
    public partial class DBSegurosContext : DbContext
    {
        public DBSegurosContext()
        {
        }

        public DBSegurosContext(DbContextOptions<DBSegurosContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Seguro> Seguros { get; set; } = null!;
        public virtual DbSet<SegurosCliente> SegurosClientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-4STK30B\\SQLEXPRESS;DataBase=DBSeguros;User Id=as;Password=joguisa98;Integrated Security=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.Property(e => e.Cedula)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NombreCliente)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Telefono)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Seguro>(entity =>
            {
                entity.Property(e => e.CodigoSeguro)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NombreSeguro)
                    .HasMaxLength(100)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Prima).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SumaAsegurada).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<SegurosCliente>(entity =>
            {
                entity.HasIndex(e => new { e.ClienteId, e.SeguroId }, "UQ_AseguradosSeguros")
                    .IsUnique();

                entity.HasOne(d => d.Cliente)
                    .WithMany(p => p.SegurosClientes)
                    .HasForeignKey(d => d.ClienteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SegurosCl__Clien__3C69FB99");

                entity.HasOne(d => d.Seguro)
                    .WithMany(p => p.SegurosClientes)
                    .HasForeignKey(d => d.SeguroId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SegurosCl__Segur__3D5E1FD2");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
