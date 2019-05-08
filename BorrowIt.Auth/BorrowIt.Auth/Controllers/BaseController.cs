using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BorrowIt.Common.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace BorrowIt.Auth.Controllers
{
    public abstract class BaseController : ControllerBase
    {
        protected readonly ICommandDispatcher CommandDispatcher;
        protected readonly IQueryDispatcher QueryDispatcher;
        protected readonly IMapper Mapper;

        protected BaseController(ICommandDispatcher commandDispatcher, IMapper mapper, IQueryDispatcher queryDispatcher)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
            QueryDispatcher = queryDispatcher;
        }
    }
}