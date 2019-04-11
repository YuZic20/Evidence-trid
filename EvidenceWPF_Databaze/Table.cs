using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace EvidenceWPF_Databaze

{
    public class Table
    {

        static Database database;

        public static Database Database
        {
            get
            {
                if (database == null)
                {
                    database = new Database(
                      Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EvidenceTridAJejichObsahu_SQLite.db3"));
                }
                return database;
            }
        }



    }

}
