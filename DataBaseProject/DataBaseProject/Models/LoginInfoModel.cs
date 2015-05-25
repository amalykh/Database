using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseProject.Models
{
    public class LoginInfoModel : PropertyChangedBase
    {
        public string Username { get; set; }
        public string IP { get; set; }
        public string Tablespace { get; set; }
    }
}
