using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.Database
{
    class Data
    {
        private SqlConnection con;

        public Data()
        {
            MessageBox.Show("HELLO I AM HERE!");
            con = new SqlConnection(@"Data Source = ABC-PC; trusted_connection = yes; Database = school; connection timeout = 30");
        }

        public void InsertHistoryEntry(String historyLog)
        {
            MessageBox.Show("HELLO I AM HERE!");

            if(historyLog != null)
            {
                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();

                // Use a parameterized query to avoid SQL Injection
                cmd.CommandText = "INSERT INTO calculator VALUES (@EntryId, @HistoryEntry)";
                cmd.Connection = con;

                // Set the value of the parameter to the entry text log
                cmd.Parameters.AddWithValue("@EntryId", null);
                cmd.Parameters.AddWithValue("@HistoryEntry", historyLog);

                MessageBox.Show(cmd.CommandText);

                // Use a try... catch...finally block to ensure the connection is closed properly
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Item Inserted");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    con.Close(); // will happen whether the try is successful or errors out, ensuring your connection is closed properly.
                }
            }
        }
    }
}

/*
CREATE DATABASE calculator;
CREATE TABLE History
(
    EntryID INT NOT NULL AUTO_INCREMENT,
    HistoryEntry varchar(255) NOT NULL,
    PRIMARY KEY (EntryID)
);
 */