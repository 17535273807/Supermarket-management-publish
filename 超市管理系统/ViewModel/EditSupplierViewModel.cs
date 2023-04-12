using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 超市管理系统.Entity;

namespace 超市管理系统.ViewModel
{
    public class EditSupplierViewModel : ViewModelBase2
    {
        private SupplierProvider supplierProvider = new SupplierProvider();

        private Supplier supplier;
        /// <summary>
        ///  当前选中的供应商
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; RaisePropertyChanged(); }
        }


        public RelayCommand<Window> OkCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    if (string.IsNullOrEmpty(Supplier.Name))
                    {
                        MessageBox.Show("姓名不能为空!");
                        return;
                    }

                    if (string.IsNullOrEmpty(Supplier.Telephone))
                    {
                        MessageBox.Show("电话不能为空!");
                        return;
                    }

                    if (string.IsNullOrEmpty(Supplier.Address))
                    {
                        MessageBox.Show("地址不能为空!");
                        return;
                    }

                    int count = supplierProvider.Update(Supplier);
                    if (count > 0)
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
