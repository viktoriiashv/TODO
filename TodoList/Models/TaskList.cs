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
            this.Id = id;
            this.Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public static void AddTaskList(TaskList taskList)
        {
            TaskListDBService.InsertData(taskList);
        }

        internal static void DeleteTaskList(int id)
        {
            Dictionary<int, TaskList> TaskLists = TaskListDBService.GetTaskListsFromDB();
            if (TaskLists.ContainsKey(id))
            {
                TaskListDBService.DeleteData(id);
            }
        }

        public static void EditTaskListName(int id, string name)
        {
            Dictionary<int, TaskList> TaskLists = TaskListDBService.GetTaskListsFromDB();
            if (TaskLists.ContainsKey(id))
            {
                TaskListDBService.UpdateData(id, name);
            }
        }
        public static string GetTaskListsJson()
        {
            return JsonSerializer.Serialize(TaskListDBService.GetTaskListsFromDB().Select(d => d.Value), GetJsonSerializerOptions());
        }

        public static string GetListTasksJson(int id)
        {
            return JsonSerializer.Serialize(TaskListDBService.GetListTasksFromDB(id).Select(d => d.Value), GetJsonSerializerOptions());
        }

        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return jso;
        }

    }
}
