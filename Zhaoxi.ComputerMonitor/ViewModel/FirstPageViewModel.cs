using LiveCharts;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.ComputerMonitor.Common;
using Zhaoxi.ComputerMonitor.DataAccess;
using Zhaoxi.ComputerMonitor.Model;

namespace Zhaoxi.ComputerMonitor.ViewModel
{
    public class FirstPageViewModel : NotifyPropertyBase
    {
        List<Task> taskList = new List<Task>();

        public SeriesCollection SeriesCollection { get; set; }
        public SeriesCollection SeriesCollection1 { get; set; }
        public ChartValues<ObservableValue> MemValues { get; set; } = new ChartValues<ObservableValue>();
        public ChartValues<ObservableValue> CPUValues { get; set; }

        private double currentCpu;

        public double CurrentCPU
        {
            get { return currentCpu; }
            set
            {
                currentCpu = Math.Ceiling(value);

                this.CPUValues.Add(new ObservableValue(currentCpu));
                this.CPUValues.RemoveAt(0);

                this.Notify("CurrentCPU");
            }
        }

        private double currnetMem;

        public double CurrentMem
        {
            get { return currnetMem; }
            set
            {
                currnetMem = Math.Ceiling(value);

                this.MemValues.Add(new ObservableValue(currnetMem));
                this.MemValues.RemoveAt(0);

                this.Notify("CurrentMem");
            }
        }

        public ObservableCollection<CourseSeriesModel> CourseList { get; set; } = new ObservableCollection<CourseSeriesModel>();

        public FirstPageViewModel()
        {
            Task.Run(new Action(() =>
            {
                this.InitCPUDatas();
                this.InitCourseList();
                this.InitMemDatas();
            }));
        }

        private void InitCourseList()
        {
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                foreach (var item in LocalDataAccess.GetInstance().GetCoursePlayRecord())
                    this.CourseList.Add(item);
            }));
        }

        bool taskSwitch = true;
        Random random = new Random();
        private void InitCPUDatas()
        {
            this.CPUValues = new ChartValues<ObservableValue>();
            for (int i = 0; i < 40; i++)
            {
                this.CPUValues.Add(new ObservableValue(0.0f));
            }

            ComputerInfo monitor = ComputerInfo.GetInstance();

            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    this.CurrentCPU = monitor.GetCPUInfo();
                    await Task.Delay(1000);
                }
            }));

            this.taskList.Add(task);
        }

        private void InitMemDatas()
        {
            this.MemValues.Clear();
            for (int i = 0; i < 40; i++)
            {
                this.MemValues.Add(new ObservableValue(0.0f));
            }

            //ComputerInfo monitor = ComputerInfo.GetInstance();
            var task = Task.Factory.StartNew(new Action(async () =>
            {
                while (taskSwitch)
                {
                    this.CurrentMem = random.Next((int)Math.Max(this.CurrentMem - 5, 0), (int)(this.CurrentMem + 5));
                    //monitor.GetMemInfo();
                    await Task.Delay(1000);
                }
            }));
            this.taskList.Add(task);
        }

        public void Dispose()
        {
            try
            {
                this.taskSwitch = false;
                Task.WaitAll(this.taskList.ToArray());
                ComputerInfo.GetInstance().Dispose();
            }
            catch { }

        }
    }
}
