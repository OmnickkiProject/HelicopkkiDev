using CSCore.CoreAudioAPI;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Control = System.Windows.Forms.Control;
using MenuItem = System.Windows.Forms.MenuItem;

namespace Helicopkki
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        // 기본 설정 관련 필드
        bool isTop = Properties.Settings.Default.isTop;
        byte size = Properties.Settings.Default.size;
        int screenWidth = Screen.PrimaryScreen.Bounds.Width;
        int screenHeight = Screen.PrimaryScreen.Bounds.Height;
        int characterWidth = 770;
        int characterHeight = 1250;

        // 커서 따라오기 관련 필드
        bool isMouseDown = false;
        bool isFollow = Properties.Settings.Default.isFollow;
        bool isRelativePositionFixed = Properties.Settings.Default.isRelativePositionFixed;
        byte relativePositionIndex = Properties.Settings.Default.relativePositionIndex;
        byte followSpeed = Properties.Settings.Default.followSpeed;

        // 춤 관련 필드
        bool isUp = false;
        bool isTestDance = false;
        bool isDance = Properties.Settings.Default.isDance;
        bool isSetNowVolumeThreadExecute = false;
        byte jumpPower = Properties.Settings.Default.jumpPower;
        byte sensitivity = Properties.Settings.Default.sensitivity;
        double criteriaLeft = 0;
        double criteriaTop = 0;
        double volume = 0;
        double passedTime = 0;
        double firstSpeedY = 0;
        double danceIntervalTime = 0;
        double g = Properties.Settings.Default.g;
        double gConverted = 0;
        
            // 딜레이 관련 필드(중력가속도 보정)
        Stopwatch delay = new Stopwatch();
        int delayCount = 0;
        double delaySum = 0;
        double delayAvg = 0;
        
        public MainWindow() //생성자
        {
            Duplicate_execution("헬리콥끠 Dev");    // 중복 실행 체크

            // 모니터 주사율 가져와서 타이머 간격 설정
            DEVMODE dm = NativeMethods.CreateDevmode();
            GetSettings(ref dm);
            int displayFrequency = dm.dmDisplayFrequency;
            double intervalTime = (double) 1 / displayFrequency * 1000;

            // 실제 캐릭터 폭, 높이 설정
            Width = 30 + (double)(characterWidth - 30) / 1000 * size * 10;
            Height = Width * ((double)characterHeight / characterWidth);

            // 캐릭터 처음 위치는 화면 밖 랜덤
            Random rnd = new Random();
            int x = rnd.Next(0, 4);
            if (x == 0)
            {
                // 왼쪽
                Left = 0 - characterWidth;
                Top = rnd.Next(0, screenHeight);
            }
            else if (x == 1)
            {
                // 천장에서
                Left = rnd.Next(0, screenWidth);
                Top = 0 - characterHeight;
            }
            else if (x == 2)
            {
                //오른쪽
                Left = screenWidth;
                Top = rnd.Next(0, screenHeight);
            }
            else
            {
                //바닥에서
                Left = rnd.Next(0, screenWidth);
                Top = screenHeight;
            }
            
            // transform, transformgroup 선언
            ScaleTransform flipTrans = new ScaleTransform();
            RotateTransform rotateTrans = new RotateTransform();
            TransformGroup myTransformGroup = new TransformGroup();

            // 중력가속도 변환
            gConverted = g * (Height * 0.64 / 1.57) * ((double)1 / 1000000);

            // 커서 이전 위치 가져오기
            double preX = 0;
            double preY = 0;

            // 던지기 관련 변수 선언
            bool isThrow = false;
            double throwSpeedX = 0;
            double throwSpeedY = 0;
            double tangent = 0;
            double reduceSpeedX = 20;
            double limitSpeed = 400;

            // 타이머 선언
            System.Timers.Timer nonFollowTimer = new System.Timers.Timer();
            System.Timers.Timer followCursorTimer = new System.Timers.Timer();
            System.Timers.Timer relativePositionFIxedTimer = new System.Timers.Timer();

            //커서 따라오기 설정 시
            followCursorTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    // 캐릭터가 움직이는 정도
                    double xMove;
                    double yMove;

                    // 마우스가 캐릭터 위에 있는지
                    bool isXIn = false;
                    bool isYIn = false;

                    // 마우스 X위치
                    if (Control.MousePosition.X < Left) // 마우스가 캐릭터의 왼쪽보다 왼쪽에 있다면 
                    {
                        xMove = (Control.MousePosition.X - Left) / (101 - followSpeed);
                    } else if (Control.MousePosition.X < Left + Width) // 마우스가 캐릭터의 왼쪽보다 오른쪽에 있지만 창의 폭보다 작다면
                    {
                        xMove = (Control.MousePosition.X - preX) / (101 - followSpeed);
                        isXIn = true;
                    } else
                    {
                        xMove = (Control.MousePosition.X - Width - Left) / (101 - followSpeed);
                    }

                    // 마우스 Y위치
                    if (Control.MousePosition.Y < Top) // 마우스가 창의 위쪽보다 위쪽에 있다면 
                    {
                        yMove = (Control.MousePosition.Y - Top) / (101 - followSpeed);

                    }
                    else if (Control.MousePosition.Y < Top + Height) // 마우스가 창의 위쪽보다 아래쪽에 있지만 창의 높이보다 작다면
                    {
                        yMove = (Control.MousePosition.Y - preY) / (101 - followSpeed);
                        isYIn = true;
                    }
                    else
                    {
                        yMove = (Control.MousePosition.Y - Height - Top) / (101 - followSpeed);
                    }

                    // 마우스의 X좌표가 캐릭터 위에 있지 않다면
                    if (!isXIn)
                    {
                        if (xMove >= 0) //캐릭터가 오른쪽으로 이동한다면
                        {
                            // 캐릭터 기울이고 뒤집기
                            flipTrans.ScaleX = -1;
                            if (xMove > 15)     // 최대 각도는 15도
                            {
                                rotateTrans.Angle = 15;
                            } else
                            {
                                rotateTrans.Angle = xMove;
                            }
                        }
                        else
                        {
                            flipTrans.ScaleX = 1;
                            if (xMove < -15)
                            {
                                rotateTrans.Angle = -15;
                            }
                            else
                            {
                                rotateTrans.Angle = xMove;
                            }
                        }

                    } else if (isYIn)   // 마우스가 캐릭터 위에 있다면 각도 초기화
                    {
                        rotateTrans.Angle = 0;
                    }

                    // 던지기 활성화
                    if (isThrow)
                    {
                        // 던져지는 동안 각도는 0
                        rotateTrans.Angle = 0;

                        // x방향 속도가 0보다 크면 
                        if (throwSpeedX >= 0)
                        {
                            // 오른쪽으로 던져지므로 뒤집고 속도는 양수이므로 빼서 줄여야
                            flipTrans.ScaleX = -1;
                            throwSpeedX -= reduceSpeedX;
                        } else
                        {
                            // 왼쪽으로 던져지면 속도가 음수이므로 더해서 줄여야
                            flipTrans.ScaleX = 1;
                            throwSpeedX += reduceSpeedX;
                        }
                        
                        // 던져지는 곳으로 움직임 재설정
                        xMove = throwSpeedX;
                        yMove = throwSpeedY;

                        // y방향 속도가 0보다 크면 y 이동 방향은 x * tan
                        if (throwSpeedY >= 0)
                        {
                            throwSpeedY -= reduceSpeedX * tangent;
                        }
                        else
                        {
                            throwSpeedY += reduceSpeedX * tangent;
                        }

                        // 현재 속도가 reduceSpeed보다 같거나 작다면 isThrow 초기화
                        if (Math.Pow(throwSpeedX, 2) + Math.Pow(throwSpeedY, 2) <= 
                        Math.Pow(reduceSpeedX, 2) + Math.Pow(reduceSpeedX * tangent, 2))
                        {
                            isThrow = false;
                            isMouseDown = false;
                        }
                    }
                    else if (isMouseDown) // 캐릭터 마우스로 누르면
                    {
                        // 캐릭터 마우스 중앙으로 배치
                        Left = Control.MousePosition.X - Width / 2;
                        Top = Control.MousePosition.Y - Height / 2;

                        // throwSpeed 설정
                        throwSpeedX = Control.MousePosition.X - preX;
                        throwSpeedY = Control.MousePosition.Y - preY;

                        // 마우스 따라 이동만 가능하게
                        xMove = 0;
                        yMove = 0;

                        // throwSpeed가 limitSpeed보다 크다면 isThrow true, tangent 설정
                        if (Math.Pow(Math.Pow(throwSpeedX, 2) + Math.Pow(throwSpeedY, 2), 0.5) > limitSpeed)
                        {
                            isThrow = true;
                            tangent = Math.Abs(throwSpeedY / throwSpeedX);
                        }
                    }

                    // 캐릭터 회전 각도, 대칭 값 실질적 변경
                    myTransformGroup.Children.Clear();
                    myTransformGroup.Children.Add(flipTrans);
                    myTransformGroup.Children.Add(rotateTrans);
                    characterImage.RenderTransform = myTransformGroup;

                    // 캐릭터 위치 실질적 변경
                    Left += xMove;
                    Top += yMove;
                    preX = Control.MousePosition.X;
                    preY = Control.MousePosition.Y;
                    

                    // 커서 따라오기 해제(춤 설정/해제 창 나타남)
                    if (!isFollow)
                    {   
                        // 각도, 위치 초기화
                        rotateTrans.Angle = 0;
                        Left = screenWidth / 2 - Width / 2;
                        Top = screenHeight / 2 - Height / 2;

                        // 기준 위치 맞추기
                        criteriaLeft = Left;
                        criteriaTop = Top;

                        // 타이머 재설정
                        followCursorTimer.Stop();
                        nonFollowTimer.Start();
                    }

                    // 속도 100이고 상대위치 설정되어 있으며 커서 따라오기가 설정되어 있을 경우
                    if (followSpeed == 100 && isRelativePositionFixed && isFollow)
                    {
                        rotateTrans.Angle = 0;
                        followCursorTimer.Stop();
                        relativePositionFIxedTimer.Start();
                    }

                    // Topmost 변경
                    Topmost = isTop;
                }));
            };
            followCursorTimer.Interval = intervalTime;
            followCursorTimer.Start();  //커서 따라오기 timer 종료


            // 속도 100이고 상대위치 설정되어 있으며 커서 따라오기가 설정되어 있을 경우
            relativePositionFIxedTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    // 캐릭터 상대위치 고정
                    switch (relativePositionIndex)
                    {
                        case 0:
                            Left = Control.MousePosition.X - Width;
                            Top = Control.MousePosition.Y - Height;
                            break;
                        case 1:
                            Left = Control.MousePosition.X - Width;
                            Top = Control.MousePosition.Y;
                            break;
                        case 2:
                            Left = Control.MousePosition.X;
                            Top = Control.MousePosition.Y - Height;
                            break;
                        default:
                            Left = Control.MousePosition.X;
                            Top = Control.MousePosition.Y;
                            break;
                    }

                    // 조건 미충족 시 타이머 변경
                    if(followSpeed != 100 || !isRelativePositionFixed || !isFollow)
                    {
                        relativePositionFIxedTimer.Stop();
                        followCursorTimer.Start();
                    }

                    // Topmost 변경
                    Topmost = isTop;
                }));
            };
            relativePositionFIxedTimer.Interval = intervalTime;

            double currentMoveY = 0;
            double instanceVolume = 0;
            double preVolume = 0;

            // 마우스 follow 비활성화 시 타이머(춤추거나 정지)
            nonFollowTimer.Elapsed += delegate
            {
                Dispatcher.Invoke(new Action(delegate
                {
                    // 스톱워치가 작동 중일경우
                    if(delay.IsRunning)
                    {
                        // 딜레이 평균값으로 이 타이머의 interval 재설정
                        delay.Stop();
                        delayCount++;
                        delaySum += delay.ElapsedMilliseconds;
                        delayAvg = delaySum / delayCount;
                        danceIntervalTime = delayAvg;
                        delay.Reset();
                        delay.Start();
                    } else
                    {
                        // 변수 초기화 후 시작
                        delaySum = 0;
                        delayAvg = 0;
                        delayCount = 0;
                        delay.Start();
                    }
                    
                    // 커서 따라오기가 설정되어 있는 경우
                    if (isFollow)
                    {
                        // 타이머 재설정
                        nonFollowTimer.Stop();
                        followCursorTimer.Start();
                    }
                    else if (isMouseDown)   // 캐릭터 클릭 시
                    {
                        // 캐릭터 위치, 기준 위치 재설정
                        Left = Control.MousePosition.X - Width / 2;
                        Top = Control.MousePosition.Y - Height / 2;
                        criteriaLeft = Left;
                        criteriaTop = Top;
                    }
                    else if (isDance)    // 춤 설정이 되어있는 경우
                    {
                        // 조건부 스레드 실행
                        SetNowVolumeThread();

                        // 캐릭터가 지면에 있거나 올라가는 상황이 아니라면
                        if (Top == criteriaTop || !isUp)
                        {
                            instanceVolume = volume * 100 * jumpPower;
                        }

                        // 올라가는 상황인 경우
                        if (isUp)
                        {
                            if (currentMoveY >= 0) // 볼륨이 클수록 높게 점프, 캐릭터 속도가 음수가 아니라면
                            {
                                // 캐릭터 계속 위로 올리기
                                passedTime += danceIntervalTime;
                                currentMoveY = ((firstSpeedY - gConverted * passedTime) + (firstSpeedY - gConverted * (passedTime - danceIntervalTime))) / 2 * danceIntervalTime;
                                Top -= currentMoveY;
                            }
                            else
                            {
                                // 끝까지 다 올라간 것이므로 떨어져야
                                isUp = false;
                            }
                        }
                        else if (preVolume + (100 - sensitivity) * 0.1 * jumpPower < instanceVolume)    // 올라가지 않고 preVolume + 보정값 보다 volume이 클 때
                        {
                            // 점프, 변수 초기화 (isUp이 true, delay는 running이 되어야 하므로 initDanceVar 사용 불가)
                            isUp = true;
                            passedTime = 0;
                            currentMoveY = 0;
                            firstSpeedY = Math.Pow(2 * gConverted * instanceVolume, 0.5);
                        }
                        else if (Top < criteriaTop)  // 올라가지 않고 volume이 preVolume보다 작거나 같으면서 criteriaTop보다 위에 있으면 떨어져야.
                        {
                            // 캐릭터 계속 아래로 떨어뜨리기
                            passedTime += danceIntervalTime;
                            currentMoveY = ((firstSpeedY - gConverted * passedTime) + (firstSpeedY - gConverted * (passedTime - danceIntervalTime))) / 2 * danceIntervalTime;

                            // 다음 턴에 지면보다 캐릭터가 아래에 있다면
                            if (Top - currentMoveY > criteriaTop)
                            {
                                Top = criteriaTop;
                            }
                            else
                            {
                                Top -= currentMoveY;
                            }
                        }

                        // 이전 볼륨 설정
                        preVolume = instanceVolume;
                        
                    } else if (isTestDance)     // 점프 테스트 클릭 시
                    {
                        // 올라가는 상황이 아니라면(여기서는 지면에 있다면)
                        if (!isUp)
                        {
                            instanceVolume = 10 * jumpPower;
                            firstSpeedY = Math.Pow(2 * gConverted * instanceVolume, 0.5);
                            isUp = true;
                        }
                        else // 볼륨이 클수록 높게 점프, 캐릭터 속도가 음수가 아니라면
                        {
                            // (아래 변수들은 isTestDance가 설정되면서 초기화됨)
                            passedTime += danceIntervalTime;
                            currentMoveY = ((firstSpeedY - gConverted * passedTime) + (firstSpeedY - gConverted * (passedTime - danceIntervalTime))) / 2 * danceIntervalTime;
                            if (Top - currentMoveY > criteriaTop)
                            {
                                Top = criteriaTop;

                                // 변수 초기화 (delay는 running이 되어야 하므로 initDanceVar 사용 불가)
                                isUp = false;
                                isTestDance = false;
                                passedTime = 0;
                            }
                            else
                            {
                                Top -= currentMoveY;
                            }
                        }
                       
                    }
                    //Console.WriteLine(delay.ElapsedMilliseconds);
                    // Topmost 변경
                    Topmost = isTop;
                }));
            };
            nonFollowTimer.Interval = intervalTime;

            // 캐릭터 마우스 클릭 시
            MouseDown += MainWindow_MouseDown;
            MouseUp += MainWindow_MouseUp;

            //시스템 트레이 아이콘
            var notiThread = new Thread(delegate ()
            {
                var menu = new System.Windows.Forms.ContextMenu();
                var noti = new NotifyIcon
                {
                    Icon = Properties.Resources.characterIcon,
                    Visible = true,
                    Text = "호에에...",
                    ContextMenu = menu,
                };
                var item = new MenuItem
                {
                    Index = 0,
                    Text = "설정"
                };
                var item2 = new MenuItem
                {
                    Index = 1,
                    Text = "끠바ㅠㅠ"
                };
                // 설정 클릭 시
                item.Click += (object o, EventArgs e) =>
                {
                    OpenSettingWindow();
                };
                // 프로그램 종료 클릭 시
                item2.Click += (object o, EventArgs e) =>
                {
                    // 모든 타이머 종료
                    followCursorTimer.Stop();
                    relativePositionFIxedTimer.Stop();
                    nonFollowTimer.Stop();

                    // 아이템 클릭 못하게
                    item.Enabled = false;

                    // 종료 Text 변경
                    item2.Text = "빨리가-ㅅ-";

                    // SettingWindow가 열려 있다면
                    Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
                    {
                        foreach (Window w in System.Windows.Application.Current.Windows)
                        {
                            if (w is SettingWindow)
                            {
                                // 창 닫기
                                w.Close();
                            }
                        }

                    }));

                    // 위로 끝까지 올라가면 종료
                    System.Timers.Timer terminateTimer = new System.Timers.Timer();
                    terminateTimer.Elapsed += delegate
                    {
                        Dispatcher.Invoke(new Action(delegate
                        {
                            // 올라가는 속도는 5
                            Top -= 5;
                            if (Top < 0 - Height)
                            {
                                noti.Dispose();
                                Environment.Exit(0);
                            }
                            Topmost = isTop;
                        }));
                    };
                    terminateTimer.Interval = intervalTime;
                    terminateTimer.Start();
                    
                };

                menu.MenuItems.Add(item);
                var separator = new MenuItem("-");
                menu.MenuItems.Add(separator);
                menu.MenuItems.Add(item2);
                noti.ContextMenu = menu;
                System.Windows.Forms.Application.Run();
            });
            notiThread.SetApartmentState(ApartmentState.STA);
            notiThread.Start();

        }// 생성자 종료

        // SettingWindow 열기
        private void OpenSettingWindow()
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                bool isWindowOpen = false;

                // 창이 이미 열려 있다면 다시 활성화  
                foreach (Window w in System.Windows.Application.Current.Windows)
                {
                    if (w is SettingWindow)
                    {
                        isWindowOpen = true;    
                        w.Activate();
                    }
                }

                // 창이 열려 있지 않다면 열기
                if (!isWindowOpen)
                {
                    SettingWindow SettingWindow = new SettingWindow();
                    SettingWindow.IsFollowCursorDataSendEvent += SetIsFollow;
                    SettingWindow.FollowSpeedSendEvent += SetFollowSpeed;
                    SettingWindow.SizeSendEvent += SetSize;
                    SettingWindow.IsRelativePositionFixedDataSendEvent += SetIsRelativePositionFixed;
                    SettingWindow.RelativePositionIndexSendEvent += SetRelativePositionIndex;
                    SettingWindow.IsTopSendEvent += SetIsTop;
                    SettingWindow.IsDanceSendEvent += SetIsDance;
                    SettingWindow.JumpPowerSendEvent += SetJumpPower;
                    SettingWindow.SensitivitySendEvent += SetSensitive;
                    SettingWindow.GSendEvent += SetG;
                    SettingWindow.IsTestDanceSendEvent += SetIsTestDance;
                    SettingWindow.Show();
                }
                
            }));
            
        }

        // SettingWindow에서 값 받아온 즉시 필드 대입
        private void SetIsFollow(bool item)
        {
            isFollow = item;

            // dance 실행 -> 커서 따라오기 실행 -> dance 실행되면 필드 값은 무조건 초기화되어야.
            if (!isFollow)
            {
                InitDanceVar();
            }
            Properties.Settings.Default.isFollow = isFollow;
            Properties.Settings.Default.Save();
        }
        private void SetFollowSpeed(byte item)
        {
            followSpeed = item;
            Properties.Settings.Default.followSpeed = followSpeed;
            Properties.Settings.Default.Save();
        }
        private void SetSize(byte item)
        {
            size = item;
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                Width = 30 + (double)(characterWidth - 30) / 1000 * size * 10;
                Height = Width * ((double)characterHeight / characterWidth);
            }));
            Properties.Settings.Default.size = size;
            Properties.Settings.Default.Save();
        }
        private void SetIsRelativePositionFixed(bool item)
        {
            isRelativePositionFixed = item;
            Properties.Settings.Default.isRelativePositionFixed = isRelativePositionFixed;
            Properties.Settings.Default.Save();
        }
        private void SetRelativePositionIndex(byte item)
        {
            relativePositionIndex = item;
            Properties.Settings.Default.relativePositionIndex = relativePositionIndex;
            Properties.Settings.Default.Save();
        }
        private void SetIsTop(bool item)
        {
            isTop = item;
            Properties.Settings.Default.isTop = isTop;
            Properties.Settings.Default.Save();
        }
        private void SetIsDance(bool item)
        {
            // 점프 도중 설정 바꾸는 것을 고려하여 초기화
            InitDanceVar();
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                // 춤 설정 변경 시 캐릭터가 너무 위에 있으면
                if (Top < 0)
                {
                    // 중간으로 재배치
                    Left = screenWidth / 2 - Width / 2;
                    Top = screenHeight / 2 - Height / 2;
                    criteriaTop = Top;
                    criteriaLeft = Left;
                } else
                {
                    // 아래처럼 설정해야 점프 도중 설정이 바뀌어도 그 위치로 기준이 설정됨
                    Top = criteriaTop;
                    Left = criteriaLeft;
                }
            }));
            isDance = item;
            Properties.Settings.Default.isDance = isDance;
            Properties.Settings.Default.Save();
        }
        private void SetJumpPower(byte item)
        {
            jumpPower = item;
            Properties.Settings.Default.jumpPower = jumpPower;
            Properties.Settings.Default.Save();
        }
        private void SetSensitive(byte item)
        {
            sensitivity = item;
            Properties.Settings.Default.sensitivity = sensitivity;
            Properties.Settings.Default.Save();
        }
        private void SetG(double item)
        {
            Dispatcher.Invoke(DispatcherPriority.Normal, new Action(delegate
            {
                // m/s^2 -> pixel/ms^2 변환 (키는 157cm 고정, Height은 머리카락과 여백을 보정)
                gConverted = item * (Height * 0.64 / 1.57) * ((double)1 / 1000000);    
            }));
            Properties.Settings.Default.g = item;
            Properties.Settings.Default.Save();
        }
        private void SetIsTestDance(bool item)
        {
            isTestDance = item;
        }

        // 춤추기 기능 관련 주요 변수 초기화
        private void InitDanceVar()
        {
            isUp = false;
            isTestDance = false;
            passedTime = 0;
            // currentMoveY = 0;
            // instanceVolume = 0;
            // preVolume = 0;
            if (delay.IsRunning)
            {
                delay.Stop();
                delay.Reset();
            }
        }

        // 현재 사운드 레벨 설정하는 메서드
        public void SetNowVolumeThread()
        {
            if (!isSetNowVolumeThreadExecute)
            {
                isSetNowVolumeThreadExecute = true;
                var audioThread = new Thread(delegate ()
                {
                    while (true)
                    {
                        if (!isFollow && isDance)   //마우스 커서 따라오기 해제이고 춤 설정일 경우에만 스레드 실행
                        {
                            double volumeTemp = 0;
                            using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
                            {
                                using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
                                {
                                    foreach (var session in sessionEnumerator)
                                    {
                                        using (var audioMeterInformation = session.QueryInterface<AudioMeterInformation>())
                                        {
                                            volumeTemp += audioMeterInformation.GetPeakValue();
                                        }
                                    }
                                }
                            }
                            volume = volumeTemp;
                        } else
                        {
                            isSetNowVolumeThreadExecute = false;
                            volume = 0;
                            break;
                        }
                        
                    }

                });
                audioThread.SetApartmentState(ApartmentState.MTA);
                audioThread.Start();
            }
            
        }
        public AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
        {
            using (var enumerator = new MMDeviceEnumerator())
            {
                using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
                {
                    var sessionManager = AudioSessionManager2.FromMMDevice(device);
                    return sessionManager;
                }
            }
        }

        // 캐릭터 클릭 시 실행 메서드
        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
        }
        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = false;
        }

        // 현재 모니터 주사율 가져오는 메서드
        private int GetSettings(ref DEVMODE dm) { return GetSettings(ref dm, NativeMethods.ENUM_CURRENT_SETTINGS); }
        private int GetSettings(ref DEVMODE dm, int iModeNum) { return NativeMethods.EnumDisplaySettings(null, iModeNum, ref dm); }
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct DEVMODE
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmDeviceName;

            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string dmFormName;

            public short dmLogPixels;
            public short dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
            public int dmICMMethod;
            public int dmICMIntent;
            public int dmMediaType;
            public int dmDitherType;
            public int dmReserved1;
            public int dmReserved2;
            public int dmPanningWidth;
            public int dmPanningHeight;
        };
        public class NativeMethods
        {
            // PInvoke declaration for EnumDisplaySettings Win32 API
            [DllImport("user32.dll", CharSet = CharSet.Ansi)]
            public static extern int EnumDisplaySettings(string lpszDeviceName, int iModeNum, ref DEVMODE lpDevMode);

            // PInvoke declaration for ChangeDisplaySettings Win32 API
            [DllImport("user32.dll", CharSet = CharSet.Ansi)]
            public static extern int ChangeDisplaySettings(ref DEVMODE lpDevMode, int dwFlags);

            // helper for creating an initialized DEVMODE structure
            public static DEVMODE CreateDevmode()
            {
                DEVMODE dm = new DEVMODE();
                dm.dmDeviceName = new String(new char[32]);
                dm.dmFormName = new String(new char[32]);
                dm.dmSize = (short)Marshal.SizeOf(dm);
                return dm;
            }

            // constants
            public const int ENUM_CURRENT_SETTINGS = -1;
            public const int DISP_CHANGE_SUCCESSFUL = 0;
            public const int DISP_CHANGE_BADDUALVIEW = -6;
            public const int DISP_CHANGE_BADFLAGS = -4;
            public const int DISP_CHANGE_BADMODE = -2;
            public const int DISP_CHANGE_BADPARAM = -5;
            public const int DISP_CHANGE_FAILED = -1;
            public const int DISP_CHANGE_NOTUPDATED = -3;
            public const int DISP_CHANGE_RESTART = 1;
            public const int DMDO_DEFAULT = 0;
            public const int DMDO_90 = 1;
            public const int DMDO_180 = 2;
            public const int DMDO_270 = 3;
        }

        // 중복 실행 방지 메서드
        Mutex mutex = null;
        private void Duplicate_execution(string mutexName)
        {
            try
            {
                mutex = new Mutex(false, mutexName);
            }
            catch
            {
                Environment.Exit(0);
            }
            if (mutex.WaitOne(0, false))
            {
                InitializeComponent();
            }
            else
            {
                System.Windows.MessageBox.Show("두 명의 캐릭터는 존재할 수 없다!", "알림", MessageBoxButton.OK, MessageBoxImage.Warning);
                Environment.Exit(0);
            }
        }
    }
}