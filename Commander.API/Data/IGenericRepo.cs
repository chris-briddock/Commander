using Commander.API.Models;

namespace Commander.API.Data
{
    public interface IGenericRepo<T> 
    {
        bool SaveChanges();
        IEnumerable<T> ReadAll();
        void Create(T command);
        T Read(int Id);
        void Update(T command);
    }
}