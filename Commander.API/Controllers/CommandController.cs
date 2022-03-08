using System;
using Commander.API.Data;
using Commander.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Controllers
{
    public class CommandsController : ControllerBase
    {
        private readonly IGenericRepo<Command> _repo;
        private ILogger _logger;
        public CommandsController(IGenericRepo<Command> commandRepo, ILogger logger)
        {
            _repo = commandRepo;
            _logger = logger;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(int Id) 
        {
            Command command = (Command)null;
            try 
            {
                 command = _repo.Read(Id);
                 return Ok(command);
            }
            catch 
            {
                _logger.LogError("Error in GetCommand, failed to read from the database.");
                return NoContent();
                
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
        public ActionResult<Command> PutCommand([FromBody]Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }
            try 
            {
                _repo.Update(command);

                if (_repo.SaveChanges() == true)
                {
                    return Ok();
                }
                else
                {
                    return NoContent();
                }
            }
            catch  
            {
                _logger.LogError("Error PutCommand, failed to update the database.");
                return NoContent();
            }
        }     
        [HttpPost]
        public ActionResult<Command> PostCommand([FromBody]Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                _repo.Create(command);
            }

            if (_repo.SaveChanges() == true)
            {
                return Ok();
            }
            else
            {
                return NoContent();
            }
        }
    }
}
