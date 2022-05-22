using System;
using Commander.API.Data;
using Commander.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Controllers
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

        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(Guid Id)
        {
            try
            {
                Command command = _repo.Read(Id);
                return Ok(command);
            }
            catch
            {
                _logger.LogError("Error in GetCommand, failed to read from the database.");
                return NotFound();

            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            IEnumerable<Command> commands;
            try
            {
                commands = _repo.ReadAll();
                return Ok(commands);
            }
            catch
            {
                _logger.LogError("Error in GetCommands, failed to read from the database.");
                return NoContent();
            }
        }
        [HttpPut]
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
        public ActionResult<Command> PostCommand([FromBody] Command command)
        {
            _repo.Create(command);

            if (_repo.SaveChanges() == true)
            {
                return CreatedAtAction(nameof(GetCommand), new { id = command.Id }, command);
            }
            else
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public ActionResult<Command> DeleteCommand([FromBody]Guid Id) 
        {
            if (Id == Guid.Empty) 
            {
                return BadRequest();
            }
            _repo.Delete(Id);

            if (_repo.SaveChanges() == true) 
            {
                return NoContent();  
            }
            else 
            {
                return BadRequest(); 
            }
        }
    }
}
