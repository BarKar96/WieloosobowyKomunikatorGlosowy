using System;
using System.Data.SQLite;
using System.Collections.Generic;

namespace WieloosobowyKomunikatorGlosowy_Serwer
{
    class Database
    {
        
        public static void CreateDatabase()
        {
            SQLiteConnection.CreateFile("communicator_database.sqlite");
            SQLiteConnection m_dbconnection;
            m_dbconnection = new SQLiteConnection("data source=communicator_database.sqlite;version=3;");
            m_dbconnection.Open();
            string sql = "create table channels (channel_id integer primary key autoincrement not null, channel_name varchar(25) not null unique, " +
                "password varchar(32), description varchar(255))";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbconnection);
            command.ExecuteNonQuery();
            sql = "create table users (user_id integer primary key autoincrement not null, user_name varchar(25) not null unique, password varchar(32) not null," +
                " channel_id integer, foreign key (channel_id) references channels(channel_id))";
            command = new SQLiteCommand(sql, m_dbconnection);
            command.ExecuteNonQuery();
            m_dbconnection.Close();
        }
        public SQLiteConnection m_dbconnection;
        public SQLiteCommand command;
        private SQLiteDataReader reader;

        public Database()
        {
            m_dbconnection = new SQLiteConnection("data source=communicator_database.sqlite;version=3;");
            m_dbconnection.Open();           
        }

        public bool AddUser(string login, string password)
        {
            try
            {
                command = new SQLiteCommand("insert into users (user_name, password) values ($login, $password)", m_dbconnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", password);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void getusersinchannel(int id)
        {
            command = new SQLiteCommand("select * from users where channel_id=$id", m_dbconnection);
            command.Parameters.AddWithValue("$id", id);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["user_id"] + " "
                    + reader["user_name"] + " " + reader["password"] + " "
                    + reader["ip_adress"] + " " + reader["channel_id"]);
            }
            Console.ReadKey();
        }

        public bool CheckPassword(string login, string password)
        {
            command = new SQLiteCommand("select * from users where user_name=$login", m_dbconnection);
            command.Parameters.AddWithValue("$login", login);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                if (reader["password"].ToString() == password)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        private bool changepassword(string login, string password)
        {
            try
            {
                command = new SQLiteCommand("update users set password = $password where user_name=$login", m_dbconnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$password", password);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                Console.WriteLine("nie ma takiego loginu");
                return false;
            }
        }

        public bool AddChannel(string channel_name, string password, string description)
        {
            try
            {
                command = new SQLiteCommand("insert into channels(channel_name, password, description) " +
                    "values($channel_name, $password, $description)", m_dbconnection);
                command.Parameters.AddWithValue("$channel_name", channel_name);
                command.Parameters.AddWithValue("$password", password);
                command.Parameters.AddWithValue("$description", description);
                command.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public List<Channel> GetChannelList()
        {
            List<Channel> channelList = new List<Channel>();
            command = new SQLiteCommand("select * from channels", m_dbconnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                channelList.Add(new Channel(reader["channel_name"].ToString(), reader["password"].ToString(),
                    reader["description"].ToString()));
            }
            return channelList;
        }

        public void AddUserToChannel(string login, string channel_name)
        {
            command = new SQLiteCommand("select channel_id from channels where channel_name=$channel_name", m_dbconnection);
            command.Parameters.AddWithValue("$channel_name", channel_name);
            reader = command.ExecuteReader();
            if (reader.Read())
            {
                int channel_id = Int32.Parse(reader["channel_id"].ToString());
                command = new SQLiteCommand("update users set channel_id = $channel_id where user_name=$login", m_dbconnection);
                command.Parameters.AddWithValue("$login", login);
                command.Parameters.AddWithValue("$channel_id", channel_id);
                command.ExecuteNonQuery();
            }
        }
        public void RemoveUserFromChannel(string login)
        {
            command = new SQLiteCommand("update users set channel_id = NULL where user_name=$login", m_dbconnection);
            command.Parameters.AddWithValue("$login", login);
            command.ExecuteNonQuery();
        }
        public void RemoveChannel(string channelName)
        {
            command = new SQLiteCommand("delete from channels where channel_name =$channelName", m_dbconnection);
            command.Parameters.AddWithValue("$channelName", channelName);
            command.ExecuteNonQuery();
        }
    }
}
