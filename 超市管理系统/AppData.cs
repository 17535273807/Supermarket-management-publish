using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using 超市管理系统.Entity;
using 超市管理系统.Enums;

namespace 超市管理系统
{
    public class AppData : ObservableObject
    {
        /// <summary>
        /// 单例模式
        /// </summary>
        public static AppData Instance { get;  set; } = new Lazy<AppData>(() => new AppData()).Value;



        /// <summary>
        /// 当前登录的用户
        /// </summary>
        public Member CurrentUser { get; set; } = new Member();

        /// <summary>
        /// 当前登录的顾客
        /// </summary>
        public Customer CurrentCustomer { get; set; } = new Customer();
        /// <summary>
        /// 当前订单购物车
        /// </summary>
        public Order CurrentOrder { get;set; } = null;

        public string FullTitle => Title + "管理系统";

        public string Title => "某某超市";

        /// <summary>
        /// 背景颜色
        /// </summary>
        public SolidColorBrush Background => new SolidColorBrush(Color.FromRgb(41, 55, 82));

        /// <summary>
        /// 前景颜色
        /// </summary>
        public SolidColorBrush Foreground => new SolidColorBrush(Color.FromRgb(255, 255, 255));


        private CurrentUserType userType = CurrentUserType.管理员;
        /// <summary>
        /// 用户类别
        /// </summary>
        public CurrentUserType UserType
        {
            get { return userType; }
            set { userType = value; RaisePropertyChanged(); }
        }


    }
}
