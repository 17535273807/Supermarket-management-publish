using CommonServiceLocator;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using 超市管理系统.Entity;
using 超市管理系统.View;

namespace 超市管理系统.ViewModel
{
    //combobox绑定数据源
    public class MemberViewModel : ViewModelBase2
    {
        private MemberProvider memberProvider = new MemberProvider();

        private List<Member> memberList = new List<Member>();
        public List<Member> MemberList
        {
            get { return memberList; }
            set { memberList = value; RaisePropertyChanged(); }
        }

        private Member member;
        /// <summary>
        ///  当前选中的用户实体
        /// </summary>
        public Member Member
        {
            get { return member; }
            set { member = value; RaisePropertyChanged(); }
        }

        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    MemberList = memberProvider.GetAll();
                    Member = null;
                });
            }
        }

        /// <summary>
        /// 新增顾客
        /// </summary>
        public RelayCommand<UserControl> OpenAddViewCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    AddMemberView window = new AddMemberView();
                    if (window.ShowDialog().Value == true)
                    {
                        MemberList = memberProvider.GetAll();
                    }
                });
            }
        }
        /// <summary>
        /// 删除当前选中的用户
        /// </summary>
        public RelayCommand<UserControl> DeleteCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {

                    if (Member == null) return;
                    if (Dialog.Show())
                    {
                        var count = memberProvider.Delete(Member);
                        if (count > 0)
                        {
                            MessageBox.Show("操作成功！");
                            MemberList = memberProvider.GetAll();
                        }
                    }

                });
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        public RelayCommand<UserControl> SaveCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    int count = memberProvider.Save();
                    if (count > 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                });
            }
        }

        /// <summary>
        /// 修改顾客
        /// </summary>
        public RelayCommand<UserControl> EditCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    if (Member == null) return;
                    var vm = ServiceLocator.Current.GetInstance<EditMemberViewModel>();
                    vm.Member = Member;
                    EditMemberView window = new EditMemberView();
                    if (window.ShowDialog().Value == true)
                    {
                        MemberList = memberProvider.GetAll();
                    }
                });
            }
        }

        #endregion
    }
}
