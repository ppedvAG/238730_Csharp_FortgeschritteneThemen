// See https://aka.ms/new-console-template for more information
using ppedv.CarManager.Data.EfCore;
using ppedv.CarManager.Model;

Console.WriteLine("Hello, World!");

var con = new EfContext();

con.Database.EnsureCreated();

//var car = new Car() { Manufacturer = "Baudi", Model = "911", KW = 18 };
//con.Cars.Add(car);
//con.SaveChanges();

foreach (var c in con.Cars.Where(x => x.KW > 5 && x.Model.StartsWith("9")))
{
    Console.WriteLine($"{c.Manufacturer} {c.Model} KW:{c.KW}");
}

