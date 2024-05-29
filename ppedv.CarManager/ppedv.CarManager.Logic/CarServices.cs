using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;

namespace ppedv.CarManager.Logic
{
    public class CarServices(IRepository repository)
    {
        private readonly IRepository repository = repository;

        public Driver? GetDriverWithTheFastestCars()
        {
            return repository.GetAll<Driver>()
                             .OrderBy(x => x.Cars.Sum(y => y.KW))
                             .FirstOrDefault();
        }
    }
}
