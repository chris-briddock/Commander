using System;
using Commander.API.Commands.Data;
using Commander.API.Commands.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Commands.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _repo;
        private ILogger _logger;
        public CommandsController(ICommandRepo commandRepo, ILogger<CommandsController> logger)
        {
            _repo = commandRepo;
            _logger = logger;
        }
        [HttpGet]
        [Route("GetCommand/{id}")]
        public ActionResult<Command> GetCommand(Guid Id)
        {
            try
            {
                Command command =  _repo.Read(Id);
                return Ok(command);
            }
            catch
            {
                _logger.LogError("Error in GetCommand, failed to read from the database.");
                return NotFound();

            }
        }
        [HttpGet]
        [Route("GetCommands")]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            try
            {
                IEnumerable<Command> commands = _repo.ReadAll();
                return Ok(commands);
            }
            catch
            {
                _logger.LogError("Error in GetCommands, failed to read from the database.");
                return NoContent();
            }
        }
        [HttpPut]
        [Route("PutCommand")]
        public ActionResult<Command> PutCommand([FromBody] Command command)
        {
            if (command == null)
            {
                return BadRequest();
            }
            _repo.Update(command);
            if (_repo.SaveChanges() == true)
            {
                return Ok();
            }
            else
            {
                _logger.LogError("Error in PutCommand, failed to update the database.");
                return BadRequest();
            }
        }
        [HttpPost]
        [Route("PostCommand")]
        public ActionResult<Command> PostCommand([FromBody] Command command)
        {
            _repo.Create(command);

             var savedChanges = _repo.SaveChangesAsync();

            if (savedChanges.IsCompletedSuccessfully)
            {
                return CreatedAtAction(nameof(GetCommand), new { id = command.Id }, command);
            }
            else
            {
                return BadRequest();
            }
        }
        [Route("DeleteCommand/{id}")]
        public ActionResult<Command> DeleteCommand([FromBody]Guid Id) 
        {
            if (Id == Guid.Empty) 
            {
                return BadRequest();
            }
            _repo.Delete(Id);
            var savedChanges = _repo.SaveChangesAsync();
            if (savedChanges.IsCompletedSuccessfully)
            {
                return NoContent();
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetCommandAsync/{id}")]
        public async Task<ActionResult<Command>> GetCommandAsync(Guid Id)
        {
            try
            {
                Command command =  await _repo.ReadAsync(Id);
                return Ok(command);
            }
            catch
            {
                _logger.LogError("Error in GetCommandAsync, failed to read from the database.");
                return NotFound();

            }
        }
        [HttpGet]
        [Route("GetCommandsAsync")]
         public async Task<ActionResult<Command>> GetCommandsAsync() 
         {
            try
            {
                IEnumerable<Command> commands = await _repo.ReadAllAsync();
                return Ok(commands);
            }
            catch
            {
                _logger.LogError("Error in GetCommandsAsync, failed to read from the database.");
                return NoContent();
            }
         }
         

    }

}
