using Hardcodet.Wpf.TaskbarNotification;
using NAudio.Dsp;
using NAudio.Wave;
using HelicopkkiDev.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace HelicopkkiDev.View.Behavior
{
    /// <summary>
    /// 프로그램 최초 로드 시 실행
    /// </summary>
    class CharacterWindowLoadedAndClosedBehavior : BehaviorBase<Window>
    {
        #region Basic DependencyProperty
        public static DependencyProperty CharacterPositionProperty = DependencyProperty.Register(
        "CharacterPosition",
        typeof(Point),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterSizeProperty = DependencyProperty.Register(
        "CharacterSize",
        typeof(Vector),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterAngleProperty = DependencyProperty.Register(
        "CharacterAngle",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterOpacityProperty = DependencyProperty.Register(
        "CharacterOpacity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterScaleXProperty = DependencyProperty.Register(
        "CharacterScaleX",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterGifSpeedProperty = DependencyProperty.Register(
        "CharacterGifSpeed",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterWindowTopmostProperty = DependencyProperty.Register(
        "CharacterWindowTopmost",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty ShowsCharacterShadowProperty = DependencyProperty.Register(
        "ShowsCharacterShadow",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterShadowDirectionProperty = DependencyProperty.Register(
        "CharacterShadowDirection",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterShadowDepthProperty = DependencyProperty.Register(
        "CharacterShadowDepth",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterShadowRadiusProperty = DependencyProperty.Register(
        "CharacterShadowRadius",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterShadowOpacityProperty = DependencyProperty.Register(
        "CharacterShadowOpacity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public Point CharacterPosition
        {
            get => (Point)GetValue(CharacterPositionProperty);
            set => SetValue(CharacterPositionProperty, value);
        }
        public Vector CharacterSize
        {
            get => (Vector)GetValue(CharacterSizeProperty);
            set => SetValue(CharacterSizeProperty, value);
        }
        public double CharacterAngle
        {
            get => (double)GetValue(CharacterAngleProperty);
            set => SetValue(CharacterAngleProperty, value);
        }
        public double CharacterOpacity
        {
            get => (double)GetValue(CharacterOpacityProperty);
            set => SetValue(CharacterOpacityProperty, value);
        }
        public double CharacterScaleX
        {
            get => (double)GetValue(CharacterScaleXProperty);
            set => SetValue(CharacterScaleXProperty, value);
        }
        public double CharacterGifSpeed
        {
            get => (double)GetValue(CharacterGifSpeedProperty);
            set => SetValue(CharacterGifSpeedProperty, value);
        }
        public bool CharacterWindowTopmost
        {
            get => (bool)GetValue(CharacterWindowTopmostProperty);
            set => SetValue(CharacterWindowTopmostProperty, value);
        }
        public bool ShowsCharacterShadow
        {
            get => (bool)GetValue(ShowsCharacterShadowProperty);
            set => SetValue(ShowsCharacterShadowProperty, value);
        }
        public double CharacterShadowDirection
        {
            get => (double)GetValue(CharacterShadowDirectionProperty);
            set => SetValue(CharacterShadowDirectionProperty, value);
        }
        public double CharacterShadowDepth
        {
            get => (double)GetValue(CharacterShadowDepthProperty);
            set => SetValue(CharacterShadowDepthProperty, value);
        }
        public double CharacterShadowRadius
        {
            get => (double)GetValue(CharacterShadowRadiusProperty);
            set => SetValue(CharacterShadowRadiusProperty, value);
        }

        public double CharacterShadowOpacity
        {
            get => (double)GetValue(CharacterShadowOpacityProperty);
            set => SetValue(CharacterShadowOpacityProperty, value);
        }

        public static DependencyProperty ShowsHotMenuProperty = DependencyProperty.Register(
        "ShowsHotMenu",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public bool ShowsHotMenu
        {
            get => (bool)GetValue(ShowsHotMenuProperty);
            set => SetValue(ShowsHotMenuProperty, value);
        }


        public bool ShowsFps
        {
            get { return (bool)GetValue(ShowsFpsProperty); }
            set { SetValue(ShowsFpsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShowsFps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShowsFpsProperty =
            DependencyProperty.Register("ShowsFps", typeof(bool), typeof(CharacterWindowLoadedAndClosedBehavior), new PropertyMetadata());
        #endregion

        #region Mouse DependencyProperty
        public static DependencyProperty ReactCharacterByMouseProperty = DependencyProperty.Register(
        "ReactCharacterByMouse",
        typeof(Action<object, EventArgs>),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty AllowsMouseReactionProperty = DependencyProperty.Register(
        "AllowsMouseReaction",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterFollowIntensityProperty = DependencyProperty.Register(
        "CharacterFollowIntensity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterTiltedAngleProperty = DependencyProperty.Register(
        "CharacterTiltedAngle",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterTiltIntensityProperty = DependencyProperty.Register(
        "CharacterTiltIntensity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterFlippedScaleXProperty = DependencyProperty.Register(
        "CharacterFlippedScaleX",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public static DependencyProperty CharacterFlipIntensityProperty = DependencyProperty.Register(
        "CharacterFlipIntensity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public Action<object, EventArgs> ReactCharacterByMouse
        {
            get => (Action<object, EventArgs>)GetValue(ReactCharacterByMouseProperty);
            set => SetValue(ReactCharacterByMouseProperty, value);
        }
        public bool AllowsMouseReaction
        {
            get => (bool)GetValue(AllowsMouseReactionProperty);
            set => SetValue(AllowsMouseReactionProperty, value);
        }
        public double CharacterFollowIntensity
        {
            get => (double)GetValue(CharacterFollowIntensityProperty);
            set => SetValue(CharacterFollowIntensityProperty, value);
        }
        public double CharacterTiltedAngle
        {
            get => (double)GetValue(CharacterTiltedAngleProperty);
            set => SetValue(CharacterTiltedAngleProperty, value);
        }
        public double CharacterTiltIntensity
        {
            get => (double)GetValue(CharacterTiltIntensityProperty);
            set => SetValue(CharacterTiltIntensityProperty, value);
        }
        public double CharacterFlippedScaleX
        {
            get => (double)GetValue(CharacterFlippedScaleXProperty);
            set => SetValue(CharacterFlippedScaleXProperty, value);
        }
        public double CharacterFlipIntensity
        {
            get => (double)GetValue(CharacterFlipIntensityProperty);
            set => SetValue(CharacterFlipIntensityProperty, value);
        }

        public static DependencyProperty CharacterMaxHeadingScaleProperty = DependencyProperty.Register(
        "CharacterMaxHeadingScale",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterMaxHeadingScale
        {
            get => (double)GetValue(CharacterMaxHeadingScaleProperty);
            set => SetValue(CharacterMaxHeadingScaleProperty, value);
        }

        public static DependencyProperty CharacterEscapeIntensityProperty = DependencyProperty.Register(
        "CharacterEscapeIntensity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterEscapeIntensity
        {
            get => (double)GetValue(CharacterEscapeIntensityProperty);
            set => SetValue(CharacterEscapeIntensityProperty, value);
        }
        #endregion

        #region System DependencyProperty
        public static DependencyProperty FpsProperty = DependencyProperty.Register(
        "Fps",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());

        public double Fps
        {
            get => (double)GetValue(FpsProperty);
            set => SetValue(FpsProperty, value);
        }

        public static DependencyProperty CharacterMaxSizeProperty = DependencyProperty.Register(
        "CharacterMaxSize",
        typeof(Vector),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public Vector CharacterMaxSize
        {
            get => (Vector)GetValue(CharacterMaxSizeProperty);
            set => SetValue(CharacterMaxSizeProperty, value);
        }

        public static DependencyProperty CharacterMinSizeProperty = DependencyProperty.Register(
        "CharacterMinSize",
        typeof(Vector),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public Vector CharacterMinSize
        {
            get => (Vector)GetValue(CharacterMinSizeProperty);
            set => SetValue(CharacterMinSizeProperty, value);
        }

        public static DependencyProperty CharacterAspectRatioProperty = DependencyProperty.Register(
        "CharacterAspectRatio",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterAspectRatio
        {
            get => (double)GetValue(CharacterAspectRatioProperty);
            set => SetValue(CharacterAspectRatioProperty, value);
        }


        public Action<object, EventArgs> CalculateFps
        {
            get { return (Action<object, EventArgs>)GetValue(CalculateFpsProperty); }
            set { SetValue(CalculateFpsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CalculateFps.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CalculateFpsProperty =
            DependencyProperty.Register("CalculateFps", typeof(Action<object, EventArgs>), typeof(CharacterWindowLoadedAndClosedBehavior), new PropertyMetadata());


        public int CurrentSettingIndex
        {
            get { return (int)GetValue(CurrentSettingIndexProperty); }
            set { SetValue(CurrentSettingIndexProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentSettingIndex.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentSettingIndexProperty =
            DependencyProperty.Register("CurrentSettingIndex", typeof(int), typeof(CharacterWindowLoadedAndClosedBehavior), new PropertyMetadata());
        #endregion

        #region Audio DependencyProperty
        public static DependencyProperty AllowsAudioReactionProperty = DependencyProperty.Register(
        "AllowsAudioReaction",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public bool AllowsAudioReaction
        {
            get => (bool)GetValue(AllowsAudioReactionProperty);
            set => SetValue(AllowsAudioReactionProperty, value);
        }

        public static DependencyProperty AudioCaptureProperty = DependencyProperty.Register(
        "AudioCapture",
        typeof(WasapiLoopbackCapture),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public WasapiLoopbackCapture AudioCapture
        {
            get => (WasapiLoopbackCapture)GetValue(AudioCaptureProperty);
            set => SetValue(AudioCaptureProperty, value);
        }

        public static DependencyProperty AllowsCharacterToScaleDanceProperty = DependencyProperty.Register(
        "AllowsCharacterToScaleDance",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public bool AllowsCharacterToScaleDance
        {
            get => (bool)GetValue(AllowsCharacterToScaleDanceProperty);
            set => SetValue(AllowsCharacterToScaleDanceProperty, value);
        }

        public static DependencyProperty AllowsCharacterToJumpDanceProperty = DependencyProperty.Register(
        "AllowsCharacterToJumpDance",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public bool AllowsCharacterToJumpDance
        {
            get => (bool)GetValue(AllowsCharacterToJumpDanceProperty);
            set => SetValue(AllowsCharacterToJumpDanceProperty, value);
        }

        public static DependencyProperty CharacterMaxDanceScaleProperty = DependencyProperty.Register(
        "CharacterMaxDanceScale",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterMaxDanceScale
        {
            get => (double)GetValue(CharacterMaxDanceScaleProperty);
            set => SetValue(CharacterMaxDanceScaleProperty, value);
        }

        public static DependencyProperty CharacterDancedHeightProperty = DependencyProperty.Register(
        "CharacterDancedHeight",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterDancedHeight
        {
            get => (double)GetValue(CharacterDancedHeightProperty);
            set => SetValue(CharacterDancedHeightProperty, value);
        }

        public static DependencyProperty CharacterDancedScaleProperty = DependencyProperty.Register(
        "CharacterDancedScale",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterDancedScale
        {
            get => (double)GetValue(CharacterDancedScaleProperty);
            set => SetValue(CharacterDancedScaleProperty, value);
        }

        //public static DependencyProperty CharacterDanceScaleYProperty = DependencyProperty.Register(
        //"CharacterDanceScaleY",
        //typeof(double),
        //typeof(CharacterWindowLoadedBehavior),
        //new PropertyMetadata());
        //public double CharacterDanceScaleY
        //{
        //    get => (double)GetValue(CharacterDanceScaleYProperty);
        //    set => SetValue(CharacterDanceScaleYProperty, value);
        //}

        public static DependencyProperty CharacterDanceIntensityProperty = DependencyProperty.Register(
        "CharacterDanceIntensity",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterDanceIntensity
        {
            get => (double)GetValue(CharacterDanceIntensityProperty);
            set => SetValue(CharacterDanceIntensityProperty, value);
        }

        public static DependencyProperty AudioVolumeWeightProperty = DependencyProperty.Register(
        "AudioVolumeWeight",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double AudioVolumeWeight
        {
            get => (double)GetValue(AudioVolumeWeightProperty);
            set => SetValue(AudioVolumeWeightProperty, value);
        }

        public static DependencyProperty AllowsCharacterToSpinWithAudioProperty = DependencyProperty.Register(
        "AllowsCharacterToSpinWithAudio",
        typeof(bool),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public bool AllowsCharacterToSpinWithAudio
        {
            get => (bool)GetValue(AllowsCharacterToSpinWithAudioProperty);
            set => SetValue(AllowsCharacterToSpinWithAudioProperty, value);
        }

        public static DependencyProperty CharacterSpunAngleProperty = DependencyProperty.Register(
        "CharacterSpunAngle",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterSpunAngle
        {
            get => (double)GetValue(CharacterSpunAngleProperty);
            set => SetValue(CharacterSpunAngleProperty, value);
        }

        public static DependencyProperty CharacterSpinSpeedProperty = DependencyProperty.Register(
        "CharacterSpinSpeed",
        typeof(double),
        typeof(CharacterWindowLoadedAndClosedBehavior),
        new PropertyMetadata());
        public double CharacterSpinSpeed
        {
            get => (double)GetValue(CharacterSpinSpeedProperty);
            set => SetValue(CharacterSpinSpeedProperty, value);
        }
        #endregion

        #region System Field
        private Point _dpi;
        private long _elapsedTime;
        private long _preTime;
        #endregion

        #region Mouse Field
        private double _screenHalf;
        bool _isFlipping;
        double _targetFlipScaleX;
        #endregion

        #region Audio Field
        // 전체 막대 바의 개수(반드시 2의 계승이어야 함)
        private static readonly int _size = 4096;
        // 캡처된 사운드 정보가 담기는 버퍼
        private WaveBuffer _buffer;
        // 사운드 카드의 초당 샘플링 속도
        private int _fs;
        // 가청 주파수 내부 막대 바의 개수
        private int _validSize;
        // _size는 2의 _m승이다
        private readonly int _m = (int)Math.Log(_size, 2);
        #endregion

        private void WindowOnLoaded(object sender, EventArgs e)
        {
            #region Initialize System Element
            CharacterWindow.Instance = (CharacterWindow)AssociatedObject;
            CurrentSettingIndex = 0;
            PresentationSource source = PresentationSource.FromVisual(AssociatedObject);
            _dpi = new Point(96.0 * source.CompositionTarget.TransformToDevice.M11, 96.0 * source.CompositionTarget.TransformToDevice.M22);
            BitmapFrame bitmapFrame = BitmapFrame.Create(new Uri(@"pack://application:,,,/Resource/Image/Character.gif"), BitmapCreateOptions.DelayCreation, BitmapCacheOption.None);
            CharacterMaxSize = new Vector(bitmapFrame.PixelWidth / _dpi.X, bitmapFrame.PixelHeight / _dpi.Y) * 96.0;
            // CharacterMinSize는 CharacterMaxSize의 1%로 고정
            CharacterMinSize = CharacterMaxSize * 0.01;
            CharacterAspectRatio = CharacterMaxSize.Y / CharacterMaxSize.X;
            CalculateFps += CompositionTargetOnRenderingFps;
            ReactCharacterByMouse += CompositionTargetOnRenderingMouseReaction;
            AudioCapture = new WasapiLoopbackCapture();
            var screenSize = new Vector(SystemParameters.WorkArea.Width, SystemParameters.WorkArea.Height);
            _screenHalf = screenSize.Length * 0.5;
            #endregion

            #region Initialize Basic Element
            var rawCharacterPosition = new Point(Properties.BasicModel.Default.CharacterPositionX, Properties.BasicModel.Default.CharacterPositionY);
            if(rawCharacterPosition.X == 0.0 && rawCharacterPosition.Y == 0.0)
            {
                // 초기 위치가 (0, 0)일 경우(최초 실행 시)에만 화면 정중앙에 배치
                CharacterPosition = new Point((screenSize.X  - CharacterSize.X ) * 0.5, (screenSize.Y - CharacterSize.Y) * 0.5);
            }
            else
            {
                CharacterPosition = rawCharacterPosition;
            }
            CharacterSize = CharacterMaxSize * Properties.BasicModel.Default.CharacterSizeRatio;
            CharacterAngle = Properties.BasicModel.Default.CharacterAngle;
            CharacterOpacity = Properties.BasicModel.Default.CharacterOpacity;
            CharacterScaleX = Properties.BasicModel.Default.CharacterScaleX;
            CharacterGifSpeed = Properties.BasicModel.Default.CharacterGifSpeed;
            ShowsCharacterShadow = Properties.BasicModel.Default.ShowsCharacterShadow;
            CharacterShadowDirection = Properties.BasicModel.Default.CharacterShadowDirection;
            CharacterShadowDepth = Properties.BasicModel.Default.CharacterShadowDepth;
            CharacterShadowRadius = Properties.BasicModel.Default.CharacterShadowRadius;
            CharacterShadowOpacity = Properties.BasicModel.Default.CharacterShadowOpacity;
            CharacterWindowTopmost = Properties.BasicModel.Default.CharacterWindowTopmost;
            ShowsHotMenu = Properties.BasicModel.Default.ShowsHotMenu;
            ShowsFps = Properties.BasicModel.Default.ShowsFps;
            if (ShowsFps)
            {
                CompositionTarget.Rendering += CalculateFps.Invoke;
            }
            #endregion

            #region Initialize Mouse Element
            AllowsMouseReaction = Properties.MouseModel.Default.AllowsMouseReaction;
            if (AllowsMouseReaction)
            {
                CompositionTarget.Rendering += ReactCharacterByMouse.Invoke;
            }
            CharacterFollowIntensity = Properties.MouseModel.Default.CharacterFollowIntensity;
            CharacterTiltIntensity = Properties.MouseModel.Default.CharacterTiltIntensity;
            CharacterFlipIntensity = Properties.MouseModel.Default.CharacterFlipIntensity;
            CharacterMaxHeadingScale = Properties.MouseModel.Default.CharacterMaxHeadingScale;
            CharacterEscapeIntensity = Properties.MouseModel.Default.CharacterEscapeIntensity;
            CharacterTiltedAngle = 0.0;
            CharacterFlippedScaleX = 1.0;
            #endregion

            #region Initialize Audio Element
            AllowsAudioReaction = Properties.AudioModel.Default.AllowsAudioReaction;
            AllowsCharacterToScaleDance = Properties.AudioModel.Default.AllowsCharacterToScaleDance;
            AllowsCharacterToJumpDance = Properties.AudioModel.Default.AllowsCharacterToJumpDance;
            CharacterMaxDanceScale = Properties.AudioModel.Default.CharacterMaxDanceScale;
            CharacterDanceIntensity = Properties.AudioModel.Default.CharacterDanceIntensity;
            AudioVolumeWeight = Properties.AudioModel.Default.AudioVolumeWeight;
            CharacterSpunAngle = 0.0;
            AllowsCharacterToSpinWithAudio = Properties.AudioModel.Default.AllowsCharacterToSpinWithAudio;
            CharacterSpinSpeed = Properties.AudioModel.Default.CharacterSpinSpeed;
            CharacterDancedHeight = 0.0;
            CharacterDancedScale = 1.0;
            // AudioCapture.StartRecording(); 시 반복 호출
            AudioCapture.DataAvailable += (s, e) =>
            {
                _buffer = new WaveBuffer(e.Buffer);

                if (_fs == 0)
                {
                    // fs 산출 공식
                    _fs = (e.Buffer.Length >> 3) * 10;
                    // 막대 바 사이의 주파수 간격
                    float deltaFreq = (float)_fs / (_size >> 1);
                    _validSize = (int) (20000.0f / deltaFreq);
                    Dispatcher.Invoke(new Action(delegate
                    {
                        CompositionTarget.Rendering += CompositionTargetOnRenderingAudioReaction;
                    }));
                }
            };
            // AudioCapture.RecordingStopped() 시 1회 호출
            AudioCapture.RecordingStopped += (s, e) =>
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    CompositionTarget.Rendering -= CompositionTargetOnRenderingAudioReaction;
                }));
                // 변형 값 원위치
                _fs = 0;
                CharacterDancedScale = 1.0;
                CharacterDancedHeight = 0.0;
                CharacterSpunAngle = 0.0;
            };
            if (AllowsAudioReaction)
            {
                AudioCapture.StartRecording();
            }
            #endregion
        }

        #region Calculate FPS Method
        private void CompositionTargetOnRenderingFps(object sender, EventArgs e)
        {
            long differenceTime = DateTime.UtcNow.Ticks - _preTime;
            _elapsedTime += differenceTime;
            if (_elapsedTime > 1000_0000)
            {
                Fps = Math.Ceiling(1000_0000.0 / differenceTime);
                _elapsedTime = 0;

            }
            _preTime = DateTime.UtcNow.Ticks;
        }
        #endregion

        #region Capture Mouse Method
        private void CompositionTargetOnRenderingMouseReaction(object sender, EventArgs e)
        {
            var mousePosition = new Point
            (
                System.Windows.Forms.Control.MousePosition.X * 96.0 / _dpi.X, 
                System.Windows.Forms.Control.MousePosition.Y * 96.0 / _dpi.Y
            );

            Point centerPoint = CharacterPosition + CharacterSize * 0.5;
            Vector centerToMouse = mousePosition - centerPoint;
            centerToMouse.Normalize();

            Vector startVector = centerToMouse * (CharacterSize.Length * 0.5) * CharacterMaxHeadingScale;
            Vector endVector = mousePosition - centerPoint;
            Vector routeVector = endVector - startVector;

            // 마우스가 무시 범위 밖에 있을 때
            if (startVector.Length < endVector.Length)
            {
                #region Follow 실행부
                // FollowIntensity가 최대치보다 크거나 같을 때
                if (CharacterFollowIntensity >= 1.0)
                {
                    CharacterPosition += routeVector;
                }
                // FollowIntensity가 최대치보다 작고 최소치보다 클 때
                else if (CharacterFollowIntensity > 0.0)
                {
                    CharacterPosition += routeVector * CharacterFollowIntensity * 0.1;
                }
                #endregion

                #region Tilt 실행부
                // TiltIntensity가 최대치보다 크거나 같을 때
                if (CharacterTiltIntensity >= 1.0)
                {
                    CharacterTiltedAngle = Vector.AngleBetween(new Vector(0.0, -1.0), routeVector).ToDeg180();
                }
                // TiltIntensity가 최대치보다 작고 최소치보다 클 때
                else if (CharacterTiltIntensity > 0.0)
                {
                    double targetTilt = Vector.AngleBetween(new Vector(0.0, -1.0), routeVector).ToDeg180();

                    // 상하좌우 0 0 -90 90 좌표계로 변환
                    if (targetTilt > 90.0)
                    {
                        targetTilt = 180.0 - targetTilt;
                    }
                    else if (targetTilt < -90.0)
                    {
                        targetTilt = -180.0 - targetTilt;
                    }

                    double ratio = (routeVector.Length * CharacterTiltIntensity / _screenHalf).ToMaxRange(1.0);
                    CharacterTiltedAngle = targetTilt * ratio;
                }
                // CharacterTiltIntensity가 최소치로 설정되면 MouseSettingUserControlViewModel에서 CharacterTiltedAngle = 0으로 핸들링됨
                #endregion

                #region Flip 결정부
                bool isMouseLeft = mousePosition.X < centerPoint.X;
                bool isCharacterLeft = CharacterFlippedScaleX > 0.0;
                _targetFlipScaleX = isMouseLeft ? 1.0 : -1.0;

                // 마우스와 바라보는 방향이 다르고, 뒤집혀지는 상태가 아닐 때
                if (isMouseLeft != isCharacterLeft && _isFlipping == false)
                {
                    // FlipIntensity가 최대치보다 크거나 같을 때
                    if (CharacterFlipIntensity >= 1.0)
                    {
                        CharacterFlippedScaleX *= -1.0;
                    }
                    // FlipIntensity가 최대치보다 작고 최소치보다 클 때
                    else if (CharacterFlipIntensity > 0.0)
                    {
                        _isFlipping = true;
                    }
                    // CharacterTiltIntensity가 최소치로 설정되면 MouseSettingUserControlViewModel에서 CharacterFlippedScaleX = 1.0으로 핸들링됨
                }
                #endregion
            }
            // 마우스가 무시 범위 안에 있을 때
            else
            {
                #region Escape 실행부
                // CharacterEscapeIntensity 최대치보다 크거나 같을 때
                if (CharacterEscapeIntensity >= 1.0)
                {
                    CharacterPosition += routeVector;
                }
                // CharacterEscapeIntensity 최소치보다 클 때
                else if (CharacterEscapeIntensity > 0.0)
                {
                    CharacterPosition += routeVector * CharacterEscapeIntensity * 0.1;
                }
                #endregion
            }

            #region Flip 실행부
            if (_isFlipping)
            {
                if (CharacterFlipIntensity == 0.0)
                {
                    CharacterFlippedScaleX = 1.0;
                    _isFlipping = false;
                }
                else if (Math.Abs(CharacterFlippedScaleX - _targetFlipScaleX) < CharacterFlipIntensity)
                {
                    CharacterFlippedScaleX = _targetFlipScaleX;
                    _isFlipping = false;
                }
                else if (CharacterFlippedScaleX < _targetFlipScaleX)
                {
                    CharacterFlippedScaleX += CharacterFlipIntensity;
                }
                else
                {
                    CharacterFlippedScaleX -= CharacterFlipIntensity;
                }
            }
            #endregion
        }
        #endregion

        #region Capture Audio Method
        private void CompositionTargetOnRenderingAudioReaction(object sender, EventArgs e)
        {
            // 캡쳐된 사운드 정보를 고속 푸리에 변환
            var values = new Complex[_size];
            for (var i = 0; i < _size; i++)
            {
                values[i].X = _buffer.FloatBuffer[i] * (float)FastFourierTransform.HannWindow(i, _size);
                values[i].Y = 0.0f;
            }
            FastFourierTransform.FFT(true, _m, values);

            // i = 0은 실제 주파수 값이 아니므로 제외
            // i = 1은 HannWindow로 인한 추가 invalid 값이다.
            var validSizeValues = new float[_validSize];
            float max = 0.0f;
            for (var i = 2; i < _validSize + 2; i++)
            {
                validSizeValues[i - 2] = Math.Abs(values[i].X);
                if (max < validSizeValues[i - 2])
                {
                    max = validSizeValues[i - 2];
                }
            }
            if(max < 1e-10f)
            {
                max = 0.0f;
            }

            double calculatedDanceScale = max * AudioVolumeWeight + 1.0;
            if (AllowsCharacterToScaleDance)
            {
                double targetDanceScale = calculatedDanceScale.ToMaxRange(CharacterMaxDanceScale);
                CharacterDancedScale += (targetDanceScale - CharacterDancedScale) * CharacterDanceIntensity;
            }
            if (AllowsCharacterToJumpDance)
            {
                double targetDanceHeight = (CharacterSize.Y * (calculatedDanceScale - 1.0)).ToMaxRange(CharacterSize.Y * (CharacterMaxDanceScale - 1.0));
                CharacterDancedHeight += (targetDanceHeight - CharacterDancedHeight) * CharacterDanceIntensity;
            }
            if (AllowsCharacterToSpinWithAudio && calculatedDanceScale > 1.0)
            {
                CharacterSpunAngle = (CharacterSpunAngle + CharacterSpinSpeed).ToDeg180();
            }
        }
        #endregion

        private void WindowOnClosed(object sender, EventArgs e)
        {
            // 설정값 저장 (총 29개)
            Properties.BasicModel.Default.CharacterPositionX = CharacterPosition.X;
            Properties.BasicModel.Default.CharacterPositionY = CharacterPosition.Y;
            Properties.BasicModel.Default.CharacterSizeRatio = CharacterSize.Length / CharacterMaxSize.Length;
            Properties.BasicModel.Default.CharacterAngle = CharacterAngle;
            Properties.BasicModel.Default.CharacterOpacity = CharacterOpacity;
            Properties.BasicModel.Default.CharacterScaleX = CharacterScaleX;
            Properties.BasicModel.Default.CharacterGifSpeed = CharacterGifSpeed;
            Properties.BasicModel.Default.ShowsCharacterShadow = ShowsCharacterShadow;
            Properties.BasicModel.Default.CharacterShadowDirection = CharacterShadowDirection;
            Properties.BasicModel.Default.CharacterShadowDepth = CharacterShadowDepth;
            Properties.BasicModel.Default.CharacterShadowRadius = CharacterShadowRadius;
            Properties.BasicModel.Default.CharacterShadowOpacity = CharacterShadowOpacity;
            Properties.BasicModel.Default.CharacterWindowTopmost = CharacterWindowTopmost;
            Properties.BasicModel.Default.ShowsHotMenu = ShowsHotMenu;
            Properties.BasicModel.Default.ShowsFps = ShowsFps;

            Properties.MouseModel.Default.AllowsMouseReaction = AllowsMouseReaction;
            Properties.MouseModel.Default.CharacterFollowIntensity = CharacterFollowIntensity;
            Properties.MouseModel.Default.CharacterTiltIntensity = CharacterTiltIntensity;
            Properties.MouseModel.Default.CharacterFlipIntensity = CharacterFlipIntensity;
            Properties.MouseModel.Default.CharacterMaxHeadingScale = CharacterMaxHeadingScale;
            Properties.MouseModel.Default.CharacterEscapeIntensity = CharacterEscapeIntensity;

            Properties.AudioModel.Default.AllowsAudioReaction = AllowsAudioReaction;
            Properties.AudioModel.Default.AllowsCharacterToScaleDance = AllowsCharacterToScaleDance;
            Properties.AudioModel.Default.AllowsCharacterToJumpDance = AllowsCharacterToJumpDance;
            Properties.AudioModel.Default.CharacterMaxDanceScale = CharacterMaxDanceScale;
            Properties.AudioModel.Default.CharacterDanceIntensity = CharacterDanceIntensity;
            Properties.AudioModel.Default.AudioVolumeWeight = AudioVolumeWeight;
            Properties.AudioModel.Default.AllowsCharacterToSpinWithAudio = AllowsCharacterToSpinWithAudio;
            Properties.AudioModel.Default.CharacterSpinSpeed = CharacterSpinSpeed;

            Properties.BasicModel.Default.Save();
            Properties.MouseModel.Default.Save();
            Properties.AudioModel.Default.Save();

            // TaskbarIcon 이미지 제거
            TaskbarIcon defaultTaskbarIcon = FindVisualChildByName<TaskbarIcon>(AssociatedObject, "defaultTaskbarIcon");
            defaultTaskbarIcon.Dispose();

            // 프로그램 종료
            Environment.Exit(0);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Loaded += WindowOnLoaded;
            AssociatedObject.Closed += WindowOnClosed;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.Loaded -= WindowOnLoaded;
            AssociatedObject.Closed -= WindowOnClosed;
        }
    }
}
