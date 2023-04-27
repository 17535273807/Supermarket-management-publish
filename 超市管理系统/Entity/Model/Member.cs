using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 超市管理系统.Entity.Model;

namespace 超市管理系统.Entity
{
    public partial class Member : BaseModel
    {
        public string NameEx
        {
            get
            {
                return Name;
            }
            set
            {
                Name= value;RaisePropertyChanged();
            }
        }

        public string PasswordEx
        {
            get
            {
                return Password;
            }
            set
            {
                Password = value; RaisePropertyChanged();
            }
        }
    }
}
