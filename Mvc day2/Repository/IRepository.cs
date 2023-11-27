namespace Mvc_day2.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        void Delete(int id);
        void Create(T obj);
        void Save();
    }
}
