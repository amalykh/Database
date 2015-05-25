using DataBases.DbConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class DB
    {
        public static IDbConnector connector { get; set; }
    }
}
