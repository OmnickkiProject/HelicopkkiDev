using NAudio.Wave;
using System;
using System.Windows;

namespace HelicopkkiDev.Model
{
    sealed class SystemModel : ModelBase
    {
        private static SystemModel s_instance;
        public static SystemModel Instance => s_instance ??= new();
        private SystemModel() { }


        private double _fps;
        public double Fps
        {
            get => _fps;
            set
            {
                if (value == _fps) return;
                _fps = value;
                OnPropertyChanged();
            }
        }

        private bool _isAnyWindowActive;
        public bool IsAnyWindowActive
        {
            get => _isAnyWindowActive;
            set
            {
                if (value == _isAnyWindowActive) return;
                _isAnyWindowActive = value;
                OnPropertyChanged();
            }
        }

        /// 0: Basic, 1: Mouse, 2: Audio
        private int _currentSettingIndex;
        public int CurrentSettingIndex
        {
            get => _currentSettingIndex;
            set
            {
                if (value == _currentSettingIndex) return;
                _currentSettingIndex = value;
                OnPropertyChanged();
            }
        }

        private Vector _characterMaxSize;
        public Vector CharacterMaxSize
        {
            get => _characterMaxSize;
            set
            {
                if (value == _characterMaxSize) return;
                _characterMaxSize = value;
                OnPropertyChanged();
            }
        }

        private Vector _characterMinSize;
        public Vector CharacterMinSize
        {
            get => _characterMinSize;
            set
            {
                if (value == _characterMinSize) return;
                _characterMinSize = value;
                OnPropertyChanged();
            }
        }

        private double _characterAspectRatio;
        public double CharacterAspectRatio
        {
            get => _characterAspectRatio;
            set
            {
                if (value == _characterAspectRatio) return;
                _characterAspectRatio = value;
                OnPropertyChanged();
            }
        }

        private Action<object, EventArgs> _calculateFps;
        public Action<object, EventArgs> CalculateFps
        {
            get => _calculateFps;
            set
            {
                if (value == _calculateFps) return;
                _calculateFps = value;
                OnPropertyChanged();
            }
        }

        private Action<object, EventArgs> _reactCharacterByMouse;
        public Action<object, EventArgs> ReactCharacterByMouse
        {
            get => _reactCharacterByMouse;
            set
            {
                if (value == _reactCharacterByMouse) return;
                _reactCharacterByMouse = value;
                OnPropertyChanged();
            }
        }

        private WasapiLoopbackCapture _audioCapture;
        public WasapiLoopbackCapture AudioCapture
        {
            get => _audioCapture;
            set
            {
                if (value == _audioCapture) return;
                _audioCapture = value;
                OnPropertyChanged();
            }
        }
    }
}
