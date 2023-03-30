using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Tutorias_Unphu.Models;

public partial class TutoriasUnphuContext : DbContext
{
    public TutoriasUnphuContext()
    {
    }

    public TutoriasUnphuContext(DbContextOptions<TutoriasUnphuContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asignatura> Asignaturas { get; set; }

    public virtual DbSet<Estatus> Estatuses { get; set; }

    public virtual DbSet<Pensum> Pensums { get; set; }

    public virtual DbSet<ProfAsig> ProfAsigs { get; set; }

    public virtual DbSet<ProfPem> ProfPems { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tutorium> Tutoria { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asignatura>(entity =>
        {
            entity.HasKey(e => e.CodigoAsignatura).HasName("PK__asignatu__9C8EA37AD3BD1843");

            entity.ToTable("asignaturas");

            entity.Property(e => e.CodigoAsignatura)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("codigo_asignatura");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.NombreAsignatura)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre_asignatura");
            entity.Property(e => e.Pemsum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pemsum");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_asignaturas_estatus");

            entity.HasOne(d => d.PemsumNavigation).WithMany(p => p.Asignaturas)
                .HasForeignKey(d => d.Pemsum)
                .HasConstraintName("FK_asignaturas_Pensums1");
        });

        modelBuilder.Entity<Estatus>(entity =>
        {
            entity.HasKey(e => e.Estatus1);

            entity.ToTable("estatus");

            entity.Property(e => e.Estatus1)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
        });

        modelBuilder.Entity<Pensum>(entity =>
        {
            entity.HasKey(e => e.NombrePemsum).HasName("PK_Pensums_1");

            entity.Property(e => e.NombrePemsum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_pemsum");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Pensums)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_Pensums_estatus");
        });

        modelBuilder.Entity<ProfAsig>(entity =>
        {
            entity.ToTable("prof_asig");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Asig)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("asig");
            entity.Property(e => e.Prof)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("prof");

            entity.HasOne(d => d.AsigNavigation).WithMany(p => p.ProfAsigs)
                .HasForeignKey(d => d.Asig)
                .HasConstraintName("FK_prof_asig_asignaturas");

            entity.HasOne(d => d.ProfNavigation).WithMany(p => p.ProfAsigs)
                .HasForeignKey(d => d.Prof)
                .HasConstraintName("FK_prof_asig_profesores");
        });

        modelBuilder.Entity<ProfPem>(entity =>
        {
            entity.ToTable("prof_pems");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Pemsum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pemsum");
            entity.Property(e => e.Profesor)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("profesor");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.ProfPems)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_prof_pems_estatus");

            entity.HasOne(d => d.PemsumNavigation).WithMany(p => p.ProfPems)
                .HasForeignKey(d => d.Pemsum)
                .HasConstraintName("FK_prof_pems_Pensums1");

            entity.HasOne(d => d.ProfesorNavigation).WithMany(p => p.ProfPems)
                .HasForeignKey(d => d.Profesor)
                .HasConstraintName("FK_prof_pems_profesores");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.Matricula).HasName("PK__profesor__30962D147306EA2C");

            entity.ToTable("profesores");

            entity.Property(e => e.Matricula)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_profesores_estatus");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Profesores)
                .HasForeignKey(d => d.Rol)
                .HasConstraintName("FK_profesores_roles");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Rol).HasName("PK__roles__C2B79D2794A38B72");

            entity.ToTable("roles");

            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Roles)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_roles_estatus");
        });

        modelBuilder.Entity<Tutorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tutoria__3213E83FCBB0B47D");

            entity.ToTable("tutoria");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Asignatura)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("asignatura");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Horario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("horario");
            entity.Property(e => e.IdProfesor)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("id_profesor");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("id_usuario");

            entity.HasOne(d => d.AsignaturaNavigation).WithMany(p => p.Tutoria)
                .HasForeignKey(d => d.Asignatura)
                .HasConstraintName("FK_tutoria_asignaturas");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Tutoria)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_tutoria_estatus");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Tutoria)
                .HasForeignKey(d => d.IdProfesor)
                .HasConstraintName("FK_tutoria_profesores");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.Tutoria)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK_tutoria_usuarios");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Matricula).HasName("PK__usuarios__30962D14DC4CD82C");

            entity.ToTable("usuarios");

            entity.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("matricula");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Edad).HasColumnName("edad");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.Estatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estatus");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Password)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Pemsum)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pemsum");
            entity.Property(e => e.Rol)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("rol");

            entity.HasOne(d => d.EstatusNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Estatus)
                .HasConstraintName("FK_usuarios_estatus");

            entity.HasOne(d => d.PemsumNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Pemsum)
                .HasConstraintName("FK_usuarios_Pensums1");

            entity.HasOne(d => d.RolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.Rol)
                .HasConstraintName("FK_usuarios_roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
