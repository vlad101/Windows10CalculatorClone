﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Calculator.Database
{
    class Data
    {
        private MySqlConnection _con;
        private MySqlCommand cmd;

        public Data()
        {
            MySqlConnectionStringBuilder _connectionStr = new MySqlConnectionStringBuilder
            {
                Server = Properties.Settings.Default.DBHost,
                Port = Convert.ToUInt32(Properties.Settings.Default.DBPort),
                Database =Properties.Settings.Default.DBName,
                UserID = Properties.Settings.Default.DBUser,
                Password = Properties.Settings.Default.DBPassword,
                ConnectionTimeout = 60,
                AllowZeroDateTime = true
            };
            _con = new MySqlConnection(_connectionStr.ConnectionString);
        }

        public void InsertHistoryEntry(String historyLog)
        {
            if(historyLog != null)
            {
                // Use a try... catch...finally block to ensure the connection is closed properly
                try
                {
                    // Open connection
                    _con.Open();

                    // Set the value of the parameter to the entry text log
                    cmd = new MySqlCommand();
                    cmd.Connection = _con;

                    // Use a parameterized query to avoid SQL Injection
                    cmd.CommandText = "INSERT INTO history VALUES (@EntryId, @HistoryEntry)";
                    cmd.Parameters.AddWithValue("@EntryId", null);
                    cmd.Parameters.AddWithValue("@HistoryEntry", historyLog);

                    // Execute query
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    MessageBox.Show("Error, Cannot get connected to database!");
                }
                finally
                {
                    _con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
                }
            }
        }
    }
}

/*
 * Database setup
 * 
 */
/*
    CREATE DATABASE calculator;
    CREATE TABLE History
    (
        EntryID INT NOT NULL AUTO_INCREMENT,
        HistoryEntry varchar(255) NOT NULL,
        PRIMARY KEY (EntryID)
    );
 */