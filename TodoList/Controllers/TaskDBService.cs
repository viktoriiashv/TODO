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
            string command = "INSERT INTO ToDO (ID, Name, Done) VALUES(" + task.id + ", '" + task.name + "', " + task.done + ")";
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
                        todos.Add(reader.GetInt32(0), new Task(reader.GetInt32(0), reader.GetString(1), reader.GetBoolean(2)));
                    }
                }
            }
            return todos;
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
