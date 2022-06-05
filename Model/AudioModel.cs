namespace HelicopkkiDev.Model
{
    sealed class AudioModel : ModelBase
    {
        private static AudioModel s_instance;
        public static AudioModel Instance => s_instance ??= new();
        private AudioModel() { }


        private bool _allowsAudioReaction;
        public bool AllowsAudioReaction
        {
            get => _allowsAudioReaction;
            set
            {
                if (value == _allowsAudioReaction) return;
                _allowsAudioReaction = value;
                OnPropertyChanged();
            }
        }

        private bool _allowsCharacterToScaleDance;
        public bool AllowsCharacterToScaleDance
        {
            get => _allowsCharacterToScaleDance;
            set
            {
                if (value == _allowsCharacterToScaleDance) return;
                _allowsCharacterToScaleDance = value;
                OnPropertyChanged();
            }
        }

        private bool _allowsCharacterToJumpDance;
        public bool AllowsCharacterToJumpDance
        {
            get => _allowsCharacterToJumpDance;
            set
            {
                if (value == _allowsCharacterToJumpDance) return;
                _allowsCharacterToJumpDance = value;
                OnPropertyChanged();
            }
        }

        private double _characterMaxDanceScale;
        public double CharacterMaxDanceScale
        {
            get => _characterMaxDanceScale;
            set
            {
                if (value == _characterMaxDanceScale) return;
                _characterMaxDanceScale = value;
                OnPropertyChanged();
            }
        }

        private double _characterDancedHeight;
        public double CharacterDancedHeight
        {
            get => _characterDancedHeight;
            set
            {
                if (value == _characterDancedHeight) return;
                _characterDancedHeight = value;
                OnPropertyChanged();
            }
        }

        private double _characterDancedScale;
        public double CharacterDancedScale
        {
            get => _characterDancedScale;
            set
            {
                if (value == _characterDancedScale) return;
                _characterDancedScale = value;
                OnPropertyChanged();
            }
        }

        private double _characterDanceIntensity;
        public double CharacterDanceIntensity
        {
            get => _characterDanceIntensity;
            set
            {
                if (value == _characterDanceIntensity) return;
                _characterDanceIntensity = value;
                OnPropertyChanged();
            }
        }

        private double _audioVolumeWeight;
        public double AudioVolumeWeight
        {
            get => _audioVolumeWeight;
            set
            {
                if (value == _audioVolumeWeight) return;
                _audioVolumeWeight = value;
                OnPropertyChanged();
            }
        }

        private bool _allowsCharacterToSpinWithAudio;
        public bool AllowsCharacterToSpinWithAudio
        {
            get => _allowsCharacterToSpinWithAudio;
            set
            {
                if (value == _allowsCharacterToSpinWithAudio) return;
                _allowsCharacterToSpinWithAudio = value;
                OnPropertyChanged();
            }
        }

        private double _characterSpunAngle;
        public double CharacterSpunAngle
        {
            get => _characterSpunAngle;
            set
            {
                if (value == _characterSpunAngle) return;
                _characterSpunAngle = value;
                OnPropertyChanged();
            }
        }

        private double _characterSpinSpeed;
        public double CharacterSpinSpeed
        {
            get => _characterSpinSpeed;
            set
            {
                if (value == _characterSpinSpeed) return;
                _characterSpinSpeed = value;
                OnPropertyChanged();
            }
        }
    }
}
