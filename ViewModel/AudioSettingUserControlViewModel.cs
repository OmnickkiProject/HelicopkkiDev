using HelicopkkiDev.Model;
using HelicopkkiDev.ViewModel.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace HelicopkkiDev.ViewModel
{
    class AudioSettingUserControlViewModel : ViewModelBase
    {
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


        private ICommand _allowsCharacterToScaleDanceUncheckedCommand;
        public ICommand AllowsCharacterToScaleDanceUncheckedCommand => _allowsCharacterToScaleDanceUncheckedCommand ??= new RelayCommand<object>((o) =>
        {
            AudioModel.CharacterDancedScale = 1.0;
        });

        private ICommand _allowsCharacterToJumpDanceUncheckedCommand;
        public ICommand AllowsCharacterToJumpDanceUncheckedCommand => _allowsCharacterToJumpDanceUncheckedCommand ??= new RelayCommand<object>((o) =>
        {
            AudioModel.CharacterDancedHeight = 0.0;
        });

        private ICommand _allowsCharacterToSpinWithAudioUncheckedCommand;
        public ICommand AllowsCharacterToSpinWithAudioUncheckedCommand => _allowsCharacterToSpinWithAudioUncheckedCommand ??= new RelayCommand<object>((o) =>
        {
            AudioModel.CharacterSpunAngle = 0.0;
        });


        public AudioSettingUserControlViewModel()
        {
            AudioModel = AudioModel.Instance;
            SystemModel = SystemModel.Instance;
        }
    }
}
