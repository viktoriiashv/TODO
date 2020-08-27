using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SQLite;
using TodoList.Controllers;

namespace TodoList.Models
{
    public class Task
    {

        public Task()
        {
        }

        public Task(int id, string name, bool done)
        {
            this.id = id;
            this.name = name;
            this.done = done;
        }

        public int id { get; set; }
        public string name { get; set; }
        public bool done { get; set; }


        public static void addTask(Task task)
        {
            Dictionary<int, Task> ToDoList = TaskDBService.GetDataFromDB();
            task.id = ToDoList.Count > 0 ? ToDoList.Keys.Max() + 1 : 1; //max needs at least 1 element in dictionary
            TaskDBService.InsertData(task);
        }

        public static string getTaskListJson()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(TaskDBService.GetDataFromDB().Select(d => d.Value), jso);
        }
        public static void changeCondition(int id, bool doneCondition)
        {
            Dictionary<int, Task> ToDoList = TaskDBService.GetDataFromDB();
            if (ToDoList.ContainsKey(id))
            {
                TaskDBService.UpdateData(id, doneCondition);
            }
            
        }
    }
}
