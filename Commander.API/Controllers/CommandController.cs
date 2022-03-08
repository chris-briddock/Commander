using System;
using Commander.API.Data;
using Commander.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Commander.API.Controllers
{
    public class CommandsController : ControllerBase
    {
        private readonly IGenericRepo<Command> _repo;
        public CommandsController(IGenericRepo<Command> commandRepo)
        {
            _repo = commandRepo;
        }
        
        [HttpGet("{id}")]
        public ActionResult<Command> GetCommand(int Id) 
        {
            var command = _repo.Read(Id);
            return Ok(command);
        }
        [HttpGet]
        public ActionResult<IEnumerable<Command>> GetCommands()
        {
            var commands = _repo.ReadAll();
            return Ok(commands);
        }
        [HttpPut]
        public ActionResult<Command> PutCommand([FromBody]Command command)
        {
            if (command == null)
            {
                throw new ArgumentNullException();
            }

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
