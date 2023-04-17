using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using 超市管理系统.Entity.Model;
using 超市管理系统.Helper;

namespace 超市管理系统.Entity
{
    public partial class Product : BaseModel
    {
        public BitmapImage BitmapImage
        {
            get
            {
                return ImageHelper.GetBitmapImage(Image);
            }
        }
    }
}
