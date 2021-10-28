using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zhaoxi.ComputerMonitor.Common;

namespace Zhaoxi.ComputerMonitor.ViewModel
{
    public class MainWindowViewModel : NotifyPropertyBase
    {
        public event EventHandler<object> ShowProfileViewEvent;

        public CommandBase ChangeContentCommand { get; set; }
        public CommandBase ShowProfileCommand { get; set; }
        public CommandBase ShowWaitforCommand { get; set; }

        private FrameworkElement childContent;

        public FrameworkElement ChildContent
        {
            get { return childContent; }
            set { childContent = value; this.Notify(); }
        }

        private bool isShowWait;

        public bool IsShowWait
        {
            get { return isShowWait; }
            set { isShowWait = value; this.Notify(); }
        }
        private string _avatar;

        public string Avatar
        {
            get { return _avatar; }
            set { _avatar = value; this.Notify(); }
        }
        private string _realName;

        public string RealName
        {
            get { return _realName; }
            set { _realName = value; this.Notify(); }
        }
        private int _gender;

        public int Gender
        {
            get { return _gender; }
            set { _gender = value; this.Notify(); }
        }



        public MainWindowViewModel()
        {
            this.IsShowWait = false;

            this.ChangeContentCommand = new CommandBase();
            this.ChangeContentCommand.ExcuteAction = new Action<object>(DoChangeContent);
            this.ChangeContentCommand.CanExecuteFunc = new Func<bool>(() => true);

            this.ShowProfileCommand = new CommandBase();
            this.ShowProfileCommand.ExcuteAction = new Action<object>(ShowProfile);
            this.ShowProfileCommand.CanExecuteFunc = new Func<bool>(() => true);

            this.ShowWaitforCommand = new CommandBase();
            this.ShowWaitforCommand.ExcuteAction = new Action<object>(ShowWaitfor);
            this.ShowWaitforCommand.CanExecuteFunc = new Func<bool>(() => true);

            this.DoChangeContent("FirstPageView");

            this.RealName = GlobalValues.UserInfo.RealName;
            this.Avatar = GlobalValues.UserInfo.Avatar;
            this.Gender = GlobalValues.UserInfo.Gender;
        }

        private void DoChangeContent(object param)
        {
            // 初始化需要显示的Content对象
            Type type = Type.GetType("Zhaoxi.ComputerMonitor.View." + param.ToString());
            ConstructorInfo ct1 = type.GetConstructor(System.Type.EmptyTypes);
            this.ChildContent = (FrameworkElement)ct1.Invoke(null);
        }

        private void ShowProfile(object param)
        {
            ShowProfileViewEvent?.Invoke(this, param);
        }

        private void ShowWaitfor(object param)
        {
            Task.Run(new Action(async () =>
            {
                this.IsShowWait = true;

                await Task.Delay(5000);

                this.IsShowWait = false;
            }));
        }
    }
}
