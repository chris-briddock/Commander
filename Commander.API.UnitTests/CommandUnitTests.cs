using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Commander.API.Models;
using Commander.API.Data;
using Commander.API.Controllers;
using Xunit;
using Moq;

namespace Commander.API.UnitTests
{
    
    public class CommandUnitTests
    {
        private readonly Mock<ICommandRepo> _commandRepo;
        private readonly Mock<NullLogger<CommandsController>> _loggger;
        private Guid _guid = Guid.Parse("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200");

        public CommandUnitTests()
        {
            _commandRepo = new Mock<ICommandRepo>();
            _loggger = new Mock<NullLogger<CommandsController>>();
        }
        [Fact]
        public void GetAllShouldReturn200Ok()
        {
            // arrange   
            _commandRepo.Setup(x => x.ReadAll());
            var controller = new CommandsController(_commandRepo.Object,_loggger.Object);

            // act
            var actionResult = controller.GetCommands();
            var result = actionResult.Result as OkObjectResult;

            //assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void GetShouldReturn200Ok()
        {
            //arrange
            Guid guid = GetMockData().First().Id;
            _commandRepo.Setup(c => c.Read(guid));
            var controller = new CommandsController(_commandRepo.Object, _loggger.Object);

            // act
            var actionResult = controller.GetCommand(guid);
            var result = actionResult.Result as OkObjectResult;

            // assert
            Assert.IsType<OkObjectResult>(result);

        }
        [Fact]
        public void PutShouldReturn200Ok()
        {
            // arrange
            Command commandToUpdate = GetMockData()
                                                .Where(x => x.Id == _guid)
                                                .First();
            string commandStringBeforeUpdate = commandToUpdate.CommandString;
            _commandRepo.Setup(x => x.Update(commandToUpdate));
            _commandRepo.Setup(x => x.SaveChanges()).Returns(true);
            var controller = new CommandsController(_commandRepo.Object, _loggger.Object);

            // act
            commandToUpdate.CommandString = "Sample Command Updated";
            var actionResult = controller.PutCommand(commandToUpdate);
            var result = actionResult.Result as OkResult ;

            // assert
            Assert.NotEqual(commandToUpdate.CommandString, commandStringBeforeUpdate);
            Assert.IsType<OkResult>(result);


        }
        [Fact]
        public void PostShouldReturn201CreatedAtAction()
        {
            // arrange
            Command newCommand = new Command()
            {
                Id = Guid.NewGuid(),
                OperatingSystem = "Sample OS",
                CommandString = "Sample Text",
                Parameters = "Sample Text",
                ParametersSummary = "Sample Summary",
                RuntimeEnvironment = "Sample Runtime Environment"
            };
            _commandRepo.Setup(x => x.Create(newCommand));
            _commandRepo.Setup(x => x.SaveChanges()).Returns(true);
            // act
            var controller = new CommandsController(_commandRepo.Object, _loggger.Object);
            var actionResult = controller.PostCommand(newCommand);
            var result = actionResult.Result as CreatedAtActionResult;
            // assert
            Assert.IsType<CreatedAtActionResult>(result);

        }
        [Fact]
        public void DeleteShouldReturn200Ok()
        {
            // arrange
            _commandRepo.Setup(x => x.Delete(_guid));
            _commandRepo.Setup(x => x.SaveChanges()).Returns(true);
            var controller = new CommandsController(_commandRepo.Object, _loggger.Object);
            
            // act
            var actionResult = controller.DeleteCommand(_guid);
            var result = actionResult.Result as NoContentResult;

            //assert
            Assert.IsType<NoContentResult>(result);
        }
        public IEnumerable<Command> GetMockData()
        {
            
            IEnumerable<Command> _commands = new List<Command>()
            {
                new Command
                {
                    Id = Guid.NewGuid(),
                    CommandString = "Sample Command",
                    OperatingSystem = "Sample OS",
                    RuntimeEnvironment = "Sample Runtime",
                    Parameters = "Sample Params",
                    ParametersSummary = "Sample Summary"
                },

                new Command
                {
                    Id = _guid,
                    CommandString = "Sample Command",
                    OperatingSystem = "Sample OS",
                    RuntimeEnvironment = "Sample Runtime",
                    Parameters = "Sample Params",
                    ParametersSummary = "Sample Summary"
                }
            };
            return _commands;
        }
    }
}
