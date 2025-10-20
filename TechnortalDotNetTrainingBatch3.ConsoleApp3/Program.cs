// See https://aka.ms/new-console-template for more information

using TechnortalDotNetTrainingBatch3.ConsoleApp3;

Console.WriteLine("Hello, World!");

// ADO.NET
ProductService productService = new ProductService();
// productService.Read();
// productService.Create();
// productService.Update();
// productService.Delete();

SaleService saleService = new SaleService();
saleService.Read();
// saleService.Create();