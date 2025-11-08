using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblWithdrawal
{
    public int WithdrawalId { get; set; }

    public string MobileNo { get; set; } = null!;

    public decimal WithdrawalAmount { get; set; }

    public DateTime WithdrawalDate { get; set; }

    public virtual TblAccount MobileNoNavigation { get; set; } = null!;
}
