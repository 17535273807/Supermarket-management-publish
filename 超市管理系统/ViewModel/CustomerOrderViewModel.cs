using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using 超市管理系统.Entity;

namespace 超市管理系统.ViewModel
{
    public class CustomerOrderViewModel : ViewModelBase2
    {

        private ObservableCollection<Order> orders = new ObservableCollection<Order>();
        public ObservableCollection<Order> OrderList
        {
            get { return orders; }
            set { orders = value; RaisePropertyChanged(); }
        }

        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    var _orders = OrderProvider.Current.GetAll().Where(t => t.CustomerId == AppData.CurrentCustomer.Id);
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
                        }
                    }
                });
            }
        }

        
        #endregion
    }
}
