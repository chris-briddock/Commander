using Commander.API.Controllers;
using Commander.API.Models;

namespace Commander.API.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;
        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void Create(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
             _context?.Commands?.Add(command);
        }
        public IEnumerable<Command> ReadAll()
        {
             return _context.Commands.ToArray();
        }
        public Command Read(Guid Id)
        {
            return _context.Commands.Where(c => c.Id == Id).First();
            
        }
        public void Update(Command command)
        {
            _context.Commands?.Update(command);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
        public void Delete(Guid Id) 
        {
            _context.Remove(Id);
        }
    }
}
