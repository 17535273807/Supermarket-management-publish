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
    public class SupplierViewModel : ViewModelBase2
    {
        private SupplierProvider supplierProvider = new SupplierProvider();

        private List<Supplier> supplierList = new List<Supplier>();
        public List<Supplier> SupplierList
        {
            get { return supplierList; }
            set { supplierList = value; RaisePropertyChanged(); }
        }

        private Supplier supplier;
        /// <summary>
        ///  当前选中的供应商
        /// </summary>
        public Supplier Supplier
        {
            get { return supplier; }
            set { supplier = value; RaisePropertyChanged(); }
        }

        #region commands

        /// <summary>
        /// 加载所有供应商
        /// </summary>
        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    SupplierList = supplierProvider.GetAll();
                    Supplier = null;
                });
            }
        }

        /// <summary>
        /// 新增供应商
        /// </summary>
        public RelayCommand<UserControl> OpenAddViewCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {
                    AddSupplierView window = new AddSupplierView();
                    if (window.ShowDialog().Value == true)
                    {
                        SupplierList = supplierProvider.GetAll();
                    }
                });
            }
        }
        /// <summary>
        /// 删除当前选中的供应商
        /// </summary>
        public RelayCommand<UserControl> DeleteCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {

                    if (Supplier == null) return;
                    if (Dialog.Show())
                    {
                        var count = supplierProvider.Delete(Supplier);
                        if (count > 0)
                        {
                            MessageBox.Show("操作成功！");
                            SupplierList = supplierProvider.GetAll();
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
                    int count = supplierProvider.Save();
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
                    if (Supplier == null) return;
                    var vm = ServiceLocator.Current.GetInstance<EditSupplierViewModel>();
                    vm.Supplier = Supplier;
                    EditSupplierView window = new EditSupplierView();
                    if (window.ShowDialog().Value == true)
                    {
                        SupplierList = supplierProvider.GetAll();
                    }
                });
            }
        }

        #endregion
    }
}
