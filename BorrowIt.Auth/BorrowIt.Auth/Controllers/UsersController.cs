using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Auth.Application.Commands;
using BorrowIt.Auth.Application.DTOs;
using BorrowIt.Auth.Application.Queries;
using BorrowIt.Common.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController(ICommandDispatcher commandDispatcher, IMapper mapper, IQueryDispatcher queryDispatcher) : base(commandDispatcher, mapper, queryDispatcher)
        {
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> Post([FromBody] SignInQuery query)
        {
            var token = await QueryDispatcher.DispatchQueryAsync<UserSignedInDto, SignInQuery>(query);

            return Ok(token);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("bangla");
        }
    }
}