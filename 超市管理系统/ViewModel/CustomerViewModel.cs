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
    public class CustomerViewModel: ViewModelBase2
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

        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    CustomerList = CustomerProvider.Current.GetAll();
                    Customer = null; 
                });
            }
        }

        /// <summary>
        /// 新增顾客
        /// </summary>
        public RelayCommand<UserControl> OpenAddViewCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    AddCustomerView addCustomerView=new AddCustomerView();
                    if (addCustomerView.ShowDialog().Value == true)
                    {
                        CustomerList = CustomerProvider.Current.GetAll();
                    }
                });
            }
        }
        /// <summary>
        /// 删除当前选中的顾客
        /// </summary>
        public RelayCommand<UserControl> DeleteCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {

                    if (Customer == null) return;
                    if (Dialog.Show())
                    {
                        var count = CustomerProvider.Current.Delete(Customer);
                        if (count > 0)
                        {
                            MessageBox.Show("操作成功！");
                            CustomerList = CustomerProvider.Current.GetAll();
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
                    int count = CustomerProvider.Current.Save();
                    if(count> 0)
                    {
                        MessageBox.Show("保存成功");
                    }
                });
            }
        }

        /// <summary>
        /// 修改顾客
        /// </summary>
        public RelayCommand<UserControl> EditCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    if (Customer == null) return;
                    var vm = ServiceLocator.Current.GetInstance<EditCustomerViewModel>();
                    vm.Customer = Customer;
                    EditCustomerView editCustomerView = new EditCustomerView();
                    if (editCustomerView.ShowDialog().Value == true)
                    {
                        CustomerList = CustomerProvider.Current.GetAll();
                    }
                });
            }
        }
        
        #endregion
    }
}
