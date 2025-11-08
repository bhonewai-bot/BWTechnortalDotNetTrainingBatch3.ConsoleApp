using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAccount> TblAccounts { get; set; }

    public virtual DbSet<TblDeposit> TblDeposits { get; set; }

    public virtual DbSet<TblTransactionHistory> TblTransactionHistories { get; set; }

    public virtual DbSet<TblWithdrawal> TblWithdrawals { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=MiniBanking;User ID=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Tbl_Acco__349DA5A60CEDFC9E");

            entity.ToTable("Tbl_Accounts");

            entity.HasIndex(e => e.MobileNo, "UQ__Tbl_Acco__D6D73A86D027429B").IsUnique();

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.CustomerCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasComputedColumnSql("(right('000000'+CONVERT([varchar],[AccountId]),(6)))", true);
            entity.Property(e => e.CustomerName).HasMaxLength(100);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblDeposit>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK__Tbl_Depo__AB60DF714EEBF4C0");

            entity.ToTable("Tbl_Deposits");

            entity.Property(e => e.DepositAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.DepositDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);

            entity.HasOne(d => d.MobileNoNavigation).WithMany(p => p.TblDeposits)
                .HasPrincipalKey(p => p.MobileNo)
                .HasForeignKey(d => d.MobileNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Depos__Mobil__412EB0B6");
        });

        modelBuilder.Entity<TblTransactionHistory>(entity =>
        {
            entity.HasKey(e => e.TransactionHistoryId).HasName("PK__Tbl_Tran__599D20B2E8C2DC7D");

            entity.ToTable("Tbl_TransactionHistory");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FromMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ToMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.FromMobileNoNavigation).WithMany(p => p.TblTransactionHistoryFromMobileNoNavigations)
                .HasPrincipalKey(p => p.MobileNo)
                .HasForeignKey(d => d.FromMobileNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Trans__FromM__3C69FB99");

            entity.HasOne(d => d.ToMobileNoNavigation).WithMany(p => p.TblTransactionHistoryToMobileNoNavigations)
                .HasPrincipalKey(p => p.MobileNo)
                .HasForeignKey(d => d.ToMobileNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Trans__ToMob__3D5E1FD2");
        });

        modelBuilder.Entity<TblWithdrawal>(entity =>
        {
            entity.HasKey(e => e.WithdrawalId).HasName("PK__Tbl_With__7C842C6EEAB7268A");

            entity.ToTable("Tbl_Withdrawals");

            entity.Property(e => e.MobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.WithdrawalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.WithdrawalDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.MobileNoNavigation).WithMany(p => p.TblWithdrawals)
                .HasPrincipalKey(p => p.MobileNo)
                .HasForeignKey(d => d.MobileNo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tbl_Withd__Mobil__44FF419A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
