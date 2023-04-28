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
    public class ShoppingCartViewModel : ViewModelBase2
    {
        private double _price;
        /// <summary>
        /// 购物车合计总价格
        /// </summary>
        public double SumPrice
        {
            get { return _price; }
            set { _price = value; RaisePropertyChanged(); }
        }



        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    SumPrice = 0;
                    if (AppData.CurrentOrder == null) return;
                    SumPrice = AppData.CurrentOrder.Children.Sum(x => x.Price * x.Quantity);
                });
            }
        }

        /// <summary>
        /// 移出购物车
        /// </summary>
        public RelayCommand<OrderDetail> RemoveCommand
        {
            get
            {
                return new RelayCommand<OrderDetail>((model) =>
                {
                    AppData.CurrentOrder.Children.Remove(model);
                    OrderDetailProvider.Current.Delete(model);
                    CalcSumPrice();
                });
            }
        }

        /// <summary>
        /// 增加数量
        /// </summary>
        public RelayCommand<OrderDetail> AddCommand
        {
            get
            {
                return new RelayCommand<OrderDetail>((model) =>
                {
                    //判断库存
                    var product = ProductProvider.Current.GetAll().FirstOrDefault(t => t.Id == model.ProductId);
                    if (product != null)
                    {
                        if (product.Quantity <= model.QuantityEx)
                        {
                            MessageBox.Show("库存不足");
                            return;
                        }
                        //增加数量
                        model.QuantityEx += 1;
                        OrderDetailProvider.Current.Update(model);
                        CalcSumPrice();
                    }

                    
                });
            }
        }
        /// <summary>
        /// 减少数量
        /// </summary>
        public RelayCommand<OrderDetail> DecCommand
        {
            get
            {
                return new RelayCommand<OrderDetail>((model) =>
                {
                    if (model.QuantityEx == 1) return;
                    model.QuantityEx -= 1;
                    OrderDetailProvider.Current.Update(model);
                    CalcSumPrice();
                });
            }
        }

        /// <summary>
        /// 结算购物车
        /// </summary>
        public RelayCommand PayCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (AppData.CurrentOrder.Children.Count == 0)
                    {
                        MessageBox.Show("请选择商品");
                        return;
                    }

                    int count = 0;
                    foreach (var item in AppData.CurrentOrder.Children)
                    {
                        //增加出库记录
                        Stock stock = new Stock();
                        stock.ProductId = item.ProductId;
                        stock.Quantity = item.Quantity;
                        stock.Type = StockType.出库.ToString();
                        stock.InsertDate = DateTime.Now;
                        count += StockProvider.Current.Insert(stock);

                        //修改库存
                        var product = ProductProvider.Current.GetAll().FirstOrDefault(t => t.Id == item.ProductId);
                        if (product != null)
                        {
                            product.Quantity -= item.Quantity;
                            ProductProvider.Current.Update(product);  
                        }
                    }
                    AppData.CurrentOrder.PayDate = DateTime.Now;
                    AppData.CurrentOrder.OrderState = OrderState.已完成.ToString();
                    count += OrderProvider.Current.Update(AppData.CurrentOrder);

                    if (count == AppData.CurrentOrder.Children.Count + 1)
                    {
                        MessageBox.Show("恭喜您，购物成功");
                        AppData.CurrentOrder.Children.Clear();
                        AppData.CurrentOrder = null;
                        return;
                    }
                });
            }
        }

        public RelayCommand ClearCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    AppData.CurrentOrder.Children.Clear();
                });
            }
        }

        

        #endregion

        /// <summary>
        /// 合计函数
        /// </summary>
        private void CalcSumPrice()
        {
            SumPrice = AppData.CurrentOrder.Children.Sum(x => x.Price * x.Quantity);
        }
    }
}
