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
using 超市管理系统.Enums;
using 超市管理系统.View;

namespace 超市管理系统.ViewModel
{
    public class InstorageViewModel : ViewModelBase2
    {
        //private ProductProvider productProvider = new ProductProvider();
        private StockProvider stockProvider = new StockProvider();
        

        private List<Product> productList = new List<Product>();
        public List<Product> ProductList
        {
            get { return productList; }
            set { productList = value; RaisePropertyChanged(); }
        }

        private Product product = null;
        /// <summary>
        ///  当前选中的商品
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(); }
        }

        private Stock stock;
        /// <summary>
        ///  当前入库记录实体
        /// </summary>
        public Stock Stock
        {
            get { return stock; }
            set { stock = value; RaisePropertyChanged(); }
        }


        private List<Stock> stockList = new List<Stock>();
        /// <summary>
        /// 所有入库记录
        /// </summary>
        public List<Stock> StockList
        {
            get { return stockList; }
            set { stockList = value; RaisePropertyChanged(); }
        }

        private Stock currentStock;
        /// <summary>
        ///  当前选中的入库记录实体
        /// </summary>
        public Stock CurrentStock
        {
            get { return currentStock; }
            set { currentStock = value; RaisePropertyChanged(); }
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
                    StockList = stockProvider.GetAll();
                    ProductList = ProductViewModel.productProvider.GetAll();
                    Product = null;
                    Stock = new Stock() 
                    { 
                        Type = StockType.入库.ToString()                         
                    };
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
                        ProductList = ProductViewModel.productProvider.GetAll();
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
                        var count = ProductViewModel.productProvider.Delete(Product);
                        if (count > 0)
                        {
                            MessageBox.Show("操作成功！");
                            ProductList = ProductViewModel.productProvider.GetAll();
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
                    if (Product == null)
                    {
                        MessageBox.Show("请选择入库商品");
                        return;
                    }
                    if (Stock.Quantity <= 0)
                    {
                        MessageBox.Show("入库数量应大于零");
                        return;
                    }
                    Stock.InsertDate = DateTime.Now;
                    Stock.ProductId = Product.Id;
                    int count = stockProvider.Insert(Stock);
                    if (count > 0)
                    { 
                        //增加库存
                        Product.Quantity += Stock.Quantity;
                        ProductViewModel.productProvider.Update(Product);

                        //刷新界面
                        StockList = stockProvider.GetAll();

                        MessageBox.Show("保存成功");
                        Stock = new Stock() { Type = StockType.入库.ToString() };
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
                        ProductList = ProductViewModel.productProvider.GetAll();
                    }
                });
            }
        }

        #endregion
    }
}
