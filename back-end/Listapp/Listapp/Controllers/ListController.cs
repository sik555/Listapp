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
using MongoDB.Bson;

namespace Listapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : ControllerBase
    {
        private readonly ListService _listService;
        private readonly List<List> _Lists;

        private readonly ListValidator listvalidator = new ListValidator();

        public ListController(ListService listService)
        {
            _listService = listService;
            _Lists = listService.Get();
        }

        [Route("/error")]
        public IActionResult Error() => Problem();

        [HttpGet]
        public IEnumerable<List> Get()
        {
            return _listService.Get();
        }

        [HttpGet("id", Name = "getList")]
        public ActionResult<List> Get(string id)
        {
            var list = _listService.GetList(id);

            return list;
        }

        [HttpGet("{id}", Name = "getList2")]
        public ActionResult<List> GetList(string id)
        {
            var list = _listService.GetList(id);

            return list;
        }

        [HttpPost]
        public IActionResult Create([FromBody] List list)
        {
            ValidationResult valresult = listvalidator.Validate(list);
            List result;

            if (!valresult.IsValid)
            {
                return BadRequest("Make sure title and description are filled in");
            }

            try
            {
                result = _listService.Create(list);
            }
            catch (Exception e)
            {
                return CreatedAtRoute("returnError", e.ToString());
            }

            return CreatedAtRoute("getList", new { id = list.Id.ToString() });
        }

        [HttpGet("votedlist/{id}", Name = "getVotedList")]
        public IEnumerable<List> GetVotedList(string id)
        {
            var list = _listService.GetVotedList(id);

            return list;
        }

        [HttpPost("{id}", Name = "UpdateList")]
        public void updatelist(string id,List list)
        {
            foreach(Item item in list.Items)
            {
                if(item.Id == "" || item.Id == null)
                {
                    item.Id = ObjectId.GenerateNewId().ToString();
                }
            }
                _listService.Update(id, list);
        }

        [HttpDelete("{id}", Name = "deletelist")]
       public IActionResult Delete(string id)
        {
            var list = _listService.GetList(id);

            if (list == null) return NotFound();

            try
            {
                _listService.Remove(id);
            }catch(Exception e)
            {
                return CreatedAtRoute("returnError", e.ToString());
            }

            return NoContent();
        }
    }
}
