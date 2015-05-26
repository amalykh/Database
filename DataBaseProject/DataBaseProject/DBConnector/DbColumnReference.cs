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

        public static DbColumnReference TryParse(string s)
        {
            var t = new DbColumnReference();

            t.Owner = null;
            try
            {
                t.TableName = s.Split('.')[0];
                t.ColumnName = s.Split('.')[1];
            }
            catch (Exception e)
            {
                throw new Exception("Reference must be something like this: TableName.ColumnName");
            }

            return t;
        }
    }
}
