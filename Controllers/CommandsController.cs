using Commander.Models;
using Commander.Data;
using Microsoft.AspNetCore.Mvc;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;

        public CommandsController(ICommanderRepository repository) {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();

            return Ok(commands);
        }

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            return command != null ? Ok(command) : NotFound();

        }
    }
}