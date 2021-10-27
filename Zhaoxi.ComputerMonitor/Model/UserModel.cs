using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.ComputerMonitor.Model
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Password { get; set; }
        public bool IsCanLogin { get; set; }
        public string Avatar { get; set; }
        public int Gender { get; set; }
    }
}
