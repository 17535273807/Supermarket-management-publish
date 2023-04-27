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
    /// CustomerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CustomerWindow : Window
    {
        public CustomerWindow()
        {
            InitializeComponent();
        }

        private void View_Checked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton radioButton)
            {
                if (string.IsNullOrEmpty(radioButton.Name)) return;

                //第二种导航
                container.Content = Activator.CreateInstance(Type.GetType("超市管理系统.View." + radioButton.Name));
            }
        }
    }
}
