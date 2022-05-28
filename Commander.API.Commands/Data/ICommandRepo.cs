using Commander.API.Commands.Models;

namespace Commander.API.Commands.Data
{
    public interface ICommandRepo 
    {
        bool SaveChanges();
        IEnumerable<Command> ReadAll();
        void Create(Command command);
        Command Read(Guid Id);
        void Update(Command command);
        void Delete(Guid Id);

        Task<int> SaveChangesAsync();
        Task<IList<Command>> ReadAllAsync();
        Task<Command> ReadAsync(Guid Id);
        // Task CreateAsync(Command command);
        // Task UpdateAsync(Command command);
        // Task DeleteAsync(Guid Id);
    }
}