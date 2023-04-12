using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 超市管理系统.Entity;
using 超市管理系统.View;

namespace 超市管理系统.ViewModel
{
    public class LoginViewModel : ViewModelBase2
    {
        MemberProvider memberProvider = new MemberProvider();

        

        private Member member = new Member() { Name="admin", Password="1"};//字段
        /// <summary>
        /// 用户实体
        /// </summary>
        public Member Member
        {
            get { return member; }
            set { member = value; RaisePropertyChanged(); }
        }


        #region commands


        /// <summary>
        /// 用户登录 
        /// </summary>
        public RelayCommand<LoginView> LoginCommand
        {
            get
            {
                return new RelayCommand<LoginView>((view) =>
                {
                    if (string.IsNullOrEmpty(member.Name) || string.IsNullOrEmpty(member.Password))
                    {
                        return;
                    }

                    var list = memberProvider.GetAll();
                    var model = list.FirstOrDefault(t => t.Name == member.Name && t.Password == member.Password);
                    if (model != null)
                    {
                        AppData.CurrentUser= model;

                        //进入主窗体

                        new MainWindow().Show();
                        view.Close();
                    }
                    else
                    {
                        MessageBox.Show("用户名或密码错误！");
                    }
                });
            }
        }

        public RelayCommand<LoginView> ExitCommand
        {
            get
            {
                return new RelayCommand<LoginView>((view) =>
                {
                    view.Close();//Environment.Exit(0);
                });
            }
        }
        

        #endregion

    }
}
