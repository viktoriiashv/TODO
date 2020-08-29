using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Microsoft.Data.Sqlite;
using TodoList.Models;


namespace TodoList.Controllers
{
    public class DBService
    {
        public static SqliteConnection DBConnection()
        {
            SqliteConnection dbConnection = new SqliteConnection("Data Source=database.db");
            return dbConnection;
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
