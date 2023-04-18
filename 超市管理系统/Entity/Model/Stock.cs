using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using 超市管理系统.Entity.Model;
using 超市管理系统.Helper;

namespace 超市管理系统.Entity
{
    public partial class Stock : BaseModel
    {
        /// <summary>
        /// 商品名称
        /// </summary>
        public string ProductName
        {
            get
            {
                return ProductProvider.Current.GetAll().FirstOrDefault(t => t.Id == ProductId).Name;
            }
        }

        /// <summary>
        /// 扩展的属性
        /// </summary>
        public BitmapImage BitmapImage
        {
            get
            {

                return ImageHelper.GetBitmapImage(ProductProvider.Current.GetAll().FirstOrDefault(t => t.Id == ProductId).Image);
            }
        }

        /// <summary>
        /// 扩展的属性
        /// </summary>
        public string Unit
        {
            get
            {

                return ProductProvider.Current.GetAll().FirstOrDefault(t => t.Id == ProductId).Unit;
            }
        }
    }
}
