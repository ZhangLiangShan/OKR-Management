using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.ComputerMonitor.DataAccess.Entity
{
    public class UserEntity
    {
        public string user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public int is_can_login { get; set; }
        public int is_teacher { get; set; }
    }
}
