using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DL;

public partial class SistemaNominaContext : DbContext
{
    public SistemaNominaContext()
    {
    }

    public SistemaNominaContext(DbContextOptions<SistemaNominaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Departamento> Departamentos { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<HistorialPermiso> HistorialPermisos { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<StatusPermiso> StatusPermisos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<GetAllEmpleadoDTO> GetAllEmpleadoDTO { get; set; }

    public virtual DbSet<GetAllUsuarioDTO> GetAllUsuarioDTO { get; set; }

    public virtual DbSet<GetAllPermisoDTO> GetAllPermisoDTO { get; set; }

    public virtual DbSet<GetAllHistorialPermisoDTO> GetAllHistorialPermisoDTO { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=SistemaNomina;User ID=sa;Password=pass@word1; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<GetAllHistorialPermisoDTO>(entity =>
        {
            entity.HasNoKey();

        });
        modelBuilder.Entity<GetAllEmpleadoDTO>(entity =>
        {
            entity.HasNoKey();

        });

        modelBuilder.Entity<GetAllUsuarioDTO>(entity =>
        {
            entity.HasNoKey();

        });

        modelBuilder.Entity<GetAllPermisoDTO>(entity =>
        {
            entity.HasNoKey();

        });

        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.HasKey(e => e.IdDepartamento).HasName("PK__Departam__787A433D428D6795");

            entity.ToTable("Departamento");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__CE6D8B9E7AAF9544");

            entity.ToTable("Empleado");

            entity.HasIndex(e => e.Nss, "UQ__Empleado__C7DE920BCD6145CF").IsUnique();

            entity.HasIndex(e => e.Rfc, "UQ__Empleado__CAFFA85EA0FC027C").IsUnique();

            entity.HasIndex(e => e.Curp, "UQ__Empleado__F46C4CBFB9060F55").IsUnique();

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(40)
                .IsUnicode(false);
            entity.Property(e => e.Curp)
                .HasMaxLength(18)
                .IsUnicode(false)
                .HasColumnName("CURP");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Nss)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("NSS");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsUnicode(false)
                .HasColumnName("RFC");
            entity.Property(e => e.SalarioBase).HasColumnType("decimal(7, 2)");

            entity.HasOne(d => d.IdDepartamentoNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdDepartamento)
                .HasConstraintName("FK__Empleado__IdDepa__173876EA");
        });

        modelBuilder.Entity<HistorialPermiso>(entity =>
        {
            entity.HasKey(e => e.IdHistorialPermiso).HasName("PK__Historia__0000E4BFF125E5CC");

            entity.ToTable("HistorialPermiso");

            entity.Property(e => e.Observaciones)
                .HasMaxLength(150)
                .IsUnicode(false);

            entity.HasOne(d => d.AutorizoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.Autorizo)
                .HasConstraintName("FK__Historial__Autor__4BAC3F29");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__Historial__IdPer__49C3F6B7");

            entity.HasOne(d => d.IdStatusPermisoNavigation).WithMany(p => p.HistorialPermisos)
                .HasForeignKey(d => d.IdStatusPermiso)
                .HasConstraintName("FK__Historial__IdSta__4AB81AF0");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permiso__0D626EC88FCEFC4A");

            entity.ToTable("Permiso");
            entity.ToTable(tb => tb.HasTrigger("PermisoUpdateTrigger"));

            entity.Property(e => e.HoraFin).HasPrecision(0);
            entity.Property(e => e.HoraInicio).HasPrecision(0);
            entity.Property(e => e.Motivo)
                .HasMaxLength(200)
                .IsUnicode(false);

            entity.HasOne(d => d.AutorizoNavigation).WithMany(p => p.PermisoAutorizoNavigations)
                .HasForeignKey(d => d.Autorizo)
                .HasConstraintName("FK__Permiso__Autoriz__276EDEB3");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.PermisoIdEmpleadoNavigations)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Permiso__IdEmple__25869641");

            entity.HasOne(d => d.IdStatusPermisoNavigation).WithMany(p => p.Permisos)
                .HasForeignKey(d => d.IdStatusPermiso)
                .HasConstraintName("FK__Permiso__IdStatu__267ABA7A");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Rol__2A49584CE3AC5F97");

            entity.ToTable("Rol");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<StatusPermiso>(entity =>
        {
            entity.HasKey(e => e.IdStatusPermiso).HasName("PK__StatusPe__D8526C076D796C0F");

            entity.ToTable("StatusPermiso");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F265C89B");

            entity.ToTable("Usuario");

            entity.HasIndex(e => e.NombreUsuario, "UQ__Usuario__6B0F5AE058C96BF6").IsUnique();

            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Usuario__IdEmple__1B0907CE");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuario__IdRol__1BFD2C07");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
