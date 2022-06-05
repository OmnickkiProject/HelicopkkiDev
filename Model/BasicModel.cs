using System.Windows;

namespace HelicopkkiDev.Model
{
    sealed class BasicModel : ModelBase
    {
        private static BasicModel s_instance;
        public static BasicModel Instance => s_instance ??= new();
        private BasicModel() { }


        private Vector _characterSize;
        public Vector CharacterSize
        {
            get => _characterSize;
            set
            {
                if (value == _characterSize) return;

                // CharacterSize를 항상 Character의 중심을 기준으로 변형되도록 보이게 함
                CharacterPosition -= (value - _characterSize) * 0.5;

                _characterSize = value;
                OnPropertyChanged();
            }
        }

        private double _characterAngle;
        public double CharacterAngle
        {
            get => _characterAngle;
            set
            {
                if (value == _characterAngle) return;
                _characterAngle = value;
                OnPropertyChanged();
            }
        }

        private Point _characterPosition;
        public Point CharacterPosition
        {
            get => _characterPosition;
            set
            {
                if (value == _characterPosition) return;
                _characterPosition = value;
                OnPropertyChanged();
            }
        }

        private double _characterOpacity;
        public double CharacterOpacity
        {
            get => _characterOpacity;
            set
            {
                if (value == _characterOpacity) return;
                _characterOpacity = value;
                OnPropertyChanged();
            }
        }

        private double _characterScaleX;
        public double CharacterScaleX
        {
            get => _characterScaleX;
            set
            {
                if (value == _characterScaleX) return;
                _characterScaleX = value;
                OnPropertyChanged();
            }
        }

        private double _characterGifSpeed;
        public double CharacterGifSpeed
        {
            get => _characterGifSpeed;
            set
            {
                if (value == _characterGifSpeed) return;
                _characterGifSpeed = value;
                OnPropertyChanged();
            }
        }

        private bool _characterWindowTopmost;
        public bool CharacterWindowTopmost
        {
            get => _characterWindowTopmost;
            set
            {
                if (value == _characterWindowTopmost) return;
                _characterWindowTopmost = value;
                OnPropertyChanged();
            }
        }

        private bool _showsCharacterShadow;
        public bool ShowsCharacterShadow
        {
            get => _showsCharacterShadow;
            set
            {
                if (value == _showsCharacterShadow) return;
                _showsCharacterShadow = value;
                OnPropertyChanged();
            }
        }

        private double _characterShadowDirection;
        public double CharacterShadowDirection
        {
            get => _characterShadowDirection;
            set
            {
                if (value == _characterShadowDirection) return;
                _characterShadowDirection = value;
                OnPropertyChanged();
            }
        }

        private double _characterShadowDepth;
        public double CharacterShadowDepth
        {
            get => _characterShadowDepth;
            set
            {
                if (value == _characterShadowDepth) return;
                _characterShadowDepth = value;
                OnPropertyChanged();
            }
        }

        private double _characterShadowRadius;
        public double CharacterShadowRadius
        {
            get => _characterShadowRadius;
            set
            {
                if (value == _characterShadowRadius) return;
                _characterShadowRadius = value;
                OnPropertyChanged();
            }
        }

        private double _characterShadowOpacity;
        public double CharacterShadowOpacity
        {
            get => _characterShadowOpacity;
            set
            {
                if (value == _characterShadowOpacity) return;
                _characterShadowOpacity = value;
                OnPropertyChanged();
            }
        }

        private bool _showsHotMenu;
        public bool ShowsHotMenu
        {
            get => _showsHotMenu;
            set
            {
                if (value == _showsHotMenu) return;
                _showsHotMenu = value;
                OnPropertyChanged();
            }
        }

        private bool _showsFps;
        public bool ShowsFps
        {
            get => _showsFps;
            set
            {
                if (value == _showsFps) return;
                _showsFps = value;
                OnPropertyChanged();
            }
        }
    }
}
