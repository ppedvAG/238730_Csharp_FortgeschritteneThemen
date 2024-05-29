using Autofac;
using ppedv.CarManager.Data.EfCore;
using ppedv.CarManager.Logic;
using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;
using System.Reflection;

Console.WriteLine("*** CarManager v0.1 ***");

//DI per code
//IRepository repo = new ppedv.CarManager.Data.EfCore.EfRepository();
//CarServices carServices = new CarServices(repo);

//DI per Reflection, doof wegen abhängigkeiten der abhänigkeiten
//var path = @"C:\Users\Fred\source\repos\238730_Csharp_FortgeschritteneThemen\ppedv.CarManager\ppedv.CarManager.Data.EfCore\bin\Debug\net8.0\ppedv.CarManager.Data.EfCore.dll";
//var ass = Assembly.LoadFrom(path);
//var typeMitRepo = ass.GetTypes().FirstOrDefault(x => x.GetInterfaces().Contains(typeof(IRepository)));
//var repo = (IRepository)Activator.CreateInstance(typeMitRepo);
//CarServices carServices = new CarServices(repo);

//DI per AutoFac
var builder = new ContainerBuilder();
//builder.RegisterType<EfRepository>().AsImplementedInterfaces();
builder.RegisterType<EfRepository>().As<IRepository>();
var container = builder.Build();

IRepository repo= container.Resolve<IRepository>();
CarServices carServices = new CarServices(repo);


CreateTestDaten();
var fastestDriver = carServices.GetDriverWithTheFastestCars();

Console.WriteLine($"Fastest Driver:{fastestDriver?.Name}");

void CreateTestDaten()
{
    if (repo.GetAll<Driver>().Count() == 0)
    {
        var testdaten = new List<Driver>
            {
                new Driver
                {
                    Name = "Driver1",
                    Cars = new List<Car>
                    {
                        new Car { Model = "Car1", KW = 150 },
                        new Car {  Model = "Car2", KW = 200 }
                    }
                },
                new Driver
                {
                    Name = "Driver2",
                    Cars = new List<Car>
                    {
                        new Car {  Model = "Car4", KW = 400 }
                    }
                },
                new Driver
                {
                    Name = "Driver3",
                    Cars = new List<Car>
                    {
                        new Car {  Model = "Car5", KW = 500 }
                    }
                }
            };

        foreach (Driver driver in testdaten)
            repo.Add(driver);

        repo.SaveAll();
    }
}


