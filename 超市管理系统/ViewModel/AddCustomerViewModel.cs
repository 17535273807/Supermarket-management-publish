using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using 超市管理系统.Entity;

namespace 超市管理系统.ViewModel
{
    public  class AddCustomerViewModel : ViewModelBase2
    {
        private CustomerProvider customerProvider = new CustomerProvider();

        private Customer customer;
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; RaisePropertyChanged(); }
        }




        public RelayCommand<Window> LoadedCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    Customer = new Customer();
                });
            }
        }

        public RelayCommand<Window> OkCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    if(string.IsNullOrEmpty(Customer.Name))
                    {
                        MessageBox.Show("姓名不能为空!");
                        return; 
                    }

                    if (string.IsNullOrEmpty(Customer.Telephone))
                    {
                        MessageBox.Show("电话不能为空!");
                        return;
                    }

                    if (string.IsNullOrEmpty(Customer.Address))
                    {
                        MessageBox.Show("地址不能为空!");
                        return;
                    }

                    Customer.InsertDate = DateTime.Now;
                    int count = customerProvider.Insert(Customer);
                    if(count > 0)
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
