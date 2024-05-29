using ppedv.CarManager.Logic;
using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;

Console.WriteLine("*** CarManager v0.1 ***");

IRepository repo = new ppedv.CarManager.Data.EfCore.EfRepository();
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


