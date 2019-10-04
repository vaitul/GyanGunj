using Databases.Domains;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Databases
{
    public class MasterDatabase
    {
        #region Properies & Fields

        private SQLiteConnection Connection;
        private string DataBase { get { return "GyanGunj.db"; } }
        public bool IsExist { get { return File.Exists(DataBase); } }

        #endregion

        #region Entity Collections

        private IList<MasterAttribute> _Attributes { get; set; }
        public IList<MasterAttribute> Attributes
        {
            get
            {
                if (_Attributes == null)
                    Fill();
                return _Attributes;
            }
            set
            {
                _Attributes = value;
            }
        }
        private IList<MasterLibraryData> _LibraryData { get; set; }
        public IList<MasterLibraryData> LibraryData
        {
            get
            {
                if (_LibraryData == null)
                    Fill();
                return _LibraryData;
            }
            set
            {
                _LibraryData = value;
            }
        }

        #endregion

        #region Ctor

        public MasterDatabase()
        {
            Connection = new SQLiteConnection("Data Source=" + DataBase + ";Version=3;New=True;Compress=True;Convert Zero Datetime=True;");

            Fill();
        }

        #endregion

        #region Methods

        public void Create()
        {

            if (!IsExist)
            {
                Connection.Open();
                SQLiteCommand cmd;

                var IniCommands = new List<string>()
                {
                    @"CREATE TABLE IF NOT EXISTS Attributes (
                        [Id] integer PRIMARY KEY, 
                        [Name] varchar(80) NULL, 
                        [Type] varchar(20) NULL, 
                        [Value] varchar(50) NULL,
                        [ValidFrom] DateTime NULL, 
                        [ValidTo] DateTime NULL
                    )",
                    @"CREATE TABLE IF NOT EXISTS LibraryData (
                        [Id] integer PRIMARY KEY, 
                        [Name] varchar(100) NULL, 
                        [Address] varchar(150) NULL,  
                        [Email] varchar(100) NULL,
                        [Mobile] varchar(100) NULL, 
                        [Phone] varchar(100) NULL, 
                        [PinCode] varchar(100) NULL, 
                        [City] varchar(100) NULL, 
                        [State] varchar(100) NULL, 
                        [Country] varchar(100) NULL,
                        [Website] varchar(50) NULL, 
                        [FolderName] varchar(20) NULL
                    )",
                };

                foreach (var query in IniCommands)
                {
                    cmd = new SQLiteCommand(query, Connection);
                    cmd.ExecuteNonQuery();
                }
            }
            Connection.Close();
        }
        private void Fill()
        {
            if (!IsExist)
                return;

            Connection.Open();

            SQLiteCommand cmd;
            SQLiteDataReader dr;

            //Filling Attributes
            Attributes = new List<MasterAttribute>();
            cmd = new SQLiteCommand("SELECT * FROM Attributes ORDER BY Id", Connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Attributes.Add(new MasterAttribute()
                {
                    Id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Type = dr.GetString(2),
                    Value = dr.GetString(3),
                    //ValidFrom = dr.GetDateTime(4),
                    //ValidTo = dr.GetDateTime(5),
                });

            }

            //Filling LibraryData
            LibraryData = new List<MasterLibraryData>();
            cmd = new SQLiteCommand("SELECT * FROM LibraryData ORDER BY Id", Connection);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LibraryData.Add(new MasterLibraryData()
                {
                    Id = dr.GetInt32(0),
                    Name = dr.GetString(1),
                    Address = dr.GetString(2),
                    Email = dr.GetString(3),
                    Mobile = dr.GetString(4),
                    Phone = dr.GetString(5),
                    PinCode = dr.GetString(6),
                    City = dr.GetString(7),
                    State = dr.GetString(8),
                    Country = dr.GetString(9),
                    Website = dr.GetString(10),
                    FolderName = dr.GetString(11)
                });
            }

            Connection.Close();
        }
        public void Insert(MasterAttribute entity)
        {
            if (!IsExist)
                return;
            Connection.Open();
            SQLiteCommand cmd;
            cmd = new SQLiteCommand(string.Format("INSERT INTO Attributes VALUES(null,'{1}','{2}','{3}','{4}','{5}')", entity.Id, entity.Name, entity.Type, entity.Value, entity.ValidFrom, entity.ValidTo), Connection);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        public void Insert(IEnumerable<MasterAttribute> entities)
        {
            if (!IsExist)
                return;
            Connection.Open();
            SQLiteCommand cmd;
            foreach (var entity in entities)
            {
                cmd = new SQLiteCommand(string.Format("INSERT INTO Attributes VALUES(null,'{1}','{2}','{3}','{4}','{5}')", entity.Id, entity.Name, entity.Type, entity.Value, entity.ValidFrom, entity.ValidTo), Connection);
                cmd.ExecuteNonQuery(); ;
            }
            Connection.Close();
        }
        public void Insert(MasterLibraryData entity)
        {
            if (!IsExist)
                return;
            Connection.Open();
            SQLiteCommand cmd;
            cmd = new SQLiteCommand(string.Format("INSERT INTO LibraryData VALUES(null,'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", entity.Id, entity.Name, entity.Address, entity.Email, entity.Mobile, entity.Phone, entity.PinCode, entity.City, entity.State, entity.Country, entity.Website, entity.FolderName), Connection);
            cmd.ExecuteNonQuery();
            Connection.Close();
        }
        public void Insert(IEnumerable<MasterLibraryData> entities)
        {
            if (!IsExist)
                return;
            Connection.Open();
            SQLiteCommand cmd;
            foreach (var entity in entities)
            {
                cmd = new SQLiteCommand(string.Format("INSERT INTO LibraryData VALUES(null,'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}')", entity.Id, entity.Name, entity.Address, entity.Email, entity.Mobile, entity.Phone, entity.PinCode, entity.City, entity.State, entity.Country, entity.Website, entity.FolderName), Connection);
                cmd.ExecuteNonQuery();
            }
            Connection.Close();
        }

        #endregion
    }
}
