using Commander.API.Commands.Controllers;
using Commander.API.Commands.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.API.Commands.Data
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
            return Convert.ToBoolean(_context.SaveChanges());
        }
        public void Delete(Guid Id) 
        {
            _context.Remove(Id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await (_context.SaveChangesAsync());
            
        }

        public async Task<IList<Command>> ReadAllAsync()
        {
            return await _context.Commands.ToListAsync();
        }

        public Task<Command> ReadAsync(Guid Id)
        {
            return _context.Commands.Where(c => c.Id == Id).FirstAsync();
        }

        // public void CreateAsync(Command command)
        // {
        //     if (command == null)
        //     {
        //         throw new ArgumentNullException(nameof(command));
        //     }
        //     _context?.Commands?.AddAsync(command);
        // }

        // public void UpdateAsync(Command command)
        // {
        //     if (command == null)
        //     {
        //         throw new ArgumentNullException(nameof(command));
        //     }
        //     _context?.Commands?.Update(command);
        // }

        // public void DeleteAsync(Guid Id)
        // {
        //     _context.Remove(Id);
        // }
    }
}
