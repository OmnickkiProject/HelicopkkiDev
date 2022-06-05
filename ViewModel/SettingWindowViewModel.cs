using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace HelicopkkiDev.ViewModel
{
    class SettingWindowViewModel : ViewModelBase
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
        // bool IsAnyWindowActive;
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


        #region ContentView Property
        private object _basicSettingContentView;
        public object BasicSettingContentView
        {
            get => _basicSettingContentView;
            set
            {
                if (_basicSettingContentView == value) return;
                _basicSettingContentView = value;
            }
        }

        private object _mouseSettingContentView;
        public object MouseSettingContentView
        {
            get => _mouseSettingContentView;
            set
            {
                if (_mouseSettingContentView == value) return;
                _mouseSettingContentView = value;
            }
        }

        private object _audioSettingContentView;
        public object AudioSettingContentView
        {
            get => _audioSettingContentView;
            set
            {
                if (_audioSettingContentView == value) return;
                _audioSettingContentView = value;
            }
        }

        private object _defaultContextMenuContentView;
        public object DefaultContextMenuContentView
        {
            get => _defaultContextMenuContentView;
            set
            {
                if (_defaultContextMenuContentView == value) return;
                _defaultContextMenuContentView = value;
            }
        }
        #endregion


        public SettingWindowViewModel()
        {
            SystemModel = SystemModel.Instance;
            BasicModel = BasicModel.Instance;

            BasicSettingContentView = new BasicSettingUserControlViewModel();
            MouseSettingContentView = new MouseSettingUserControlViewModel();
            AudioSettingContentView = new AudioSettingUserControlViewModel();
        }
    }
}
