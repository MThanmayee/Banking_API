using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace BankingProject.Models
{
    public partial class BankingProjectContext : DbContext
    {
        public BankingProjectContext()
        {
        }

        public BankingProjectContext(DbContextOptions<BankingProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<Admin> Admin { get; set; }
        public virtual DbSet<Bank> Bank { get; set; }
        public virtual DbSet<BankMaster> BankMaster { get; set; }
        public virtual DbSet<Benificiaries> Benificiaries { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-70JUDTV;Database=BankingProject;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.AccountNumber)
                    .HasName("PK__Account__BE2ACD6EF0EF1287");

                entity.Property(e => e.AccountNumber).ValueGeneratedNever();

                entity.Property(e => e.AccountType).HasMaxLength(10);

                entity.Property(e => e.Balance).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Otp)
                    .HasColumnName("OTP")
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(20);

                entity.Property(e => e.TransactionPassword).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(30);

                entity.HasOne(d => d.EmailNavigation)
                    .WithMany(p => p.Account)
                    .HasForeignKey(d => d.Email)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Account__Email__412EB0B6");
            });

            modelBuilder.Entity<Admin>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Password).HasMaxLength(15);
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.HasKey(e => e.Ifsccode)
                    .HasName("PK__Bank__6C74377E47D75F5A");

                entity.Property(e => e.Ifsccode)
                    .HasColumnName("IFSCCode")
                    .HasMaxLength(15);

                entity.Property(e => e.BranchName).HasMaxLength(50);

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Bank)
                    .HasForeignKey(d => d.Id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Bank__ID__398D8EEE");
            });

            modelBuilder.Entity<BankMaster>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.BankName).HasMaxLength(30);
            });

            modelBuilder.Entity<Benificiaries>(entity =>
            {
                entity.HasKey(e => e.ToAccount)
                    .HasName("PK__Benifici__BABEF0BDD74DD5EF");

                entity.Property(e => e.ToAccount).ValueGeneratedNever();

                entity.Property(e => e.BenificiaryName).HasMaxLength(30);

                entity.Property(e => e.Ifsccode)
                    .HasColumnName("IFSCCode")
                    .HasMaxLength(15);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.HasOne(d => d.FromAccountNavigation)
                    .WithMany(p => p.Benificiaries)
                    .HasForeignKey(d => d.FromAccount)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Benificia__FromA__440B1D61");

                entity.HasOne(d => d.IfsccodeNavigation)
                    .WithMany(p => p.Benificiaries)
                    .HasForeignKey(d => d.Ifsccode)
                    .HasConstraintName("FK__Benificia__IFSCC__44FF419A");
            });

            modelBuilder.Entity<Transactions>(entity =>
            {
                entity.HasKey(e => e.TransactionId)
                    .HasName("PK__Transact__55433A4B299C1B57");

                entity.Property(e => e.TransactionId)
                    .HasColumnName("TransactionID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Amount).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.ClosingBalance).HasColumnType("numeric(7, 2)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MaturityInstructions).HasMaxLength(30);

                entity.Property(e => e.Remarks).HasMaxLength(30);

                entity.Property(e => e.ToClosingBalance).HasColumnType("numeric(7, 2)");

                entity.Property(e => e.TransactionType).HasMaxLength(10);

                entity.HasOne(d => d.FromAccountNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.FromAccount)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Transacti__FromA__5629CD9C");

                entity.HasOne(d => d.ToAccountNavigation)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.ToAccount)
                    .HasConstraintName("FK__Transacti__ToAcc__571DF1D5");
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.EmailId)
                    .HasName("PK__UserProf__7ED91AEF62A4B632");

                entity.Property(e => e.EmailId)
                    .HasColumnName("EmailID")
                    .HasMaxLength(30);

                entity.Property(e => e.Aadhar).HasMaxLength(12);

                entity.Property(e => e.AccountStatus).HasMaxLength(10);

                entity.Property(e => e.BranchIfsc)
                    .HasColumnName("BranchIFSC")
                    .HasMaxLength(15);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.FatherName).HasMaxLength(50);

                entity.Property(e => e.FirstName).HasMaxLength(15);

                entity.Property(e => e.GrossAnnualIncome).HasColumnType("numeric(10, 2)");

                entity.Property(e => e.LastName).HasMaxLength(15);

                entity.Property(e => e.MiddleName).HasMaxLength(15);

                entity.Property(e => e.OccupationType).HasMaxLength(30);

                entity.Property(e => e.PerAddressLine1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerAddressLine2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PerCity).HasMaxLength(30);

                entity.Property(e => e.PerLandmark)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.PerState).HasMaxLength(15);

                entity.Property(e => e.ResAddressLine1)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResAddressLine2)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ResCity).HasMaxLength(30);

                entity.Property(e => e.ResLandmark)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ResState).HasMaxLength(15);

                entity.Property(e => e.SourceOfIncome).HasMaxLength(30);

                entity.Property(e => e.Title).HasMaxLength(3);

                entity.HasOne(d => d.BranchIfscNavigation)
                    .WithMany(p => p.UserProfile)
                    .HasForeignKey(d => d.BranchIfsc)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__UserProfi__Branc__3E52440B");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
