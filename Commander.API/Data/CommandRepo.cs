using Commander.API.Controllers;
using Commander.API.Models;

namespace Commander.API.Data
{
    public class CommandRepo : IGenericRepo<Command>
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
             _context.Commands?.Add(command);
        }
        public IEnumerable<Command> ReadAll()
        {
             return _context.Commands.ToArray();
        }
        public Command Read(int Id)
        {
            return _context.Commands.Where(c => c.Id == Id).FirstOrDefault();
        }
        public void Update(Command command)
        {
            _context.Commands?.Update(command);
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}