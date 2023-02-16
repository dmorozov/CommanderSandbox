using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepository : ICommanderRepository
    {
        public Command CreateCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public void DeleteCommand(int commandId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boild an egg", Line="Boil whater", Platform="Any"},
                new Command{Id=1, HowTo="Boild an egg2", Line="Boil whater2", Platform="Any"},
                new Command{Id=2, HowTo="Boild an egg3", Line="Boil whater3", Platform="Any"}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, HowTo="Boild an egg", Line="Boil whater", Platform="Any"};
        }

        public Command UpdateCommand(Command command)
        {
            throw new NotImplementedException();
        }
    }
}