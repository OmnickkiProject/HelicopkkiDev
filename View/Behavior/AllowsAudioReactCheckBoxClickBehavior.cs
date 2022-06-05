using NAudio.CoreAudioApi;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace HelicopkkiDev.View.Behavior
{
    class AllowsAudioReactionCheckBoxClickBehavior : BehaviorBase<CheckBox>
    {
        #region DependencyProperty
        public static DependencyProperty AllowsAudioReactionProperty = DependencyProperty.Register(
        "AllowsAudioReaction",
        typeof(bool),
        typeof(AllowsAudioReactionCheckBoxClickBehavior),
        new PropertyMetadata());

        public bool AllowsAudioReaction
        {
            get => (bool)GetValue(AllowsAudioReactionProperty);
            set => SetValue(AllowsAudioReactionProperty, value);
        }

        public static DependencyProperty AudioCaptureProperty = DependencyProperty.Register(
        "AudioCapture",
        typeof(WasapiLoopbackCapture),
        typeof(AllowsAudioReactionCheckBoxClickBehavior),
        new PropertyMetadata());

        public WasapiLoopbackCapture AudioCapture
        {
            get => (WasapiLoopbackCapture)GetValue(AudioCaptureProperty);
            set => SetValue(AudioCaptureProperty, value);
        }
        #endregion

        private void CheckBoxOnClick(object sender, EventArgs e)
        {
            if (AllowsAudioReaction)
            {
                AudioCapture.StartRecording();
            }
            else
            {
                // Recording 중 변경된 Model은 StopRecording() 내부에서 모두 초기화됨
                AudioCapture.StopRecording();
            }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += CheckBoxOnClick;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Click -= CheckBoxOnClick;
        }
    }
}
