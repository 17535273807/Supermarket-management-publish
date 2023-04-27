using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using 超市管理系统.Entity;
using 超市管理系统.Enums;
using 超市管理系统.View;

namespace 超市管理系统.ViewModel
{
    public class LoginViewModel : ViewModelBase2
    {

        private Member member = new Member();//字段
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
        /// 初始化
        /// </summary>
        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
#if DEBUG
                    Member.NameEx = "admin";
                    Member.PasswordEx = "1";
#endif
                });
            }
        }

        

        /// <summary>
        /// 顾客注册
        /// </summary>
        public RelayCommand<LoginView> RegisterCommand
        {
            get
            {
                return new RelayCommand<LoginView>((view) =>
                {
                    view.Hide();
                    var result = new SignupView().ShowDialog();
                    view.Show();
                });
            }
        }


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

                    if(AppData.UserType== CurrentUserType.管理员)
                    {
                        var list = MemberProvider.Current.GetAll();
                        var model = list.FirstOrDefault(t => t.Name == member.Name && t.Password == member.Password);
                        if (model != null)
                        {
                            AppData.CurrentUser = model;                            
                            new MainWindow().Show();//进入管理员的主窗体
                            view.Close();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码错误！");
                        }
                    }
                    else
                    {
                        var list = CustomerProvider.Current.GetAll();
                        var model = list.FirstOrDefault(t => t.Name == member.Name && t.Password == member.Password);
                        if (model != null)
                        {
                            AppData.CurrentCustomer = model; 
                            new CustomerWindow().Show();//进入顾客的主窗体
                            view.Close();
                        }
                        else
                        {
                            MessageBox.Show("用户名或密码错误！");
                        }
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

        /// <summary>
        /// 切换用户
        /// </summary>
        public RelayCommand<RadioButton> CheckedCommand
        {
            get
            {
                return new RelayCommand<RadioButton>((button) =>
                {
                    if (button == null) return;
                    if(button is RadioButton radioButton)
                    {
                        if (radioButton.Tag == null) return;
                        if(Enum.TryParse(radioButton.Tag.ToString(),false,out CurrentUserType result))
                        {
                            AppData.UserType= result;
#if DEBUG
                            if(result== CurrentUserType.顾客)
                            {
                                Member.NameEx = "张三";
                            }
                            else
                            {
                                Member.NameEx = "admin";
                            }
#endif
                        }
                    }
                });
            }
        }
        #endregion

    }
}
