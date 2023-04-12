using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 超市管理系统.Entity
{
    /// <summary>
    /// 抽象类
    /// </summary>
    public abstract class ProviderBase
    {
        public ShoppingDBEntities db = new ShoppingDBEntities();
    }
}
