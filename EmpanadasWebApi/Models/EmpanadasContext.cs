using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace EmpanadasWebApi.Models
{
    public partial class EmpanadasContext : DbContext
    {
        public EmpanadasContext()
        {
        }

        public EmpanadasContext(DbContextOptions<EmpanadasContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DetallePedido> DetallePedidos { get; set; }
        public virtual DbSet<EstadosPedido> EstadosPedidos { get; set; }
        public virtual DbSet<Gusto> Gustos { get; set; }
        public virtual DbSet<MediosDePago> MediosDePagos { get; set; }
        public virtual DbSet<Pedido> Pedidos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-NFHI0EI\\SQLEXPRESS;Database=Empanadas;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.HasKey(e => e.IdDetallePedido);

                entity.Property(e => e.IdDetallePedido).HasColumnName("idDetallePedido");

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.IdGusto).HasColumnName("idGusto");

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.Total).HasColumnName("total");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.Cascade) 
                    .HasConstraintName("FK_DetallePedidos_Pedidos");
            });

            modelBuilder.Entity<EstadosPedido>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.Property(e => e.IdEstado).HasColumnName("idEstado");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Gusto>(entity =>
            {
                entity.HasKey(e => e.IdGusto);

                entity.Property(e => e.IdGusto).HasColumnName("idGusto");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<MediosDePago>(entity =>
            {
                entity.HasKey(e => e.IdMedioPago);

                entity.ToTable("MediosDePago");

                entity.Property(e => e.IdMedioPago).HasColumnName("idMedioPago");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.Property(e => e.IdPedido).HasColumnName("idPedido");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("direccion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Fecha)
                    .HasColumnType("datetime")
                    .HasColumnName("fecha");

                entity.Property(e => e.ImportePedido).HasColumnName("importePedido");

                entity.Property(e => e.MedioPago).HasColumnName("medioPago");

                entity.Property(e => e.NombreYapellido)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombreYApellido");

                entity.Property(e => e.TotalEmpanadas).HasColumnName("totalEmpanadas");

                entity.HasOne(d => d.EstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_EstadosPedidos");

                entity.HasOne(d => d.MedioPagoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.MedioPago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_MediosDePago");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
