using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.MiniBankingDatabase.AppDbContextModels;

public partial class TblDeposit
{
    public int DepositId { get; set; }

    public string MobileNo { get; set; } = null!;

    public decimal DepositAmount { get; set; }

    public DateTime DepositDate { get; set; }

    public virtual TblAccount MobileNoNavigation { get; set; } = null!;
}
