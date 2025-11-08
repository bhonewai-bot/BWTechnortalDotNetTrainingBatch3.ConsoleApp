using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblAccount
{
    public int AccountId { get; set; }

    public string MobileNo { get; set; } = null!;

    public string? CustomerCode { get; set; }

    public string CustomerName { get; set; } = null!;

    public decimal Balance { get; set; }

    public string Pin { get; set; } = null!;

    public virtual ICollection<TblDeposit> TblDeposits { get; set; } = new List<TblDeposit>();

    public virtual ICollection<TblTransactionHistory> TblTransactionHistoryFromMobileNoNavigations { get; set; } = new List<TblTransactionHistory>();

    public virtual ICollection<TblTransactionHistory> TblTransactionHistoryToMobileNoNavigations { get; set; } = new List<TblTransactionHistory>();

    public virtual ICollection<TblWithdrawal> TblWithdrawals { get; set; } = new List<TblWithdrawal>();
}
