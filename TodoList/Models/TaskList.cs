using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public class TaskList
    {
        public TaskList()
        {
        }

        public TaskList(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }

        public static void addTaskList(TaskList taskList)
        {
            Dictionary<int, TaskList> ToDoList = TaskDBService.GetTaskListsFromDB();
            taskList.id = ToDoList.Count > 0 ? ToDoList.Keys.Max() + 1 : 1; //max needs at least 1 element in dictionary
            TaskDBService.InsertDataTaskList(taskList);
        }

        internal static void deleteTaskList(int id)
        {
            TaskDBService.DeleteTaskList(id);
        }

        internal static void EditName(int id, string name)
        {
            TaskDBService.UpdateDataTaskList(id, name);
        }

        public static string getTaskListsJson()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(TaskDBService.GetTaskListsFromDB().Select(d => d.Value), jso);
        }

        public static string getListTasksJson(int id)
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(TaskDBService.GetListTasksFromDB(id).Select(d => d.Value), jso);
        }
        
    }
}
