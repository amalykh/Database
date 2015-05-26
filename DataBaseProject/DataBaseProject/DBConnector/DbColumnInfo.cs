using DataBaseProject.DBConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class DbColumnInfo
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public bool Nullable { get; set; }
        public bool PrimaryKey { get; set; }
        public string Reference { get; set; }

        public string CreateString()
        {
            StringBuilder res = new StringBuilder("");
            
            res.Append(Name);
            res.Append(" ");
            res.Append(Type);
            res.Append(" ");
            if (Reference != "" && Reference != null)
            {
                DbColumnReference reference = DbColumnReference.TryParse(Reference);
                res.Append(string.Format("REFERENCES {0}({1})", reference.TableName, reference.ColumnName));
                res.Append(" ");
            }
            if (!Nullable)
                res.Append("NOT NULL ");

            return res.ToString();
        }
    }
}
