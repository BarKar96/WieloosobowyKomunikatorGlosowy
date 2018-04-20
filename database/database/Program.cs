using System;
using System.Data.SQLite;
using System.Security.Cryptography;
using System.Text;

namespace database
{
   
    class Program
    {
        static string SHA2_256(string input)
        {
            using (SHA256Managed sha1 = new SHA256Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }
                string result = sb.ToString();
                return result.ToLower();
            }
        }
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
        }

        static void OpenConnection()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
        }

        static bool AddUser(string login, string password)
        {
            try
            {
                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand("insert into Users (User_name, Password) values ($login, $password)", m_dbConnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", SHA2_256(password));
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Login istnieje");
                return false;
            }
        }

        static void GetAllUsers()
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from Users", m_dbConnection);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["User_ID"] + " "
                    + reader["User_name"] + " " + reader["Password"] + " "
                    + reader["IP_adress"] + " " + reader["Channel_ID"]);
            }
            Console.ReadKey();
            m_dbConnection.Close();
        }

        static void GetUser(int id)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from Users where User_ID=$id", m_dbConnection);
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
            m_dbConnection.Close();
        }

        static void GetUsersInChannel(int id)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from Users where Channel_ID=$id", m_dbConnection);
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
            m_dbConnection.Close();
        }

        static bool CheckPassword(string login, string password)
        {
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
            m_dbConnection.Open();
            SQLiteCommand command = new SQLiteCommand("select * from Users where User_name=$login", m_dbConnection);
            command.Parameters.AddWithValue("$login", login);
            SQLiteDataReader reader;
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader["Password"].ToString() == SHA2_256(password))
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        static bool ChangePassword(string login, string password)
        {
            try
            {

                SQLiteConnection m_dbConnection;
                m_dbConnection = new SQLiteConnection("Data Source=Communicator_database.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand("update Users set Password = $password where User_name=$login", m_dbConnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", SHA2_256(password));
                command.ExecuteNonQuery();
                m_dbConnection.Close();
                return true;
            }
            catch
            {
                Console.WriteLine("Nie ma takiego loginu");
                return false;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(ChangePassword("Adam", "adsa"));
        }
    }
}
