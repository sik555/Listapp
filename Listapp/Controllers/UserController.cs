using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using Listapp.Models;
using Listapp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Listapp.Models.errors;

namespace Listapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly List<User> _Users;

        private readonly UserValidator uservalidator = new UserValidator();

        public UserController (UserService userService)
        {
            _userService = userService;
            _Users = userService.Get();
        }

        [Route("/error")]
        public IActionResult Error() => Problem();

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

        [HttpPost("username", Name = "LoginUser")]
        public ActionResult<User> Get( [FromBody] loginUser loginUser)
        {
            var user = _userService.Get(loginUser.Username,loginUser.Password);
            if(user == null)
            {
                return BadRequest("user does not exist make sure your username and password is correct");
            }
            return user;
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            ValidationResult valresult = uservalidator.Validate(user);

            User foundUser = _Users.Find(x => x.email == user.email || x.Username == user.Username);

            if (!valresult.IsValid)
            {
                return BadRequest("make sure your email is a valid email and your password is atleast 6 characters long");
            }

            if (foundUser != null)
            {
                return BadRequest("user already exists");
            }

            try
            {
                var reesult = _userService.Create(user);
            }catch(Exception e)
            {
                return CreatedAtRoute("returnError", e.ToString());
            }

            return CreatedAtRoute("GetUser", new { id = user.Id.ToString()});
        }

        //[HttpOptions]
        //public ActionResult<User> CreateUser([FromBody] User user)
        //{
        //    ValidationResult valresult = uservalidator.Validate(user);
        //    User foundUser = _Users.Find(x => x.email == user.email || x.Username == user.Username);

        //    if (!valresult.IsValid)
        //    {
        //        return CreatedAtRoute("returnError","make sure your email is a valid email and your password is atleast 6 long" );
        //    }

        //    if(foundUser != null)
        //    {
        //        return CreatedAtRoute("returnError", "this user already exists");
        //    }
        //    try
        //    {
        //        var reesult = _userService.Create(user);
        //    }
        //    catch (Exception e)
        //    {
        //        return CreatedAtRoute("returnError", e.ToString());
        //    }

        //    return CreatedAtRoute("GetUser", new { id = user.Id.ToString() });
        //}

        [HttpPost("{id:length(24)}")]
        public IActionResult Update ([FromBody] User user)
        {
            ValidationResult valresult = uservalidator.Validate(user);
            var u = _userService.Get(user.Id);
            if (!ModelState.IsValid)
            {
                return NoContent();
            }
            if (u == null)
            {
                return NotFound();
            }

            try
            {
                _userService.Update(user.Id, user);

            }catch(Exception e)
            {
                return CreatedAtRoute("ReturnError", e.ToString()); 
            }

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var user = _userService.Get(id);

            if (user == null) return NotFound();

            try
            {
                _userService.Remove(id);
            }catch(Exception e)
            {
                return CreatedAtRoute("returnError", e.ToString());
            }

            return NoContent();
        }
    }
}
