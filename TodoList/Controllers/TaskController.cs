using System;
using System.Collections.Generic;
using System.Linq;
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
            return Task.getTaskListJson();
        }

        // POST api/tasks
        [HttpPost("")]
        public void Post(Task task)
        {
            Task.addTask(task);
        }
    }
}
