using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.ComputerMonitor.View;

namespace Zhaoxi.ComputerMonitor
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            if(new LoginView().ShowDialog() == true)
            {
                new MainWindowView().ShowDialog();
            }
            Application.Current.Shutdown();
        }
    }
}
