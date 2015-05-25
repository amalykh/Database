using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class DbColumnNamesModel
    {
        public string Name { get; set; }
        public DbColumnNamesModel(string name)
        {
            this.Name = name;
        }
        public DbColumnNamesModel(string name, string value)
        {
            Name = name;
            Value = value;
        }
        public string Value { get; set; }
    }
}
