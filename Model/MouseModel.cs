namespace HelicopkkiDev.Model
{
    sealed class MouseModel : ModelBase
    {
        private static MouseModel s_instance;
        public static MouseModel Instance => s_instance ??= new();
        private MouseModel() { }

        private bool _allowsMouseReaction;
        public bool AllowsMouseReaction
        {
            get => _allowsMouseReaction;
            set
            {
                if (value == _allowsMouseReaction) return;
                _allowsMouseReaction = value;
                OnPropertyChanged();
            }
        }

        private double _characterMaxHeadingScale;
        public double CharacterMaxHeadingScale
        {
            get => _characterMaxHeadingScale;
            set
            {
                if (value == _characterMaxHeadingScale) return;
                _characterMaxHeadingScale = value;
                OnPropertyChanged();
            }
        }

        private double _characterEscapeIntensity;
        public double CharacterEscapeIntensity
        {
            get => _characterEscapeIntensity;
            set
            {
                if (value == _characterEscapeIntensity) return;
                _characterEscapeIntensity = value;
                OnPropertyChanged();
            }
        }

        private double _characterFollowIntensity;
        public double CharacterFollowIntensity
        {
            get => _characterFollowIntensity;
            set
            {
                if (value == _characterFollowIntensity) return;
                _characterFollowIntensity = value;
                OnPropertyChanged();
            }
        }

        private double _characterTiltedAngle;
        public double CharacterTiltedAngle
        {
            get => _characterTiltedAngle;
            set
            {
                if (value == _characterTiltedAngle) return;
                _characterTiltedAngle = value;
                OnPropertyChanged();
            }
        }

        private double _characterTiltIntensity;
        public double CharacterTiltIntensity
        {
            get => _characterTiltIntensity;
            set
            {
                if (value == _characterTiltIntensity) return;
                _characterTiltIntensity = value;
                OnPropertyChanged();
            }
        }

        private double _characterFlippedScaleX;
        public double CharacterFlippedScaleX
        {
            get => _characterFlippedScaleX;
            set
            {
                if (value == _characterFlippedScaleX) return;
                _characterFlippedScaleX = value;
                OnPropertyChanged();
            }
        }

        private double _characterFlipIntensity;
        public double CharacterFlipIntensity
        {
            get => _characterFlipIntensity;
            set
            {
                if (value == _characterFlipIntensity) return;
                _characterFlipIntensity = value;
                OnPropertyChanged();
            }
        }

    }
}
