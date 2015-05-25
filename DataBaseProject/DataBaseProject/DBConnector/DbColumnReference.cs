using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.DBConnector
{
    public class DbColumnReference
    {
        public string Owner
        {
            get;
            set;
        }

        public string TableName
        {
            get;
            set;
        }

        public string ColumnName
        {
            get;
            set;
        }

        public String ToString()
        {
            return TableName + "." + ColumnName;
        }
    }
}
