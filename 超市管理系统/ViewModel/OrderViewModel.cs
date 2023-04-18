using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using 超市管理系统.Entity;

namespace 超市管理系统.ViewModel
{
    public class OrderViewModel : ViewModelBase2
    {
        private List<Product> productList = new List<Product>();
        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; RaisePropertyChanged(); }
        }

        private Product product;
        /// <summary>
        ///  当前选中的商品
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(); }
        }


        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    ProductList = ProductProvider.Current.GetAll();
                });
            }
        }

        #endregion
    }
}
