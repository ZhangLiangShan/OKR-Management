using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Zhaoxi.ComputerMonitor.Model;
using Zhaoxi.ComputerMonitor.ViewModel;

namespace Zhaoxi.ComputerMonitor.View
{
    /// <summary>
    /// CourseListView.xaml 的交互逻辑
    /// </summary>
    public partial class CourseListView : UserControl
    {
        public CourseListView()
        {
            InitializeComponent();

            CourseViewModel model = new CourseViewModel();
            this.DataContext = model;
        }

        private void rbCategoryTeacher_Click(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            string condition = rb.Content.ToString();

            ICollectionView view = CollectionViewSource.GetDefaultView(this.icCourses.ItemsSource);

            if (condition == "全部")
            {
                view.Filter = null;
                //排序
                //view.SortDescriptions.Add(new SortDescription("CoursesName", ListSortDirection.Ascending));
            }
            else
            {
                view.Filter = new Predicate<object>((o) =>
                {
                    return (o as CourseModel).Teachers.Exists(t => t == condition);
                });
            }
        }
    }
}
