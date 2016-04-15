using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;

namespace PhotoBooth.Database.Controllers
{
    public class DataBase : Interfaces.IDataBase
    {
        public DataBase()
        {
            CreateBase();
        }

        public void CreateBase()
        {
            if(!File.Exists("PlugInBase.sqlite"))
                SQLiteConnection.CreateFile("PlugInBase.sqlite");
            if (!TableExists("PlugIn"))
            {
                using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
                {
                    db.Open();
                    string createTableSQL = "CREATE TABLE PlugIn(name text, type text);";
                    using (var query = new SQLiteCommand(createTableSQL, db))
                    {
                        query.ExecuteNonQuery();
                        db.Close();
                    }
                }
            }
            Insert("BasicPhotoEffects.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("AdvacedPhotoEffects.dll", PhotoBoothPlugInSDK.PlugInType.Image);

            Insert("BasicPhotoEffects1.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("AdvacedPhotoEffects1.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("BasicPhotoEffects2.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("AdvacedPhotoEffects2.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("BasicPhotoEffects3.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("AdvacedPhotoEffects3.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("BasicPhotoEffects4.dll", PhotoBoothPlugInSDK.PlugInType.Image);
            Insert("AdvacedPhotoEffects4.dll", PhotoBoothPlugInSDK.PlugInType.Image);
        }

        public void Insert(string name, PhotoBoothPlugInSDK.PlugInType type)
        { 
            if (!RowExists(name))
            {
                using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
                {
                    db.Open();
                    string addPlugIn = "INSERT INTO PlugIn(name,type) VALUES ('" + name + "','" + type + "');";
                    using (var command = new SQLiteCommand(addPlugIn, db))
                    {
                        command.ExecuteNonQuery();
                        db.Close();
                    }
                }
            } 
        }

        public List<string> SelectAll()
        {
            using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
            {
                db.Open();
                string selectAll = "SELECT name FROM PlugIn;";
                using (var command = new SQLiteCommand(selectAll, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<string> plugIns = new List<string>();
                        while (reader.Read())
                        {
                            plugIns.Add((string)reader["name"]);
                        }
                        db.Close();
                        return plugIns;
                    }
                }
            }
        }

        public List<string> SelectType(PhotoBoothPlugInSDK.PlugInType type)
        {
            using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
            {
                db.Open();
                string selectType = "SELECT name FROM PlugIn WHERE type='" + type + "';";
                using (var command = new SQLiteCommand(selectType, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<string> plugIns = new List<string>();
                        while (reader.Read())
                        {
                            plugIns.Add((string)reader["name"]);
                        }
                        db.Close();
                        return plugIns;
                    }
                }
            }
        }

        public void Delete(string name)
        {
            using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
            {
                db.Open();
                string delPlugIn = "DELETE FROM PlugIn WHERE name='" + name + "';";
                using (var command = new SQLiteCommand(delPlugIn, db))
                {
                    command.ExecuteNonQuery();
                    db.Close();
                }
            }
        }

        private Boolean TableExists(String tableName)
        {
            using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
            {
                db.Open();
                string query = "SELECT * FROM sqlite_master WHERE type = 'table' AND name = '" + tableName + "';";
                using (var command = new SQLiteCommand(query, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int tableCount = reader.StepCount;
                        db.Close();
                        if (tableCount == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }

        private Boolean RowExists(String name)
        {
            using (var db = new SQLiteConnection("Data Source=PlugInBase.sqlite;Version=3;"))
            {
                db.Open();
                string query = "SELECT name FROM PlugIn WHERE name = '" + name + "';";
                using (var command = new SQLiteCommand(query, db))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        int tableCount = reader.StepCount;
                        db.Close();
                        if (tableCount == 0)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }
        }
    }
}
