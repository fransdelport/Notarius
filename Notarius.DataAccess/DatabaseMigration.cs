using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Text;

namespace Notarius.DataAccess
{
    public static class DatabaseMigration
    {

        private static string _dataBase = "";

        public static SQLiteConnection OpenConnection()
        {
            if(string.IsNullOrEmpty(_dataBase))
            {
                //Load configuration
                Configuration.Confguration config = new Configuration.Confguration();
                config.LoadState();
                _dataBase = config.Database;
            }

            if (!File.Exists(_dataBase))
            {
                InitializeDatabase(_dataBase);
            }

            string connectionString = string.Format("Data Source={0};Version=3;", _dataBase);
            SQLiteConnection dbConnection = new SQLiteConnection(connectionString);
            dbConnection.Open();

            return dbConnection;
        }

        private static void InitializeDatabase(string database)
        {
            _dataBase = database;
            string dataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\SleutelWoordData\";
            if (!Directory.Exists(dataPath))
                Directory.CreateDirectory(dataPath);
            // dbPath = ":memory:";
            if (File.Exists(_dataBase))
            {
                string target = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SleutelDatabaseBackup.db3");
                if (File.Exists(target))
                    File.Delete(target);

                File.Copy(_dataBase, target);
            }
            else
                CreateDatabase();



            //_database.CreateTableAsync<SecretQuestionsDTO>().Wait();
            //_database.CreateTableAsync<SleutelWoordDTO>().Wait();
            // _database.CreateTableAsync<SecurityUserDTO>().Wait();
        }

        private static  void CreateDatabase()
        {
            try
            {
                System.Data.SQLite.SQLiteConnection.CreateFile(_dataBase);

                string connectionString = string.Format("Data Source={0};Version=3;", _dataBase);
                System.Data.SQLite.SQLiteConnection dbConnection = new System.Data.SQLite.SQLiteConnection(connectionString);
                dbConnection.Open();

                string sql;
                SQLiteCommand command;

                PatientTable(dbConnection, out sql, out command);
                ScheduleTable(dbConnection, out sql, out command);


                dbConnection.Close();
            }
            catch (Exception ex)
            {
                string er = ex.ToString();


            }
        }

        private static void PatientTable(SQLiteConnection dbConnection, out string sql, out SQLiteCommand command)
        {
            sql = SQLResources.CreatePatientTable;

            command = new System.Data.SQLite.SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

        }
        private static void ScheduleTable(SQLiteConnection dbConnection, out string sql, out SQLiteCommand command)
        {
            sql = SQLResources.CreateScheduleTable;

            command = new System.Data.SQLite.SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();

        }
    }
}
