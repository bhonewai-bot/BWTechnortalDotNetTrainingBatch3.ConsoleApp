// See https://aka.ms/new-console-template for more information

using System.Data;
using Microsoft.Data.SqlClient;
using TechnortalDotNetTrainingBatch3.ConsoleApp2;

Console.WriteLine("Hello, World!");

ProductService productService = new ProductService();
// productService.Read();
// productService.Create();
// productService.Update();
// productService.Delete();

ProductDapperService productDapperService = new ProductDapperService();
// productDapperService.Read();
// productDapperService.Create();
// productDapperService.Update();
productDapperService.Delete();