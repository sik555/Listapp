using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Listapp.Models;
using Listapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Listapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController (UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userService.Get();
        }

        [HttpGet("id", Name ="GetUser")]
        public ActionResult<User> Get(string id)
        {
            var user = _userService.Get(id);

            return user;
        }
    }
}
