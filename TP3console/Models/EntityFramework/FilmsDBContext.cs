using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace TP3console.Models.EntityFramework
{
    public partial class FilmsDBContext : DbContext
    {
        public FilmsDBContext()
        {
        }

        public FilmsDBContext(DbContextOptions<FilmsDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Avi> Avis { get; set; } = null!;
        public virtual DbSet<Categorie> Categories { get; set; } = null!;
        public virtual DbSet<Film> Films { get; set; } = null!;
        public virtual DbSet<Utilisateur> Utilisateurs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;port=5432;Database=FilmsDB; uid=postgres; password=postgres;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Avi>(entity =>
            {
                entity.HasKey(e => new { e.Film, e.Utilisateur })
                    .HasName("pk_avis");

                entity.ToTable("avis");

                entity.Property(e => e.Film).HasColumnName("film");

                entity.Property(e => e.Utilisateur).HasColumnName("utilisateur");

                entity.Property(e => e.Avis).HasColumnName("avis");

                entity.Property(e => e.Note).HasColumnName("note");

                entity.HasOne(d => d.FilmNavigation)
                    .WithMany(p => p.Avis)
                    .HasForeignKey(d => d.Film)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avis_film");

                entity.HasOne(d => d.UtilisateurNavigation)
                    .WithMany(p => p.Avis)
                    .HasForeignKey(d => d.Utilisateur)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_avis_utilisateur");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.ToTable("categorie");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");
            });

            modelBuilder.Entity<Film>(entity =>
            {
                entity.ToTable("film");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Categorie).HasColumnName("categorie");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Nom)
                    .HasMaxLength(50)
                    .HasColumnName("nom");

                entity.HasOne(d => d.CategorieNavigation)
                    .WithMany(p => p.Films)
                    .HasForeignKey(d => d.Categorie)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_film_categorie");
            });

            modelBuilder.Entity<Utilisateur>(entity =>
            {
                entity.ToTable("utilisateur");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Login)
                    .HasMaxLength(50)
                    .HasColumnName("login");

                entity.Property(e => e.Pwd)
                    .HasMaxLength(50)
                    .HasColumnName("pwd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
