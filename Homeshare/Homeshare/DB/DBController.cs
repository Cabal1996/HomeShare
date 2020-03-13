using Homeshare.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

/*
 *   Static class to control database
 * 
 */

namespace Homeshare.DB
{
    public static class DBController
    {
        // Returns connection to data base through path text
        public static SQLiteConnection GetDBConnection(string DBPath)
        {
            return new SQLiteConnection(DBPath);
        }

        // Adds new table in database based on table item class object
        public static void AddTable(TableItem tableType)
        {
            SQLiteConnection connection = GetDBConnection(App.DatabasePath);
            connection.CreateTable(tableType.GetType());
            connection.Close();
        }

        //Inserts new item to existing table
        static public void InsertItem(object ItemToinsetr)
        {
            SQLiteConnection connection = GetDBConnection(App.DatabasePath);

            connection.Insert(ItemToinsetr);


            connection.Close();
        }

        //Returns data based as list of objects
        static public List<T> GetInfo<T>() where T : new()
        {
            SQLiteConnection connection = GetDBConnection(App.DatabasePath);

            var val = connection.Table<T>().ToList();

            connection.Close();
            return val;
        }

        //Check if table with given name exists
        public static bool TableExists(String tableName)
        {
            SQLiteConnection connection = GetDBConnection(App.DatabasePath);

            TableMapping map = new TableMapping(typeof(SqlDbType)); // Instead of mapping to a specific table just map the whole database type
            object[] ps = new object[0]; // An empty parameters object since I never worked out how to use it properly! (At least I'm honest)

            int tableCount = connection.Query(map, "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "'", ps).Count; // Executes the query from which we can count the results
            if (tableCount == 0)
            {
                connection.Close();
                return false;
            }
            else if (tableCount == 1)
            {
                connection.Close();
                return true;
            }
            else
            {
                connection.Close();
                throw new Exception("More than one table by the name of " + tableName + " exists in the database.", null);
            }

        }
    }
}
