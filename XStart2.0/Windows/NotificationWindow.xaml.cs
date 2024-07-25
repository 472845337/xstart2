using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window {
        NotifyData vm = new NotifyData();
        public double TopFrom {
            get; set;
        }
        // 保留时长，多少秒
        public int SaveTime { get; set; } = 5;

        public NotificationWindow(string title, string content, Color background, int height, int saveTime) {
            InitializeComponent();
            Loaded += NotificationWindow_Loaded;
            vm.Title = title;
            vm.Background = background.ToString();
            vm.Height = height;
            vm.Content = content;
            vm.SaveTime = saveTime;
            DataContext = vm;
        }


        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e) {

            if (sender is NotificationWindow self) {
                self.UpdateLayout();
                if (Config.Configs.audio) {
                    SystemSounds.Asterisk.Play();//播放提示声
                }

                double right = SystemParameters.WorkArea.Right;//工作区最右边的值
                self.Top = self.TopFrom - self.ActualHeight;
                DoubleAnimation animation = new DoubleAnimation {
                    Duration = new Duration(TimeSpan.FromMilliseconds(500)),//NotifyTimeSpan是自己定义的一个int型变量，用来设置动画的持续时间
                    From = right,
                    To = right - self.ActualWidth//设定通知从右往左弹出
                };
                self.BeginAnimation(LeftProperty, animation);//设定动画应用于窗体的Left属性

                Task.Factory.StartNew(delegate {
                    int seconds = self.SaveTime;//通知持续多少秒后消失
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    //Invoke到主进程中去执行
                    this.Dispatcher.Invoke(delegate {
                        animation = new DoubleAnimation {
                            Duration = new Duration(TimeSpan.FromMilliseconds(500)),
                            From = right - self.ActualWidth,
                            To = right//通知从左往右收回
                        };
                        animation.Completed += (s, a) => { self.Close(); };//动画执行完毕，关闭当前窗体
                        self.BeginAnimation(LeftProperty, animation);
                    });
                });
            }
        }
    }
}
