using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using TodoList.Models;


namespace TodoList.Controllers
{
    public class TaskListDBService
    {
        public static void CreateTable()
        {
            string command = "CREATE TABLE IF NOT EXISTS ToDo (ID INT(255), Name VARCHAR(20), Done TINYINT(1))";
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }

        }
        public static void DeleteData(int id)
        {
            string command = "DELETE FROM Tasklists WHERE ID = " + id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
            command = "DELETE FROM Tasks WHERE List_ID = " + id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void UpdateData(int id, string name)
        {
            string command = "UPDATE Tasklists SET Name = '" + name + "' WHERE ID = " + id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void InsertData(TaskList taskList)
        {
            string command = "INSERT INTO Tasklists (Name) VALUES('" + taskList.Name + "')";
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static Dictionary<int, TaskList> GetTaskListsFromDB()
        {
            string command = "SELECT * FROM Tasklists";
            Dictionary<int, TaskList> todos = new Dictionary<int, TaskList>();
            using (SqliteConnection connection = DBService.DBConnection())
            {
                using (SqliteDataReader reader = DBService.ExecuteCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(reader.GetInt32(0), new TaskList(reader.GetInt32(0), reader.GetString(1)));
                    }
                }
            }
            return todos;
        }
        public static Dictionary<int, Task> GetListTasksFromDB(int id)
        {
            string command = "SELECT * FROM Tasks WHERE List_ID = " + id;
            Dictionary<int, Task> todos = new Dictionary<int, Task>();
            using (SqliteConnection connection = DBService.DBConnection())
            {
                using (SqliteDataReader reader = DBService.ExecuteCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(reader.GetInt32(0), new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3)));
                    }
                }
            }
            return todos;
        }
    }
}
