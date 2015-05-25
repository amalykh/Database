using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class DbColumnInfo
    {
        public string ColumnName { get; set; }
        public string DataType { get; set; }
        public bool Nullable { get; set; }
        public string PrimaryKey { get; set; }
        public string Reference { get; set; }
    }
}
