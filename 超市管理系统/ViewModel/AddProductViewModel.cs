using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using 超市管理系统.Entity;
using 超市管理系统.Enums;

namespace 超市管理系统.ViewModel
{
    public class AddProductViewModel : ViewModelBase2
    {
        private ProductProvider productProvider = new ProductProvider();
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

        private Product product;
        /// <summary>
        ///  当前选中的商品
        /// </summary>
        public Product Product
        {
            get { return product; }
            set { product = value; RaisePropertyChanged(); }
        }       

        public RelayCommand<Window> LoadedCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    Product = new Product();
                    SupplierList = supplierProvider.GetAll();
                });
            }
        }

        public RelayCommand<Window> OkCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    if (string.IsNullOrEmpty(Product.Name))
                    {
                        MessageBox.Show("商品名不能为空!");
                        return;
                    }

                    Product.InsertDate = DateTime.Now;
                    int count = productProvider.Insert(Product);
                    if (count > 0)
                    {
                        MessageBox.Show("操作成功!");
                    }
                    view.DialogResult = true;
                    view.Close();
                });
            }
        }

        /// <summary>
        /// 选择图片
        /// </summary>
        public RelayCommand<Window> SelectImageCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    //打开的文件选择对话框上的标题
                    openFileDialog.Title = "请选择文件";
                    //设置文件类型
                    openFileDialog.Filter = "图片文件(*.jpg)|*.jpg|所有文件(*.*)|*.*";
                    //设置默认文件类型显示顺序
                    openFileDialog.FilterIndex = 1;
                    //保存对话框是否记忆上次打开的目录
                    openFileDialog.RestoreDirectory = true;
                    //设置是否允许多选
                    openFileDialog.Multiselect = false;
                    //按下确定选择的按钮
                    if (openFileDialog.ShowDialog().Value ==true)
                    {
                        //获得文件路径
                        string localFilePath = openFileDialog.FileName.ToString();
                        //获取文件路径，不带文件名
                        //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                        //获取文件名，带后缀名，不带路径
                        string fileNameWithSuffix = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                        //去除文件后缀名
                        string fileNameWithoutSuffix = fileNameWithSuffix.Substring(0, fileNameWithSuffix.LastIndexOf("."));
                        //在文件名里加字符
                        string newFileName = localFilePath.Insert(1, "dameng");
                    }
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
