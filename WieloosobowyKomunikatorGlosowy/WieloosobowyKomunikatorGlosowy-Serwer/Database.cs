using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    class Database
    {
		/*
        static void CreateDatabase()
        {
            SQLiteConnection.CreateFile("Communicator_database.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
            string sql = "create table Channels (Channel_ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Channel_name varchar(25) not null unique, " +
                "Password varchar(32), Description varchar(255))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            sql = "create table Users (User_ID INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, User_name varchar(25) not null unique, Password varchar(32) not null," +
                " IP_adress varchar(15), Channel_ID integer, FOREIGN KEY (Channel_ID) REFERENCES Channels(Channel_ID))";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            m_dbConnection.Close();
        }*/
		public SQLiteConnection m_dbConnection;
        public SQLiteCommand command;
        public SQLiteDataReader reader;

        public Database()
        {
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        public bool AddUser(string login, string password)
        {
            try
            {
                command = new SQLiteCommand("insert into Users (User_name, Password) values ($login, $password)", m_dbConnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", password);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                Console.WriteLine("Login istnieje");
                return false;
            }
        }

        private void GetAllUsers()
        {
			command = new SQLiteCommand("select * from Users", m_dbConnection);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["User_ID"] + " "
                    + reader["User_name"] + " " + reader["Password"] + " "
                    + reader["IP_adress"] + " " + reader["Channel_ID"]);
            }
            Console.ReadKey();
        }

        private void GetUser(int id)
        {
            command = new SQLiteCommand("select * from Users where User_ID=$id", m_dbConnection);
            command.Parameters.AddWithValue("$id", id);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["User_ID"] + " "
                    + reader["User_name"] + " " + reader["Password"] + " "
                    + reader["IP_adress"] + " " + reader["Channel_ID"]);
            }
            Console.ReadKey();
        }

        private void GetUsersInChannel(int id)
        {
            command = new SQLiteCommand("select * from Users where Channel_ID=$id", m_dbConnection);
            command.Parameters.AddWithValue("$id", id);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["User_ID"] + " "
                    + reader["User_name"] + " " + reader["Password"] + " "
                    + reader["IP_adress"] + " " + reader["Channel_ID"]);
            }
            Console.ReadKey();
        }

        private bool CheckPassword(string login, string password)
        {
            command = new SQLiteCommand("select * from Users where User_name=$login", m_dbConnection);
            command.Parameters.AddWithValue("$login", login);
            
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader["Password"].ToString() == password)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool ChangePassword(string login, string password)
        {
            try
            {
                command = new SQLiteCommand("update Users set Password = $password where User_name=$login", m_dbConnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", password);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                Console.WriteLine("Nie ma takiego loginu");
                return false;
            }
        }

        private void SetIP(string login, string IP)
        {
            command = new SQLiteCommand("update Users set IP_adress = $IP_adress where User_name=$login", m_dbConnection);
            command.Parameters.AddWithValue("$login", login);
            command.Parameters.AddWithValue("$IP_adress", IP);
            command.ExecuteNonQuery();
        }
    }
}
