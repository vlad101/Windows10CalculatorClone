using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using Calculator.Memory;

namespace Calculator.Database
{
    class DataMemory
    {
        // Database connection
        private MySqlConnection _con;
        private MySqlCommand cmd;

        // Last added database entry id
        public static int LastInsertedMemoryEntryId = -1;

        public DataMemory()
        {
            MySqlConnectionStringBuilder _connectionStr = new MySqlConnectionStringBuilder
            {
                Server = Properties.Settings.Default.DBHost,
                Port = Convert.ToUInt32(Properties.Settings.Default.DBPort),
                Database = Properties.Settings.Default.DBName,
                UserID = Properties.Settings.Default.DBUser,
                Password = Properties.Settings.Default.DBPassword,
                ConnectionTimeout = 60,
                AllowZeroDateTime = true
            };
            _con = new MySqlConnection(_connectionStr.ConnectionString);
        }

        public int InsertMemoryEntry(String memoryLog)
        {
            int flag = -1;

            if (memoryLog != null)
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
                    cmd.CommandText = "INSERT INTO memory VALUES (@EntryId, @MemoryEntry)";
                    cmd.Parameters.AddWithValue("@EntryId", null);
                    cmd.Parameters.AddWithValue("@MemoryEntry", memoryLog);

                    // Execute query
                    cmd.ExecuteNonQuery();

                    // Get the Unique ID for the Last Inserted Row
                    cmd.CommandText = "SELECT LAST_INSERT_ID()";

                    // Execute query
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            flag = reader.GetInt32(0);
                        }
                    }
                    reader.Close();

                    LastInsertedMemoryEntryId = flag;
                }
                catch
                {
                    MessageBox.Show("Error, Cannot get connected to database!");

                    // Data inserted fail
                    flag = -1;
                }
                finally
                {
                    _con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
                }
            }
            return flag;
        }

        public bool UpdateMemoryEntry(String EntryId, String memoryLog)
        {
            bool flag = false;
            
            if (EntryId != null && memoryLog != null)
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
                    cmd.CommandText = "UPDATE memory SET MemoryEntry=@MemoryEntry where EntryId=@EntryId";
                    cmd.Parameters.AddWithValue("@EntryId", EntryId);
                    cmd.Parameters.AddWithValue("@MemoryEntry", memoryLog);

                    // Execute query
                    cmd.ExecuteNonQuery();

                    // Data inserted success
                    flag = true;
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
            return flag;
        }

        public Dictionary<int, MemoryLog> GetMemoryEntryList()
        {
            // Store list of memory log entries in a dictionary
            Dictionary<int, MemoryLog> dictHMemoryLog = new Dictionary<int, MemoryLog>();

            // Use a try... catch...finally block to ensure the connection is closed properly
            try
            {
                // Open connection
                _con.Open();

                // Set the value of the parameter to the entry text log
                cmd = new MySqlCommand();
                cmd.Connection = _con;

                // Use a parameterized query to avoid SQL Injection
                cmd.CommandText = "SELECT * FROM memory";

                // Execute query
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        dictHMemoryLog.Add(reader.GetInt32(0), new MemoryLog(reader.GetString(1)));
                    }
                }
                reader.Close();
            }
            catch
            {
                MessageBox.Show("Error, Cannot get connected to database!");
            }
            finally
            {
                _con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
            }
            return dictHMemoryLog;
        }

        public bool DeleteMemoryEntry()
        {
            bool isDeleted = false;

            // Use a try... catch...finally block to ensure the connection is closed properly
            try
            {
                // Open connection
                _con.Open();

                // Set the value of the parameter to the entry text log
                cmd = new MySqlCommand();
                cmd.Connection = _con;

                // Delete query
                cmd.CommandText = "DELETE FROM memory";

                // Execute query
                cmd.ExecuteNonQuery();

                // Set flag to success
                isDeleted = true;
            }
            catch
            {
                MessageBox.Show("Error, Cannot get connected to database!");
                isDeleted = false;
            }
            finally
            {
                _con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
            }
            return isDeleted;
        }

        public int DeleteMemoryEntryById(String memoryEntryId)
        {
            int flag = -1;

            if (memoryEntryId != null)
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
                    cmd.CommandText = "DELETE FROM memory where EntryId=@EntryId";
                    cmd.Parameters.AddWithValue("@EntryId", memoryEntryId);

                    // Execute query
                    cmd.ExecuteNonQuery();

                    // Success
                    flag = 1;
                }
                catch
                {
                    MessageBox.Show("Error, Cannot get connected to database!");

                    // Data inserted fail
                    flag = -1;
                }
                finally
                {
                    _con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
                }
            }
            return flag;
        }
    }
}

/*
    CREATE TABLE Memory
    (
        EntryID INT NOT NULL AUTO_INCREMENT,
        MemoryEntry varchar(255) NOT NULL,
        PRIMARY KEY (EntryID)
    );
 */