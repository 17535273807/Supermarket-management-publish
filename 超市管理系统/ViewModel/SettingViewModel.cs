using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace 超市管理系统.ViewModel
{
    public class SettingViewModel : ViewModelBase2
    {
        #region commands

        public RelayCommand<UserControl> LoadedCommand
        {
            get
            {
                return new RelayCommand<UserControl>((view) =>
                {

                });
            }
        }

        #endregion
    }
}
