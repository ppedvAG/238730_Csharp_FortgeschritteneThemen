namespace ppedv.CarManager.Model.Contracts
{
    public interface IRepository
    {
        IEnumerable<T> GetAll<T>() where T : Entity;
        T GetById<T>(int id) where T : Entity;
        void Add<T>(T dings) where T : Entity;
        void Update<T>(T dings) where T : Entity;
        void Delete<T>(T dings) where T : Entity;
        void SaveAll();
    }
}
