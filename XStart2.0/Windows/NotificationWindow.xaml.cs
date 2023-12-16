using System;
using System.Media;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using XStart2._0.ViewModels;

namespace XStart2._0.Windows {
    /// <summary>
    /// NotificationWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotificationWindow : Window {
        public double TopFrom {
            get; set;
        }
        // 消息标题
        public string NotifyTitle { get; set; }
        // 消息的背景色
        public Color NotifyBackground { get; set; }
        // 消息内容
        public string NotifyContent { get; set; }
        // 保留时长，多少秒
        public int SaveTime { get; set; } = 5;

        private NotifyData notifyData = new NotifyData();
        public NotificationWindow() {
            InitializeComponent();
            DataContext = notifyData;
            Loaded += NotificationWindow_Loaded;
        }


        private void NotificationWindow_Loaded(object sender, RoutedEventArgs e) {
            notifyData.Title = NotifyTitle;
            notifyData.Background = NotifyBackground.ToString();
            notifyData.Content = NotifyContent;
            NotificationWindow self = sender as NotificationWindow;
            if (self != null) {
                self.UpdateLayout();
                SystemSounds.Asterisk.Play();//播放提示声

                double right = SystemParameters.WorkArea.Right;//工作区最右边的值
                self.Top = self.TopFrom - self.ActualHeight;
                DoubleAnimation animation = new DoubleAnimation();
                animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));//NotifyTimeSpan是自己定义的一个int型变量，用来设置动画的持续时间
                animation.From = right;
                animation.To = right - self.ActualWidth;//设定通知从右往左弹出
                self.BeginAnimation(Window.LeftProperty, animation);//设定动画应用于窗体的Left属性

                Task.Factory.StartNew(delegate {
                    int seconds = self.SaveTime;//通知持续多少秒后消失
                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(seconds));
                    //Invoke到主进程中去执行
                    this.Dispatcher.Invoke(delegate {
                        animation = new DoubleAnimation();
                        animation.Duration = new Duration(TimeSpan.FromMilliseconds(500));
                        animation.Completed += (s, a) => { self.Close(); };//动画执行完毕，关闭当前窗体
                        animation.From = right - self.ActualWidth;
                        animation.To = right;//通知从左往右收回
                        self.BeginAnimation(Window.LeftProperty, animation);
                    });
                });
            }
        }
    }
}
