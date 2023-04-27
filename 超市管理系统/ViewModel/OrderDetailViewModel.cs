using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using 超市管理系统.Entity;
using 超市管理系统.Enums;

namespace 超市管理系统.ViewModel
{
    public class OrderDetailViewModel : ViewModelBase2
    {
        private List<Customer> customerList = new List<Customer>();
        public List<Customer> CustomerList
        {
            get { return customerList; }
            set { customerList = value; RaisePropertyChanged(); }
        }

        private Customer customer;
        /// <summary>
        ///  当前选中的顾客实体
        /// </summary>
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; RaisePropertyChanged(); }
        }


        private ObservableCollection<Order> orders = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrderList
        {
            get { return orders; }
            set { orders = value; RaisePropertyChanged(); }
        }

        private double sumMoney = 0;
        /// <summary>
        /// 销售总计
        /// </summary>
        public double SumMoney
        {
            get { return sumMoney; }
            set { sumMoney = value; RaisePropertyChanged(); }
        }

        private int sumOrderCount = 0;
        /// <summary>
        /// 销售总计
        /// </summary>
        public int SumOrderCount
        {
            get { return sumOrderCount; }
            set { sumOrderCount = value; RaisePropertyChanged(); }
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
                    InitCustomer();
                    InitOrder();

                });
            }
        }

       

        /// <summary>
        /// 加载所有顾客的订单
        /// </summary>
        public RelayCommand<UserControl> SelectCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    InitOrder();
                });
            }
        }



        /// <summary>
        /// 查询当前顾客的所有订单
        /// </summary>
        public RelayCommand<UserControl> SearchCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    if (Customer == null) 
                        return;
                    InitOrder(Customer);
                });
            }
        }
        #endregion


        /// <summary>
        /// 加载所有顾客
        /// </summary>
        private void InitCustomer()
        {
            CustomerList = CustomerProvider.Current.GetAll();
        }




        /// <summary>
        /// 加载订单详情
        /// </summary>
        /// <param name="customer"></param>
        private void InitOrder(Customer customer = null)
        {
            SumMoney = 0;
            SumOrderCount = 0;
            if (customer == null)
            {
                var _orders = OrderProvider.Current.GetAll();//加载所有顾客订单
                if (_orders != null)
                {
                    OrderList.Clear();
                    foreach (var order in _orders)
                    {
                        OrderList.Add(order);

                        //加载订单详情
                        var _details = OrderDetailProvider.Current.GetAll().Where(t => t.OrderId == order.Id).ToList();
                        if (_details != null)
                        {
                            order.Children.Clear();
                            foreach (var detail in _details)
                            {
                                order.Children.Add(detail);
                            }
                        }

                        //合计
                        SumMoney += order.Children.Sum(t => t.Quantity * t.Price);                       
                    }

                    SumOrderCount = _orders.Count;
                }
            }
            else
            {
                var _orders = OrderProvider.Current.GetAll().Where(t => t.CustomerId == Customer.Id).ToList();//加载当前顾客订单
                if (_orders != null)
                {
                    OrderList.Clear();
                    foreach (var order in _orders)
                    {
                        OrderList.Add(order);

                        //加载订单详情
                        var _details = OrderDetailProvider.Current.GetAll().Where(t => t.OrderId == order.Id).ToList();
                        if (_details != null)
                        {
                            order.Children.Clear();
                            foreach (var detail in _details)
                            {
                                order.Children.Add(detail);
                            }
                        }

                        //合计
                        SumMoney += order.Children.Sum(t => t.Quantity * t.Price);
                    }

                    SumOrderCount = _orders.Count;
                }
            }
            
        }
    }
}
