using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class schooldbContext : DbContext
    {
        public schooldbContext()
        {
        }

        public schooldbContext(DbContextOptions<schooldbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Userlogin> Userlogin { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-70JUDTV;Database=schooldb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Empid)
                    .HasName("PK__employee__AF4CE865924510A6");

                entity.ToTable("employee");

                entity.Property(e => e.Empid)
                    .HasColumnName("empid")
                    .ValueGeneratedNever();

                entity.Property(e => e.Dept)
                    .HasColumnName("dept")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Salary).HasColumnName("salary");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Rollno)
                    .HasName("PK__student__FABA8B5B5C5A19D7");

                entity.ToTable("student");

                entity.Property(e => e.Rollno)
                    .HasColumnName("rollno")
                    .ValueGeneratedNever();

                entity.Property(e => e.Class)
                    .HasColumnName("class")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.School)
                    .HasColumnName("school")
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Userlogin>(entity =>
            {
                entity.HasKey(e => e.Email)
                    .HasName("PK__userlogi__AB6E6165DDAA38D0");

                entity.ToTable("userlogin");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Mobile).HasColumnName("mobile");

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username).HasColumnName("username");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
