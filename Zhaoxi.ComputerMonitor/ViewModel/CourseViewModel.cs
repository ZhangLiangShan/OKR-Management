using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.ComputerMonitor.Common;
using Zhaoxi.ComputerMonitor.DataAccess;
using Zhaoxi.ComputerMonitor.Model;

namespace Zhaoxi.ComputerMonitor.ViewModel
{
    public class CourseViewModel
    {
        public ObservableCollection<CategoryItemModel> CategoryCourses { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTechnology { get; set; }
        public ObservableCollection<CategoryItemModel> CategoryTeacher { get; set; }

        public ObservableCollection<CourseModel> CoursesList { get; set; }
        public List<CourseModel> coursesAll { get; set; }

        public CommandBase CategoryTeacherCommand { get; set; }
        public CommandBase OpenCourseUrlCommand { get; set; }

        //模拟课程分类
        string[] courseArray = new string[] { "公开课", "VIP课程" };
        //模拟技术分类
        string[] technologyArray = new string[] { "C#", "ASP.NET", "微服务", "Java", "Vue", "微信小程序", "Winform", "WPF", "上位机" };

        public CourseViewModel()
        {
            this.CategoryTeacherCommand = new CommandBase();
            this.CategoryTeacherCommand.ExcuteAction = new Action<object>(DoFilterWithTeacher);
            this.CategoryTeacherCommand.CanExecuteFunc = new Func<bool>(() => true);

            this.OpenCourseUrlCommand = new CommandBase();
            this.OpenCourseUrlCommand.ExcuteAction = new Action<object>(DoOpenCourseUrl);
            this.OpenCourseUrlCommand.CanExecuteFunc = new Func<bool>(() => true);

            this.InitCategory();

            this.InitCoursesList();
        }

        private void InitCategory()
        {
            this.CategoryCourses = new ObservableCollection<CategoryItemModel>();
            this.CategoryCourses.Add(new CategoryItemModel("全部", true));
            foreach (var item in courseArray)
                this.CategoryCourses.Add(new CategoryItemModel(item));

            this.CategoryTechnology = new ObservableCollection<CategoryItemModel>();
            this.CategoryTechnology.Add(new CategoryItemModel("全部", true));
            foreach (var item in technologyArray)
                this.CategoryTechnology.Add(new CategoryItemModel(item));

            this.CategoryTeacher = new ObservableCollection<CategoryItemModel>();
            this.CategoryTeacher.Add(new CategoryItemModel("全部", true));
            foreach (var item in LocalDataAccess.GetInstance().GetTeachers())
                this.CategoryTeacher.Add(new CategoryItemModel(item));
        }
        private void InitCoursesList()
        {
            Task.Run(new Action(async () =>
            {
                this.CoursesList = new ObservableCollection<CourseModel>();

                for (int i = 0; i < 10; i++)
                {
                    this.CoursesList.Add(new CourseModel { IsShowSkeletion = true });
                }

                coursesAll = LocalDataAccess.GetInstance().GetCourses();

                await Task.Delay(4000);

                Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    this.CoursesList.Clear();

                    foreach (var item in coursesAll)
                        this.CoursesList.Add(item);
                }));
            }));
        }

        private void DoFilterWithTeacher(object obj)
        {
            List<CourseModel> filter = this.coursesAll;
            if (obj.ToString() != "全部")
            {
                filter = this.coursesAll.Where(c => c.Teachers.Exists(t => t.ToLower() == obj.ToString().ToLower())).ToList();
            }

            this.CoursesList.Clear();

            foreach (var item in filter)
                this.CoursesList.Add(item);
        }

        private void DoOpenCourseUrl(object obj)
        {
            System.Diagnostics.Process.Start(obj.ToString());
        }
    }
}
