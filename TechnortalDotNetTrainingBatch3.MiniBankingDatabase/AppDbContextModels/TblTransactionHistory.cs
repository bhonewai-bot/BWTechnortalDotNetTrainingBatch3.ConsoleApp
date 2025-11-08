using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblTransactionHistory
{
    public int TransactionHistoryId { get; set; }

    public string FromMobileNo { get; set; } = null!;

    public string ToMobileNo { get; set; } = null!;

    public decimal Amount { get; set; }

    public DateTime TransactionDate { get; set; }

    public virtual TblAccount FromMobileNoNavigation { get; set; } = null!;

    public virtual TblAccount ToMobileNoNavigation { get; set; } = null!;
}
