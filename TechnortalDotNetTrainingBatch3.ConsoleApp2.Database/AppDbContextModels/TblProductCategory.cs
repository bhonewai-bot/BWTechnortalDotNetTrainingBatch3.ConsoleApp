using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp2.Database.AppDbContextModels;

public partial class TblProductCategory
{
    public int ProductCategoryId { get; set; }

    public string? ProductCategoryCode { get; set; }

    public string? ProductCategoryName { get; set; }
}
