using Commander.Models;

namespace Commander.Data {
    public interface ICommanderRepository {
        IEnumerable<Command> GetAllCommands();

        Command? GetCommandById(int id);

        Command CreateCommand(Command command);

        Command UpdateCommand(Command command);

        void DeleteCommand(int commandId);
    }
}