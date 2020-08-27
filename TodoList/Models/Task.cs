using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

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

        static Dictionary<int, Task> ToDoList = new Dictionary<int, Task>();

        public static void addTask(Task task)
        {
            task.id = Task.ToDoList.Count > 0 ? Task.ToDoList.Keys.Max() + 1 : 1; //max needs at least 1 element in dictionary
            Task.ToDoList.Add(task.id, task);
        }

        public static string getTaskListJson()
        {
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            return JsonSerializer.Serialize(Task.ToDoList.Select(d => d.Value), jso);
        }
    }
}
