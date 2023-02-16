using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public CommanderRepository(CommanderContext context)
        {
            _context = context;
        }

        public Command CreateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            _context.Add(command);
            _context.SaveChanges();
            return command;
        }

        public void DeleteCommand(int commandId)
        {
            Command command  = new Command() { Id = commandId };
            _context.Remove(command);
            _context.SaveChanges();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command? GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }

        public Command UpdateCommand(Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

            _context.Update(command);
            _context.SaveChanges();
            return command;
        }
    }
}