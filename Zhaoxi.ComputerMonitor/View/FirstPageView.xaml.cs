using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhaoxi.ComputerMonitor.Common;
using Zhaoxi.ComputerMonitor.ViewModel;

namespace Zhaoxi.ComputerMonitor.View
{
    /// <summary>
    /// FirstPageView.xaml 的交互逻辑
    /// </summary>
    public partial class FirstPageView : UserControl
    {
        FirstPageViewModel model = new FirstPageViewModel();
        public FirstPageView()
        {
            InitializeComponent();

            this.DataContext = model;

            this.Unloaded += FirstPageView_Unloaded;
        }

        private void FirstPageView_Unloaded(object sender, RoutedEventArgs e)
        {
            model?.Dispose();
        }
    }
}
