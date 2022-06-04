using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace Helicopkki
{
    
    /// <summary>
    /// SettingWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : Window
    {
        public delegate void BoolDataGetEventHandler(bool item);
        public delegate void DoubleDataGetEventHandler(double item);
        public delegate void ByteDataGetEventHandler(byte item);

        public event BoolDataGetEventHandler IsFollowCursorDataSendEvent;
        public event BoolDataGetEventHandler IsRelativePositionFixedDataSendEvent;
        public event BoolDataGetEventHandler IsTopSendEvent;
        public event BoolDataGetEventHandler IsDanceSendEvent;
        public event BoolDataGetEventHandler IsTestDanceSendEvent;
        
        public event DoubleDataGetEventHandler GSendEvent;

        public event ByteDataGetEventHandler FollowSpeedSendEvent;
        public event ByteDataGetEventHandler SizeSendEvent;
        public event ByteDataGetEventHandler SensitivitySendEvent;
        public event ByteDataGetEventHandler JumpPowerSendEvent;
        public event ByteDataGetEventHandler RelativePositionIndexSendEvent;

        public SettingWindow()
        {
            InitializeComponent();

            // isFollow radiobutton 초기설정
            if (Properties.Settings.Default.isFollow)
            {
                FollowRadioAgree.IsChecked = true;
            } else
            {
                FollowRadioDisagree.IsChecked = true;
            }

            // isDance radiobutton 초기설정
            if (Properties.Settings.Default.isDance)
            {
                DanceRadioAgree.IsChecked = true;
            }
            else
            {
                DanceRadioDisagree.IsChecked = true;
            }

            // slider 초기설정
            FollowSpeedSlider.Value = Properties.Settings.Default.followSpeed;
            SizeSlider.Value = Properties.Settings.Default.size;
            JumpPowerSlider.Value = Properties.Settings.Default.jumpPower;
            SensitivitySlider.Value = Properties.Settings.Default.sensitivity;
            GSlider.Value = Properties.Settings.Default.g;

            // checkbox 초기설정
            RelativePositionFixCheckBox.IsChecked = Properties.Settings.Default.isRelativePositionFixed;
            IsTopCheckBox.IsChecked = Properties.Settings.Default.isTop;

            // combobox 초기설정
            RelativePositionFixComboBox.SelectedIndex = Properties.Settings.Default.relativePositionIndex;

        }

        // 커서 따라오기 라디오 버튼 클릭 시
        private void FollowCursorRadio_Click(object sender, RoutedEventArgs e)
        {
            IsFollowCursorDataSendEvent?.Invoke((bool)FollowRadioAgree.IsChecked);
        }

        // 커서 따라오기 속도 값 변경 시
        private void FollowSpeedSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            FollowSpeedSendEvent?.Invoke((byte)e.NewValue);
            if (RelativePositionPanel != null)
            {
                // 속도 100 설정 시
                if (e.NewValue == FollowSpeedSlider.Maximum)
                {
                    // 상대위치 고정 패널 보이게, 값 전달
                    RelativePositionPanel.Visibility = Visibility.Visible;
                    IsRelativePositionFixedDataSendEvent?.Invoke((bool)RelativePositionFixCheckBox.IsChecked);
                    RelativePositionIndexSendEvent?.Invoke((byte)RelativePositionFixComboBox.SelectedIndex);
                } else
                {
                    RelativePositionPanel.Visibility = Visibility.Collapsed;
                }
            }
            
        }

        // 크기 값 변경 시
        private void SizeSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SizeSendEvent?.Invoke((byte)e.NewValue);
        }

        // 상대위치 체크 변경 시
        private void RelativePositionFixCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {

            IsRelativePositionFixedDataSendEvent?.Invoke((bool)RelativePositionFixCheckBox.IsChecked);
            RelativePositionIndexSendEvent?.Invoke((byte)RelativePositionFixComboBox.SelectedIndex);
        }

        // 상대위치 index 바뀔 시
        private void RelativePositionFixComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RelativePositionIndexSendEvent?.Invoke((byte)RelativePositionFixComboBox.SelectedIndex);
        }

        // 항상 위 체크 변경 시
        private void IsTopCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            IsTopSendEvent?.Invoke((bool)IsTopCheckBox.IsChecked);
        }

        // 춤 설정 라디오 버튼 클릭 시
        private void DanceRadio_Click(object sender, RoutedEventArgs e)
        {
            IsDanceSendEvent?.Invoke((bool)DanceRadioAgree.IsChecked);
        }

        // 점프 높이 슬라이더 변경 시
        private void JumpPowerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            JumpPowerSendEvent?.Invoke((byte) e.NewValue);
        }

        // 중력가속도 변경 시
        private void GSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            GSendEvent?.Invoke(e.NewValue);
        }

        // 소리 민감도 변경 시
        private void SensitivitySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SensitivitySendEvent?.Invoke((byte)e.NewValue);
        }

        // 중력가속도 값 입력 후 포커스 잃었을 때
        private void GSliderTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            // 상자 않의 값으로 중력가속도 변경
            try
            {
                // 바뀐 직후 GSlider_ValueChanged 실행됨
                GSlider.Value = double.Parse(GSliderTextBox.Text);
            } catch
            {
                // 오류 시 최솟값으로 설정
                GSlider.Value = GSlider.Minimum;
            }
            
        }

        // 중력가속도 입력 후 window 클릭 시 GSliderTextBox_LostFocus 작동되게 함 
        private void SettingWIndow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GSliderTextBoxGrid.Focus();
        }

        // 점프 테스트 버튼 클릭 시
        private void DanceTestButton_Click(object sender, RoutedEventArgs e)
        {
            IsTestDanceSendEvent?.Invoke(true);
        }
    }
}
