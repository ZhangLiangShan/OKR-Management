using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhaoxi.ComputerMonitor.Common;

namespace Zhaoxi.ComputerMonitor.Model
{
    public class LoginModel : NotifyPropertyBase
    {
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                this.Notify();
            }
        }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                this.Notify();
            }
        }

        private string _validCode;

        public string ValidCode
        {
            get { return _validCode; }
            set
            {
                _validCode = value;
                this.Notify();
            }
        }

    }
}
