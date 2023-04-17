using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using 超市管理系统.Entity;
using 超市管理系统.Helper;

namespace 超市管理系统.ViewModel
{
    public class EditProductViewModel : ViewModelBase2
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

        private BitmapImage imageSource;
        /// <summary>
        ///  当前商品图片
        /// </summary>
        public BitmapImage ImageSource
        {
            get { return imageSource; }
            set { imageSource = value; RaisePropertyChanged(); }
        }

        public RelayCommand<Window> LoadedCommand
        {
            get
            {
                return new RelayCommand<Window>((view) =>
                {
                    ImageSource = Product.BitmapImage;
                    SupplierList = supplierProvider.GetAll();
                    Supplier = SupplierList.FirstOrDefault(t => t.Id == Product.SupplierId);
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

                    Product.SupplierId = Supplier.Id;

                    int count = productProvider.Update(Product);
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
                    if (openFileDialog.ShowDialog().Value == true)
                    {
                        //获得文件路径
                        string fileName = openFileDialog.FileName;
                        ImageSource = new BitmapImage(new Uri(fileName));
                        Product.Image = ImageHelper.GetImageString(fileName);
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
