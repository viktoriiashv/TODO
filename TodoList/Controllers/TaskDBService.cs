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
            string command = "CREATE TABLE IF NOT EXISTS ToDo (ID INT(255), Name VARCHAR(20), Done TINYINT(1))";
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
                
        }
        public static SqliteConnection DBConnection()
        {
            SqliteConnection dbConnection = new SqliteConnection("Data Source=database.db");
            return dbConnection;
        }

        public static void InsertData(Task task)
        {
            string command = "INSERT INTO ToDO (Name, Done, List_ID) VALUES('" + task.name + "', " + task.done + ", " + task.taskListId + ")";
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void InsertDataTaskList(TaskList taskList)
        {
            string command = "INSERT INTO List (Name) VALUES('" + taskList.name + "')";
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void UpdateData(int id, bool doneCondition)
        {
            string command = "UPDATE ToDO SET Done = " + doneCondition + " WHERE ID = "+ id;
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void UpdateDataTaskList(int id, string name)
        {
            string command = "UPDATE List SET Name = '" + name + "' WHERE ID = " + id;
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static Dictionary<int, Task>  GetDataFromDB()
        {
            string command = "SELECT * FROM ToDO";
            Dictionary<int, Task>  todos = new Dictionary<int, Task>();
            using (SqliteConnection connection = DBConnection())
            {
                using (SqliteDataReader reader = ExecuteCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(reader.GetInt32(0), new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(0)));
                    }
                }
            }
            return todos;
        }



        public static Dictionary<int, TaskList> GetTaskListsFromDB()
        {
            string command = "SELECT * FROM List";
            Dictionary<int, TaskList> todos = new Dictionary<int, TaskList>();
            using (SqliteConnection connection = DBConnection())
            {
                using (SqliteDataReader reader = ExecuteCommand(command, connection).ExecuteReader())
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
            string command = "SELECT * FROM ToDO WHERE List_ID = " + id;
            Dictionary<int, Task> todos = new Dictionary<int, Task>();
            using (SqliteConnection connection = DBConnection())
            {
                using (SqliteDataReader reader = ExecuteCommand(command, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        todos.Add(reader.GetInt32(0), new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2), reader.GetInt32(3)));
                    }
                }
            }
            return todos;
        }

        public static void DeleteData(int id)
        {
            string command = "DELETE FROM ToDO WHERE ID = " + id;
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static void DeleteTaskList(int id)
        {
            string command = "DELETE FROM List WHERE ID = " + id;
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
            command = "DELETE FROM ToDO WHERE List_ID = " + id;
            using (SqliteConnection connection = DBConnection())
            {
                SqliteCommand dbCommand = ExecuteCommand(command, connection);
                dbCommand.ExecuteNonQuery();
            }
        }

        public static SqliteCommand ExecuteCommand(string command, SqliteConnection dbConnection)
        {
            dbConnection.Open();
            SqliteCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = command;
            return dbCommand;
        }

    }
}
