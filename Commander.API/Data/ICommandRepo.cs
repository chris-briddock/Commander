using Commander.API.Models;

namespace Commander.API.Data
{
    public interface ICommandRepo 
    {
        bool SaveChanges();
        IEnumerable<Command> ReadAll();
        void Create(Command command);
        Command Read(Guid Id);
        void Update(Command command);
        void Delete(Guid Id);
    }
}