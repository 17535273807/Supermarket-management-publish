using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 超市管理系统.Entity;

namespace 超市管理系统.ViewModel
{
    public class EditProductViewModel : ViewModelBase2
    {
        private ProductProvider productProvider = new ProductProvider();

        private Product product;
        /// <summary>
        ///  当前选中的商品
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> OkCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    if (string.IsNullOrEmpty(Product.Name))
                    {
                        MessageBox.Show("商品名不能为空!");
                        return;
                    }                    

                    int count = productProvider.Update(Product);
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
