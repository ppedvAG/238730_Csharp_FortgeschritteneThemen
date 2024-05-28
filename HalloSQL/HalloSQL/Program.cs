using Microsoft.Data.SqlClient;

Console.WriteLine("Hello, World!");

var conString = "Server=(localdb)\\mssqllocaldb;Database=NORTHWND;Trusted_Connection=True;";
var conStringDEINER = "Server=.;Database=Northwind;Trusted_Connection=True;TrustServerCertificate=True";

using var con = new SqlConnection(conString);
con.Open();
Console.WriteLine("DB Connection ok");

using var cmd = new SqlCommand();
{
    cmd.Connection = con;
    cmd.CommandText = "SELECT * FROM Employees";
    using var reader = cmd.ExecuteReader();
    {
        while (reader.Read())
        {
            var vorname = reader.GetString(2);
            var nachname = reader["LastName"].ToString();
            var gebDatum = reader.GetDateTime(reader.GetOrdinal("BirthDate"));
            Console.WriteLine($"{vorname} {nachname} {gebDatum:d}");
        }
    }//reader.DisposeAsync();
}//cmd.Dispose();

using var countCmd = con.CreateCommand();
countCmd.CommandText = "SELECT COUNT(*) FROM Employees";
var count = countCmd.ExecuteScalar();
Console.WriteLine($"{count} Employees");
