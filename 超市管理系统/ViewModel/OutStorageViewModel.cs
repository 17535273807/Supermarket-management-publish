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

namespace 超市管理系统.ViewModel
{
    public class OutStorageViewModel : ViewModelBase2
    {

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


        private List<Stock> stockList = new List<Stock>();
        /// <summary>
        /// 所有出库记录
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
                    StockList = StockProvider.Current.GetAll().Where(t => t.Type == StockType.出库.ToString()).ToList();
                    ProductList = ProductProvider.Current.GetAll();
                    Product = null;
                    CurrentStock = null;
                });
            }
        }


        /// <summary>
        /// 加载所有商品
        /// </summary>
        public RelayCommand<UserControl> SelectCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    StockList = StockProvider.Current.GetAll().Where(t => t.Type == StockType.出库.ToString()).ToList();
                    CurrentStock = null;
                });
            }
        }



        /// <summary>
        /// 查询当前商品的出库记录
        /// </summary>
        public RelayCommand<UserControl> SearchCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    StockList = StockProvider.Current.GetAll().Where(t => t.Type == StockType.出库.ToString() && t.ProductId == Product.Id).ToList();
                    CurrentStock = null;
                });
            }
        }
        #endregion
    }
}
