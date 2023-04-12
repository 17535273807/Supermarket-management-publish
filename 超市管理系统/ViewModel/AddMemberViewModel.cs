using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 超市管理系统.Entity;
using 超市管理系统.Enums;

namespace 超市管理系统.ViewModel
{
    public class AddMemberViewModel : ViewModelBase2
    {
        private MemberProvider memberProvider = new MemberProvider();

        private Member member;
        /// <summary>
        ///  当前选中的用户
        /// </summary>
        public Member Member
        {
            get { return member; }
            set { member = value; RaisePropertyChanged(); }
        }

        private LevelType levelType;
        /// <summary>
        ///  当前选中的用户等级
        /// </summary>
        public LevelType LevelType
        {
            get { return levelType; }
            set { levelType = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> LoadedCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    Member = new Member();
                });
            }
        }

        public RelayCommand<Window> OkCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    if (string.IsNullOrEmpty(Member.Name))
                    {
                        MessageBox.Show("姓名不能为空!");
                        return;
                    }

                    if (string.IsNullOrEmpty(Member.Password))
                    {
                        MessageBox.Show("密码不能为空!");
                        return; 
                    }


                    if (string.IsNullOrEmpty(Member.Level))
                    {
                        MessageBox.Show("等级不能为空!");
                        return;
                    }

                    Member.InsertDate = DateTime.Now;
                    int count = memberProvider.Insert(Member);
                    if (count > 0)
                    {
                        MessageBox.Show("操作成功!");
                    }
                    view.DialogResult = true;
                    view.Close();
                });
            }
        }


        public RelayCommand<Window> ExitCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    view.Close();
                });
            }
        }
    }
}
