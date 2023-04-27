using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using 超市管理系统.Entity;
using 超市管理系统.Enums;

namespace 超市管理系统.ViewModel
{
    public  class IndexViewModel : ViewModelBase2
    {
        private ObservableCollection<Product> productList = new ObservableCollection<Product>();
        /// <summary>
        /// 库存提示
        /// </summary>
        public ObservableCollection<Product> ProductList
        {
            get { return productList; }
            set { productList = value; RaisePropertyChanged(); }
        }

        private ObservableCollection<Stock> salesList = new ObservableCollection<Stock>();
        /// <summary>
        /// 销售排行榜
        /// </summary>
        public ObservableCollection<Stock> SalesList
        {
            get { return salesList; }
            set { salesList = value; RaisePropertyChanged(); }
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


        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    //销售排行榜
                    var stocks = StockProvider.Current.GetAll().Where(t => t.Type == StockType.出库.ToString()).ToList();
                    var group = stocks.GroupBy(t => t.ProductId);
                    ObservableCollection<Stock> mylist = new ObservableCollection<Stock>();
                    foreach (var list in group)
                    {
                        Stock stock = new Stock();
                        foreach (var item in list)
                        {
                            stock.ProductId = item.ProductId;
                            stock.Type = item.Type;
                            stock.Quantity += item.Quantity;
                        }

                        mylist.Add(stock);
                    }
                    var descending = mylist.OrderByDescending(t=>t.Quantity).ToList();
                    
                    SalesList.Clear();
                    int index = 1;
                    descending.ForEach(t => 
                    {
                        SalesList.Add(t);
                        t.Id= index++;
                    });


                    //库存提示
                    var products = ProductProvider.Current.GetAll().Where(t=>t.Quantity==0).ToList();
                    ProductList.Clear();
                    products.ForEach(t=>ProductList.Add(t));
                });
            }
        }

        #endregion



        
    }
}
