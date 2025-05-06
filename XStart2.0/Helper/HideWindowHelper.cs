using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
using XStart2._0.Config;
using Utils;

namespace XStart2._0.Helper {
    public class HideWindowHelper {

        private readonly Window _window;
        private readonly System.Windows.Threading.DispatcherTimer _timer;
        private readonly List<HideCore> _hideLogicList = new List<HideCore>();

        private bool _isHide;
        private bool _isStarted;
        private HideCore _lastHiderOn;
        private bool _isInAnimation;

        private HideWindowHelper(Window window) {
            _window = window;
            _timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(200) };
            _timer.Tick += Timer_Tick;
        }


        public HideWindowHelper AddHider<THideCore>() where THideCore : HideCore, new() {
            if (_isStarted) throw new Exception("调用了Start方法后无法在添加隐藏逻辑");
            var logic = new THideCore();
            logic.Init(_window, AnimationReport);
            _hideLogicList.Add(logic);
            return this;
        }

        readonly static int step = 1;
        public static void Animate2Location(Window window, Point destination) {
            double curLeft = NumberUtils.Accuracy(window.Left, 2);
            double curTop = NumberUtils.Accuracy(window.Top, 2);
            double indexX = NumberUtils.Accuracy((destination.X - curLeft) / 200, 2);
            double indexY = NumberUtils.Accuracy((destination.Y - curTop) / 200, 2);
            if (indexX == 0) {
                indexX = destination.X > curLeft ? step : -step;
            }
            if (indexY == 0) {
                indexY = destination.Y > curTop ? step : -step;
            }
            int index = 1;
            bool xStop = false;
            bool yStop = false;
            double aniLocationX = curLeft;
            double aniLocationY = curTop;
            if (indexX == 0) {
                xStop = true;
            }
            if (indexY == 0) {
                yStop = true;
            }
            while (!xStop || !yStop) {
                if (!xStop) {
                    if (indexX < 0) {
                        if (NumberUtils.Accuracy(aniLocationX + indexX, 2) <= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX = NumberUtils.Accuracy(aniLocationX + indexX, 2);
                        }
                    } else {
                        if (NumberUtils.Accuracy(aniLocationX + indexX, 2) >= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX = NumberUtils.Accuracy(aniLocationX + indexX, 2);
                        }
                    }
                }
                if (!yStop) {
                    if (indexY < 0) {
                        if (NumberUtils.Accuracy(aniLocationY + indexY, 2) <= destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY = NumberUtils.Accuracy(aniLocationY + indexY, 2);
                        }
                    } else {
                        if (NumberUtils.Accuracy(aniLocationY + indexY, 2) >= destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY = NumberUtils.Accuracy(aniLocationY, 2) + indexY;
                        }
                    }
                }
                window.Left = aniLocationX;
                window.Top = aniLocationY;
                index++;
            }
        }

        private void Timer_Tick(object sender, EventArgs e) {
            if (_isInAnimation) return;
            if (_window.IsActive) return;
            DllUtils.Point curPoint = new DllUtils.Point();
            DllUtils.GetCursorPos(ref curPoint); //获取鼠标相对桌面的位置
            // 对鼠标位置进行缩放比例计算
            int x = (int)(curPoint.X / Configs.scale.Item1);
            int y = (int)(curPoint.Y / Configs.scale.Item2);

            var isMouseEnter = x >= _window.Left
                               && x <= _window.Left + _window.Width + 1
                               && y >= _window.Top
                               && y <= _window.Top
                               + _window.Height + 1;
            //鼠标在里面
            if (isMouseEnter) {
                //没有隐藏，直接返回
                if (!_isHide) return;

                //理论上不会出现为null的情况
                if (_lastHiderOn != null) {
                    _lastHiderOn.Show();
                    _isHide = false;
                    if (Configs.LockHandler.ToInt32() == 0) {
                        // 打开当前窗口
                        DllUtils.SwitchToThisWindow(Configs.Handler, true);
                        DllUtils.ShowWindow(Configs.Handler, WinApi.SW_SHOW);
                    }
                    //_window.ShowInTaskbar = true;
                    return;
                }
            }


            foreach (var core in _hideLogicList) {
                //鼠标在里面并且没有隐藏
                if (isMouseEnter && !_isHide) return;

                //鼠标在里面并且当期是隐藏状态且当前处理器成功显示了窗体
                if (isMouseEnter && _isHide && core.Show()) {
                    _isHide = false;
                    //_window.ShowInTaskbar = true;
                    return;
                }

                //鼠标在外面并且没有隐藏，那么调用当前处理器尝试隐藏窗体
                if (!isMouseEnter && !_isHide && core.Hide()) {
                    _lastHiderOn = core;
                    _isHide = true;
                    //_window.ShowInTaskbar = false;
                    return;
                }
            }
        }

