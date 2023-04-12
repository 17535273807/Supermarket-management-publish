using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace 超市管理系统.View
{
    /// <summary>
    /// Dialog.xaml 的交互逻辑
    /// </summary>
    public partial class Dialog : Window
    {
        public static new bool Show()
        {
            Dialog dialog = new Dialog();
            var result = dialog.ShowDialog();
            return dialog.IsOK;
        }

        public bool IsOK = false;
        public Dialog()
        {
            InitializeComponent();
        }

        private void Button_ClickOK(object sender, RoutedEventArgs e)
        {
            IsOK = true;
            Close();
        }

        private void Button_ClickCancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }
    }
}
