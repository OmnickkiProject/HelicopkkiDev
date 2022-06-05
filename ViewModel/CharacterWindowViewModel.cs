using HelicopkkiDev.Library;
using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace HelicopkkiDev.ViewModel
{
    class CharacterWindowViewModel : ViewModelBase
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


        #region ContentView Property
        private object _hotMenuContentView;
        public object HotMenuContentView
        {
            get => _hotMenuContentView;
            set
            {
                if (_hotMenuContentView == value) return;
                _hotMenuContentView = value;
            }
        }
        #endregion


        public CharacterWindowViewModel()
        {
            BasicModel = BasicModel.Instance;
            MouseModel = MouseModel.Instance;
            SystemModel = SystemModel.Instance;
            AudioModel = AudioModel.Instance;

            HotMenuContentView = new HotMenuUserControlViewModel();
        }
    }
}
