using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using HelicopkkiDev.Library;
using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;

namespace HelicopkkiDev.ViewModel
{
    class BasicSettingUserControlViewModel : ViewModelBase
    {
        #region BasicModel Property
        private BasicModel _basicModel;
        public BasicModel BasicModel
        {
            get => _basicModel;
            set
            {
                if (value == _basicModel) return;
                _basicModel = value;
            }
        }
        #endregion

        #region SystemModel Property
        private SystemModel _systemModel;
        public SystemModel SystemModel
        {
            get => _systemModel;
            set
            {
                if (value == _systemModel) return;
                _systemModel = value;
            }
        }
        #endregion


        public BasicSettingUserControlViewModel()
        {
            BasicModel = BasicModel.Instance;
            SystemModel = SystemModel.Instance;
        }
    }
}
