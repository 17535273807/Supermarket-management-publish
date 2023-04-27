using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 超市管理系统.Entity.Model;

namespace 超市管理系统.Entity
{
    public partial class Order : BaseModel
    {
        private ObservableCollection<OrderDetail> children = new ObservableCollection<OrderDetail>();
        /// <summary>
        /// 订单详情
        /// </summary>
        public ObservableCollection<OrderDetail> Children
        {
            get
            {
                return this.children;
            }
            set
            {
                this.children = value;
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// 顾客名称
        /// </summary>
        public string CustomerName
        {
            get
            {
                return CustomerProvider.Current.GetAll().FirstOrDefault(t => t.Id == CustomerId).Name;
            }
        }

        
    }
}
