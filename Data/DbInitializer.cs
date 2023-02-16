using Commander.Models;

namespace Commander.Data
{
    public class DbInitializer
    {
        public static void Initialize(CommanderContext context)
        {
            // Look for any students.
            if (context.Commands.Any())
            {
                return;   // DB has been seeded
            }

            var commands = new Command[]
            {
                new Command{Id=1, HowTo="Boild an egg", Line="Boil whater", Platform="Kitchen"},
                new Command{Id=2, HowTo="Use a restroom", Line="Sit then poop than flush", Platform="Restroom"},
                new Command{Id=3, HowTo="Use a TV", Line="Turn on. Watch and enjoy", Platform="Living Room"},
            };

            context.Commands.AddRange(commands);
            context.SaveChanges();
        }
    }
}