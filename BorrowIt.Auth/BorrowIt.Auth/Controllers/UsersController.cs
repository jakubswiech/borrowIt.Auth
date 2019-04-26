using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Common.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(ICommandDispatcher commandDispatcher, IMapper mapper) : base(commandDispatcher, mapper)
        {
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }
    }
}