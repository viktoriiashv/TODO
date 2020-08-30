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
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public int TaskListId { get; set; }

        public Task()
        {
        }

        public Task(int id, string name, bool done, int taskListId)
        {
            this.Id = id;
            this.Name = name;
            this.Done = done;
            this.TaskListId = taskListId;
        }

        public static void AddTask(Task task)
        {
            TaskDBService.InsertData(task);
        }

        public static string GetTaskListJson()
        {
            return JsonSerializer.Serialize(TaskDBService.GetDataFromDB().Select(d => d.Value), getJsonSerializerOptions());
        }
        public static void ChangeTaskCondition(int id, bool doneCondition)
        {
            Dictionary<int, Task> ToDoList = TaskDBService.GetDataFromDB();
            if (ToDoList.ContainsKey(id))
            {
                TaskDBService.UpdateData(id, doneCondition);
            }

        }
        public static void ChangeTaskName(int id, string name)
        {
            Dictionary<int, Task> ToDoList = TaskDBService.GetDataFromDB();
            if (ToDoList.ContainsKey(id))
            {
                if(name != "" && name != null)
                {
                    TaskDBService.UpdateDataName(id, name);
                }
            }

        }
        public static void DeleteTask(int id)
        {
            Dictionary<int, Task> ToDoList = TaskDBService.GetDataFromDB();
            if (ToDoList.ContainsKey(id))
            {
                TaskDBService.DeleteData(id);
            }
        }
        public static JsonSerializerOptions getJsonSerializerOptions()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return jso;
        }
    }
}