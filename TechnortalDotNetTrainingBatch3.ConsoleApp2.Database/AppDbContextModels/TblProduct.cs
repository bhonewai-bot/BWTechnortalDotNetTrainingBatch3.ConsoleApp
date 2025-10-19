using System;
using System.Collections.Generic;

namespace TechnortalDotNetTrainingBatch3.ConsoleApp2.Database.AppDbContextModels;

public partial class TblProduct
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public bool DeleteFlag { get; set; }
}
