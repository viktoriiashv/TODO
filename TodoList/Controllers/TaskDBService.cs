using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using TodoList.Models;

namespace TodoList.Controllers
{
    public class TaskDBService
    {
        public static void CreateTable()
        {
            string command = "CREATE TABLE IF NOT EXISTS Tasks (ID INT(255), Name VARCHAR(20), Done TINYINT(1))";
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
                
        }

        public static void InsertData(Task task)
        {
            string command = "INSERT INTO Tasks (Name, Done, List_ID) VALUES('" + task.Name + "', " + task.Done + ", " + task.TaskListId + ")";
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void UpdateData(int id, bool doneCondition)
        {
            string command = "UPDATE Tasks SET Done = " + doneCondition + " WHERE ID = "+ id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }
        public static void UpdateDataName(int id, string name)
        {
            string command = "UPDATE Tasks SET Name = '" + name + "' WHERE ID = " + id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static Dictionary<int, Task> GetDataFromDB()
        {
            string command = "SELECT * FROM Tasks";
            Dictionary<int, Task>  todos = new Dictionary<int, Task>();
            using (SqliteConnection connection = DBService.DBConnection())
            {
                using (SqliteDataReader reader = DBService.ExecuteCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(reader.GetInt32(0), new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(0)));
                    }
                }
            }
            return todos;
        }

        public static void DeleteData(int id)
        {
            string command = "DELETE FROM Tasks WHERE ID = " + id;
            using (SqliteConnection connection = DBService.DBConnection())
            {
                SqliteCommand dbCommand = DBService.ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
