using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvidenceWPF_Databaze
{
    public abstract class ATable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
