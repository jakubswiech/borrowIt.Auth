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
        protected readonly IMapper Mapper;

        protected BaseController(ICommandDispatcher commandDispatcher, IMapper mapper)
        {
            CommandDispatcher = commandDispatcher;
            Mapper = mapper;
        }
    }
}