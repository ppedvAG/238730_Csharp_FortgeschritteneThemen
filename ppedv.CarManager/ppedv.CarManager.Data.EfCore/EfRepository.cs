using ppedv.CarManager.Model;
using ppedv.CarManager.Model.Contracts;

namespace ppedv.CarManager.Data.EfCore
{
    public class EfRepository : IRepository
    {
        EfContext context = new EfContext();

        public void Add<T>(T dings) where T : Entity
        {
            context.Set<T>().Add(dings);
        }

        public void Delete<T>(T dings) where T : Entity
        {
            context.Set<T>().Remove(dings);
        }

        public IEnumerable<T> GetAll<T>() where T : Entity
        {
            return context.Set<T>().ToList();
        }

        public T GetById<T>(int id) where T : Entity
        {
            return context.Set<T>().Find(id);
        }

        public void SaveAll()
        {
            context.SaveChanges();
        }

        public void Update<T>(T dings) where T : Entity
        {
            context.Set<T>().Update(dings);
        }
    }
}
