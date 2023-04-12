using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using 超市管理系统.Entity;

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
    }
}
