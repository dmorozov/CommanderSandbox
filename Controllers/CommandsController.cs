using Commander.Models;
using Commander.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Commander.Dtos;

namespace Commander.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommanderRepository _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommanderRepository repository, IMapper mapper) {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetAllCommands()
        {
            var commands = _repository.GetAllCommands();
            // return Ok(commands.Select(cmd => _mapper.Map<CommandReadDto>(cmd)));
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        [HttpGet("{id}")]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            return command != null ? Ok(_mapper.Map<CommandReadDto>(command)) : NotFound();

        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandUpdateDto commandDto) {
            Command command = _mapper.Map<Command>(commandDto);
            command = _repository.CreateCommand(command);
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpPut("{id}")]
        public ActionResult<CommandReadDto> UpdateCommand(int id, CommandUpdateDto commandDto) {
            Command command = _mapper.Map<Command>(commandDto);
            command.Id = id;
            command = _repository.UpdateCommand(command);
            return Ok(_mapper.Map<CommandReadDto>(command));
        }

        [HttpDelete("{id}")]
        public ActionResult<CommandReadDto> DeleteCommandById(int id)
        {
            _repository.DeleteCommand(id);
            return Ok(true);

        }
    }
}