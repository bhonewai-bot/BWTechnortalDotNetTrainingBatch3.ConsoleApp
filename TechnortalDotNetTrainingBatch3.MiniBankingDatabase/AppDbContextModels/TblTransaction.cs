using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public int AccountId { get; set; }

    public TransactionType Type { get; set; }

    public decimal Amount { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual TblAccount Account { get; set; } = null!;
}
