using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TodoList.Models;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoList.Controllers
{
    [Route("api/tasklists")]
    [ApiController]
    public class TaskListController : ControllerBase
    {
        // GET: api/tasks
        [HttpGet("")]
        public string Get()
        {
            return TaskList.getTaskListsJson();
        }

        [HttpGet("{id}")]
        public string GetListTasks(int id)
        {
            return TaskList.getListTasksJson(id);
        }

        [HttpPost("")]
        public void Post(TaskList taskList)
        {
            TaskList.addTaskList(taskList);
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            TaskList.deleteTaskList(id);
        }

        [HttpPatch("{id}")]
        public void Patch(int id, [FromBody] TaskListPatchRequest body)
        {
            Console.WriteLine(id + " " + body.Name);
            TaskList.EditName(id, body.Name);
        }
    }
}
