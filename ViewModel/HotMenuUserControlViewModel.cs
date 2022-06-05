using HelicopkkiDev.Library;
using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HelicopkkiDev.ViewModel
{
    class HotMenuUserControlViewModel : ViewModelBase
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

        #region MouseModel Property
        private MouseModel _mouseModel;
        public MouseModel MouseModel
        {
            get => _mouseModel;
            set
            {
                if (value == _mouseModel) return;
                _mouseModel = value;
            }
        }
        #endregion

        #region AudioModel Property
        private AudioModel _audioModel;
        public AudioModel AudioModel
        {
            get => _audioModel;
            set
            {
                if (value == _audioModel) return;
                _audioModel = value;
            }
        }
        #endregion

        #region SystemModel Property
        // int CurrentSettingIndex;
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


        #region IsHotMenuSnapped Property
        private bool _isBasicHotMenuSnapped;
        public bool IsBasicHotMenuSnapped
        {
            get => _isBasicHotMenuSnapped;
            set
            {
                if (value == _isBasicHotMenuSnapped) return;
                _isBasicHotMenuSnapped = value;
                OnPropertyChanged();
            }
        }

        private bool _isAudioHotMenuSnapped;
        public bool IsAudioHotMenuSnapped
        {
            get => _isAudioHotMenuSnapped;
            set
            {
                if (value == _isAudioHotMenuSnapped) return;
                _isAudioHotMenuSnapped = value;
                OnPropertyChanged();
            }
        }
        #endregion


        private ICommand _hotMenuUserControlGotFocusCommand;
        public ICommand HotMenuUserControlGotFocusCommand => _hotMenuUserControlGotFocusCommand ??= new RelayCommand<object>((o) =>
        {
            SystemModel.CurrentSettingIndex = int.Parse((string)o);
        });


        public HotMenuUserControlViewModel()
        {
            BasicModel = BasicModel.Instance;
            MouseModel = MouseModel.Instance;
            AudioModel = AudioModel.Instance;
            SystemModel = SystemModel.Instance;
        }
    }
}
