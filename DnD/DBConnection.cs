using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DnD
{
    public class DBConnection
    {
        private DBConnection()
        {
        }

        public string Server { get; set; } = "localhost"; //Значение по умолчанию (Чтобы при новых запросах не мучаться с указанием полей у объекта)
        public string DatabaseName { get; set; } = "dungeons_and_dragons";
        public string UserName { get; set; } = "root";
        public string Password { get; set; } = "Anomalies123";



        public MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }
        public void Update()
        {
            Connection.Open();
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;   
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }

    }
}
