using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace HelicopkkiDev.ViewModel
{
    class MouseSettingUserControlViewModel : ViewModelBase
    {
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
        // Action<object, EventArgs> ReactCharacterByMouse;
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


        private ICommand _characterTiltIntensityValueChangedCommand;
        public ICommand CharacterTiltIntensityValueChangedCommand => _characterTiltIntensityValueChangedCommand ??= new RelayCommand<RoutedPropertyChangedEventArgs<double>>((e) =>
        {
            if (e.NewValue == 0.0)
            {
                MouseModel.CharacterTiltedAngle = 0.0;
            }
        });

        private ICommand _characterFlipIntensityValueChangedCommand;
        public ICommand CharacterFlipIntensityValueChangedCommand => _characterFlipIntensityValueChangedCommand ??= new RelayCommand<RoutedPropertyChangedEventArgs<double>>((e) =>
        {
            if (e.NewValue == 0.0)
            {
                MouseModel.CharacterFlippedScaleX = 1.0;
            }
        });


        public MouseSettingUserControlViewModel()
        {
            MouseModel = MouseModel.Instance;
            SystemModel = SystemModel.Instance;
        }
    }
}