        private void AnimationReport(bool isInAnimation) {
            _isInAnimation = isInAnimation;
        }

        public HideWindowHelper Start() {
            _isStarted = true;
            _timer.Start();
            return this;
        }

        public void Stop() {
            _timer.Stop();
            _isStarted = false;
        }

        public static HideWindowHelper CreateFor(Window window) {
            return new HideWindowHelper(window);
        }

        public void TryShow() {
            if (_lastHiderOn == null) return;
            _lastHiderOn.Show();
            _isHide = false;
            _window.Activate();
        }
    }


    #region 隐藏逻辑基类

    public abstract class HideCore {
        private Window _window;
        private Action<bool> _animationStateReport;

        internal void Init(Window window, Action<bool> animationStateReport) {
            _window = window;
            _animationStateReport = animationStateReport;
        }

        public abstract bool Show();

        public abstract bool Hide();

        protected Window WindowInstance => _window;

        protected void StartAnimation(DependencyProperty property, double from, double to) {
            _animationStateReport(true);
            var doubleAnimation = new DoubleAnimation(from, to, new Duration(TimeSpan.FromSeconds(0.5))) {
                AccelerationRatio = 0, // 设置加速占比为一半, 即 0.5
                DecelerationRatio = 1, // 设置减速占比为0, 其实这里可以省略, 因为默认是0
            };
            doubleAnimation.Completed += delegate {
                _window.BeginAnimation(property, null);
                _animationStateReport(false);
            };
            _window.BeginAnimation(property, doubleAnimation);
        }
    }

    #endregion

    #region 向上隐藏

    class HideOnTop : HideCore {
        public override bool Show() {
            if (WindowInstance.Top > 0) return false;
            StartAnimation(Window.TopProperty, WindowInstance.Top, 0);
            return true;
        }

        public override bool Hide() {
            if (WindowInstance.Top > 2) return false;
            StartAnimation(Window.TopProperty, WindowInstance.Top, 0 - WindowInstance.Top - WindowInstance.Height + 2);
            return true;
        }
    }

    #endregion

    #region 向左隐藏

    class HideOnLeft : HideCore {
        public override bool Show() {
            if (WindowInstance.Left > 0) return false;
            StartAnimation(Window.LeftProperty, WindowInstance.Left, 0);
            return true;
        }

        public override bool Hide() {
            if (WindowInstance.Left > 2) return false;
            StartAnimation(Window.LeftProperty, WindowInstance.Left, 0 - WindowInstance.Width + 2);
            return true;
        }
    }

    #endregion

    #region 向右隐藏

    class HideOnRight : HideCore {
        private readonly double _screenWidth;

        public HideOnRight() {
            //foreach (var screen in Screen.AllScreens) {
            //    _screenWidth += screen.Bounds.Width;
            //}
            _screenWidth = SystemParameters.PrimaryScreenWidth;
        }

        public override bool Show() {
            if (_screenWidth - WindowInstance.Left - WindowInstance.Width > 0) return false;
            StartAnimation(Window.LeftProperty, WindowInstance.Left, _screenWidth - WindowInstance.Width);
            return true;
        }

        public override bool Hide() {
            if (_screenWidth - WindowInstance.Left - WindowInstance.Width > 2) return false;
            StartAnimation(Window.LeftProperty, WindowInstance.Left, _screenWidth - 2);
            return true;
        }
    }

    #endregion
}
