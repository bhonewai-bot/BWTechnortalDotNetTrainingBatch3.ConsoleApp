using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblAccount
{
    public int AccountId { get; set; }

    public string AccountName { get; set; } = null!;

    public string PhNumber { get; set; } = null!;

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<TblTransaction> TblTransactions { get; set; } = new List<TblTransaction>();
}
