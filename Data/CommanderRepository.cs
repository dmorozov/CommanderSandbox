using Commander.Models;

namespace Commander.Data
{
    public class CommanderRepository : ICommanderRepository
    {
        private readonly CommanderContext _context;

        public CommanderRepository(CommanderContext context)
        {
            _context = context;
        }

        public IEnumerable<Command> GetAllCommands()
        {
            return _context.Commands.ToList();
        }

        public Command? GetCommandById(int id)
        {
            return _context.Commands.FirstOrDefault(p => p.Id == id);
        }
    }
}