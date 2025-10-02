// See https://aka.ms/new-console-template for more information

using System.Data;
using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

/*SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "MiniPOS";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();*/

/*string query = @"SELECT [ProductId]
    ,[ProductName]
    ,[Quantity]
    ,[Price]
    ,[DeleteFlag]
FROM [dbo].[Tbl_Product]";

SqlCommand cmd = new SqlCommand(query, connection);

SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt); */

/*
string query = @"SELECT
    ProductId,
    ProductName,
    Quantity,
    Price,
    DeleteFlag
FROM Tbl_Product";

SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

for (int i = 0; i < dt.Rows.Count; i++)
{
    DataRow row = dt.Rows[i];
    int rowNo = i + 1;
    decimal price = Convert.ToDecimal(row["Price"]);
    Console.WriteLine($"{rowNo.ToString()}. {row["ProductName"]} ({price.ToString("N0")})");
}

connection.Close();
*/

SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
sqlConnectionStringBuilder.DataSource = ".";
sqlConnectionStringBuilder.InitialCatalog = "MiniPOS";
sqlConnectionStringBuilder.UserID = "sa";
sqlConnectionStringBuilder.Password = "sasa@123";
sqlConnectionStringBuilder.TrustServerCertificate = true;

SqlConnection connection = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
connection.Open();

string query = @"SELECT
    ProductId,
    ProductName,
    Quantity,
    Price,
    DeleteFlag
FROM Tbl_Product";

SqlCommand cmd = new SqlCommand(query, connection);
SqlDataAdapter adapter = new SqlDataAdapter(cmd);
DataTable dt = new DataTable();
adapter.Fill(dt);

for (int i = 0; i < dt.Rows.Count; i++)
{
    DataRow row = dt.Rows[i];
    decimal price = Convert.ToDecimal(row["Price"]);
    Console.WriteLine($"{row["ProductId"]}. {row["ProductName"]} ({price.ToString("N0")})");
}

connection.Close();