using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        // GET: api/tasks
        [HttpGet("")]
        public string Get()
        {
            return Task.GetTaskListJson();
        }

        // POST api/tasks
        [HttpPost("")]
        public void Post(Task task)
        {
            Task.AddTask(task);
        }

        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] TaskPatchRequest body)
        {
            Task.ChangeTaskCondition(id, body.Done);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Task.DeleteTask(id);
        }
    }
}
