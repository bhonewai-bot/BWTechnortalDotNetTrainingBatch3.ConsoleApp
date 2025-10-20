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
// saleService.Read();
// saleService.Create();


// Dapper
ProductDapperService productDapperService = new ProductDapperService();
// productDapperService.Read();
// productDapperService.Create();
// productDapperService.Update();
// productDapperService.Delete();

SaleDapperService saleDapperService = new SaleDapperService();
// saleDapperService.Read();
// saleDapperService.Create();


// EFCore
ProductEFCoreService productEfCoreService = new ProductEFCoreService();
// productEfCoreService.Read();
// productEfCoreService.Create();
// productEfCoreService.Update();
// productEfCoreService.Delete();

SaleEFCoreService saleEfCoreService = new SaleEFCoreService();
saleEfCoreService.Read();
// saleEfCoreService.Create();