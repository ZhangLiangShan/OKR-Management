using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Zhaoxi.ComputerMonitor.Common;
using Zhaoxi.ComputerMonitor.DataAccess;
using Zhaoxi.ComputerMonitor.Model;

namespace Zhaoxi.ComputerMonitor.ViewModel
{
    public class LoginViewModel : NotifyPropertyBase
    {
        public LoginModel LoginModel { get; set; } = new LoginModel();

        public CommandBase CloseCommand { get; set; }
        public CommandBase LoginCommand { get; set; }

        private Visibility _showProgress = Visibility.Collapsed;

        public Visibility ShowProgress
        {
            get { return _showProgress; }
            set
            {
                _showProgress = value;
                this.Notify();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                this.Notify();
            }
        }
        private Visibility _showError;

        public Visibility ShowError
        {
            get { return _showError; }
            set
            {
                _showError = value;
                this.Notify();
            }
        }



        public LoginViewModel()
        {
            this.LoginCommand = new CommandBase();
            this.LoginCommand.ExcuteAction = new Action<object>(DoLogin);
            this.LoginCommand.CanExecuteFunc = new Func<bool>(() =>
            {
                return ShowProgress == Visibility.Collapsed;
            });

            this.CloseCommand = new CommandBase();
            this.CloseCommand.ExcuteAction = new Action<object>(DoClose);
            this.CloseCommand.CanExecuteFunc = new Func<bool>(() => { return true; });

        }

        private void DoClose(object o)
        {
            (o as Window).DialogResult = false;
        }
        private void DoLogin(object o)
        {
            this.ShowProgress = Visibility.Visible;
            this.ShowError = Visibility.Collapsed;

            if (string.IsNullOrEmpty(LoginModel.UserName))
            {
                this.ErrorMessage = "请输入用户名";
                this.ShowError = Visibility.Visible;
                this.ShowProgress = Visibility.Collapsed;
                return;
            }
            if (string.IsNullOrEmpty(LoginModel.Password))
            {
                this.ErrorMessage = "请输入密码";
                this.ShowError = Visibility.Visible;
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            if (string.IsNullOrEmpty(LoginModel.ValidCode) ||
                LoginModel.ValidCode.ToLower() != "etu4")
            {
                this.ErrorMessage = "验证码输入不正确";
                this.ShowError = Visibility.Visible;
                this.ShowProgress = Visibility.Collapsed;
                return;
            }

            Task.Run(new Action(async () =>
            {
                await Task.Delay(2000);
                try
                {
                    var userInfo = LocalDataAccess.GetInstance().CheckUserInfo(LoginModel.UserName, LoginModel.Password);
                    if (userInfo == null)
                        throw new Exception("登录失败！用户名或密码错误！");

                    GlobalValues.UserInfo = userInfo;

                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        (o as Window).DialogResult = true;
                    }));
                }
                catch (Exception ex)
                {
                    this.ErrorMessage = "登录失败！" + ex.Message;
                    this.ShowError = Visibility.Visible;
                }
                finally
                {
                    Application.Current.Dispatcher.Invoke(new Action(() =>
                    {
                        this.ShowProgress = Visibility.Collapsed;
                    }));
                }
            }));
        }
    }
}
