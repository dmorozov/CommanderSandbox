using Commander.Models;
using Commander.Data;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Commander.Dtos;
using Microsoft.AspNetCore.JsonPatch;

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

        [HttpGet("{id}", Name = nameof(GetCommandById))]
        public ActionResult<CommandReadDto> GetCommandById(int id)
        {
            var command = _repository.GetCommandById(id);

            return command != null ? Ok(_mapper.Map<CommandReadDto>(command)) : NotFound();

        }

        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommand(CommandUpdateDto commandDto)
        {
            Command command = _mapper.Map<Command>(commandDto);
            command = _repository.CreateCommand(command);
            CommandReadDto responsedto = _mapper.Map<CommandReadDto>(command);

            // This way we will send back HTTP 201 and proper url to newly create entity in "Location" header
            // @See "Name" parameter for the HttpGet annotation for the GetCommandById method. This is important!
            return CreatedAtRoute(nameof(GetCommandById), new { Id = responsedto.Id }, responsedto);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandDto)
        {
            Command? command = _repository.GetCommandById(id);
            if (command == null) {
                return NotFound();
            }

            // Update fields from commandDto -> command
            _mapper.Map(commandDto, command);
            _repository.UpdateCommand(command);
            return NoContent();
        }

        /*
            This is require extra packages to make it works:
            Microsoft.AspNetCore.JsonPatch
            Microsoft.AspNetCore.Mvc.NewtonsoftJson -> require setup! (See AddControllers().AddNewtonsoftJson ...)
        */
        [HttpPatch("{id}")]
        public ActionResult PartialUpdateCommand(int id, JsonPatchDocument<CommandUpdateDto> patchDocument)
        {
            Command? command = _repository.GetCommandById(id);
            if (command == null) {
                return NotFound();
            }

            CommandUpdateDto commandDto = _mapper.Map<CommandUpdateDto>(command);
            patchDocument.ApplyTo(commandDto);
            if (!TryValidateModel(commandDto)) {
                return ValidationProblem(ModelState);
            }

            _repository.UpdateCommand(_mapper.Map<Command>(commandDto));

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<CommandReadDto> DeleteCommandById(int id)
        {
            _repository.DeleteCommand(id);
            return Ok(true);

        }
    }
}