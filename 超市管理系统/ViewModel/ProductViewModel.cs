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
    public class ProductViewModel : ViewModelBase2
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

        /// <summary>
        /// 加载所有商品
        /// </summary>
        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    ProductList = ProductProvider.Current.GetAll();
                    Product = null;
                });
            }
        }

        /// <summary>
        /// 新增商品
        /// </summary>
        public RelayCommand<UserControl> OpenAddViewCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    AddProductView window = new AddProductView();
                    if (window.ShowDialog().Value == true)
                    {
                        ProductList = ProductProvider.Current.GetAll();
                    }
                });
            }
        }
        /// <summary>
        /// 删除当前选中的商品
        /// </summary>
        public RelayCommand<UserControl> DeleteCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {

                    if (Product == null) return;
                    if (Dialog.Show())
                    {
                        var count = ProductProvider.Current.Delete(Product);
                        if (count > 0)
                        {
                            MessageBox.Show("操作成功！");
                            ProductList = ProductProvider.Current.GetAll();
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
                    int count = ProductProvider.Current.Save();
                    if (count > 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                });
            }
        }

        /// <summary>
        /// 修改供应商
        /// </summary>
        public RelayCommand<UserControl> EditCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    if (Product == null) return;
                    var vm = ServiceLocator.Current.GetInstance<EditProductViewModel>();
                    vm.Product = Product;
                    EditProductView window = new EditProductView();
                    if (window.ShowDialog().Value == true)
                    {
                        ProductList = ProductProvider.Current.GetAll();
                    }
                });
            }
        }

        #endregion
    }
}
