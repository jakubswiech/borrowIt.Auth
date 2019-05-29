using System;
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
            var query = new GetUserQuery() {Id = GetCurrentUserId()};
            var user = await QueryDispatcher.DispatchQueryAsync<UserDto, GetUserQuery>(query);

            return Ok(user);
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            var command = new DeleteUserCommand() {Id = GetCurrentUserId()};
            await CommandDispatcher.DispatchAsync(command);
            return Ok();
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
        {
            command.Id = GetCurrentUserId();
            await CommandDispatcher.DispatchAsync(command);

            return Ok();
        }

        private Guid GetCurrentUserId()
        {
            return new Guid(User.Identity.Name);
        }
    }
}