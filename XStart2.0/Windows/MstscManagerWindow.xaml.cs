using MSTSCLib;
using System;
using System.Windows;
using System.Windows.Interop;
using XStart2._0.Bean;

namespace XStart2._0.Windows {
    /// <summary>
    /// MstscManagerWindow.xaml 的交互逻辑
    /// 使用了WinForm的远程控件，因为Wpf是数据驱动的，无法动态加载AxMsTscAxNotSafeForScripting
    /// 只能使用WinForm的TabControl来加载不同的rdp连接
    /// </summary>
    public partial class MstscManagerWindow : Window {

        private readonly System.Windows.Threading.DispatcherTimer RdpTimer = new System.Windows.Threading.DispatcherTimer() { Interval = TimeSpan.FromMilliseconds(200) };
        public MstscManagerWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
            ContentRendered += Window_ContentRendered;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            var rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
            if (rdpTabControl.TabPages.Count > 0) {
                if (MessageBoxResult.OK == MessageBox.Show("当前存在远程连接，确认关闭吗？", Const.Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    // 关闭所有连接
                    CloseAllRdp();
                } else {
                    e.Cancel = true;
                    return;
                }
            }
            if (Config.Configs.mstscRealClose) {
                Config.Configs.MstscHandler = IntPtr.Zero;
                e.Cancel = false;
            } else {
                Hide();
                e.Cancel = true;
            }
        }

        Rdp newRdp;
        public void AddRdp(string id, string title, string server, int port, string account, string password) {
            newRdp = new Rdp() { Id = id, Title = title, Server = server, Port = port, Account = account, Password = password };
        }

        public void Window_ContentRendered(object sender, EventArgs e) {
            RdpTimer.Tick += RdpConnectTick;
            RdpTimer.Start();
        }

        int rdpWidth = 0;
        int rdpHeight = 0;
        private void Window_Loaded(object sender, EventArgs e) {
            var rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
            rdpTabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            rdpTabControl.DrawItem += AppRunTabControl_DrawItem;
            rdpTabControl.MouseDown += AppRunTabControl_MouseDown;
            var rightMenu = new System.Windows.Forms.ContextMenuStrip();
            var reconnectMenu = new System.Windows.Forms.ToolStripMenuItem { Name = "Reconnect", Text = "重新连接" };
            var disconnectMenu = new System.Windows.Forms.ToolStripMenuItem { Name = "Reconnect", Text = "关闭连接" };
            var closeAllMenu = new System.Windows.Forms.ToolStripMenuItem { Name = "Reconnect", Text = "全部关闭" };
            // 右键按钮事件
            reconnectMenu.Click += Reconnect_Click;
            disconnectMenu.Click += Disconnect_Click;
            closeAllMenu.Click += CloseAll_Click;
            // 添加右键菜单
            rightMenu.Items.Add(reconnectMenu);
            rightMenu.Items.Add(disconnectMenu);
            rightMenu.Items.Add(closeAllMenu);
            rdpTabControl.ContextMenuStrip = rightMenu;
            int height = rdpTabControl.ClientSize.Height;
            int width = rdpTabControl.ClientSize.Width;
            if (height < 800) {
                height = 800;
            }
            if (width < 1000) {
                width = 1000;
            }
            rdpHeight = height - 32;
            rdpWidth = width - 10;
            // 赋值句柄
            var formDependency = PresentationSource.FromDependencyObject(this);
            HwndSource winformWindow = formDependency as HwndSource;
            Config.Configs.MstscHandler = winformWindow.Handle;
        }

        private void RdpConnectTick(object sender, EventArgs e) {
            if (newRdp != null) {
                System.Windows.Forms.TabPage tabPage = new System.Windows.Forms.TabPage {
                    Text = newRdp.Title + "    "// 标题要加长，用于放置自定义的关闭按钮
                };
                AxMSTSCLib.AxMsTscAxNotSafeForScripting rdpScript = new AxMSTSCLib.AxMsTscAxNotSafeForScripting {
                    Height = rdpHeight,
                    Width = rdpWidth
                };
                tabPage.Controls.Add(rdpScript);
                tabPage.Tag = newRdp;
                System.Windows.Forms.TabControl rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
                rdpTabControl.TabPages.Add(tabPage);
                rdpTabControl.SelectedTab = tabPage;
                try {
                    rdpScript.Server = newRdp.Server;
                    rdpScript.UserName = newRdp.Account;

                    rdpScript.ConnectingText = $"正在连接{newRdp.Title}...";
                    // 设置端口和密码
                    IMsRdpClientAdvancedSettings7 iClientSetting = (IMsRdpClientAdvancedSettings7)rdpScript.AdvancedSettings;
                    if (newRdp.Port > 0) {
                        iClientSetting.RDPPort = newRdp.Port;
                    }
                    iClientSetting.ClearTextPassword = newRdp.Password;
                    iClientSetting.EnableSuperPan = false;
                    // 非全屏时也可执行win+*快捷键，对应于mstsc.exe里的本地资源-键盘-应用windows组合键
                    // 0：仅在客户端计算机上本地应用组合键。
                    // 1：在远程服务器上应用组合键。
                    // 2：仅当客户端以全屏模式运行时，才将组合键应用于远程服务器。 这是默认值。
                    IMsRdpClientSecuredSettings securedSetting = (IMsRdpClientSecuredSettings)rdpScript.SecuredSettings;
                    securedSetting.KeyboardHookMode = 1;
                    // 连接远程服务器
                    rdpScript.Connect();
                } catch (Exception Ex) {
                    MessageBox.Show("远程连接异常： " + newRdp.Server + " 错误:  " + Ex.Message, Const.Constants.MESSAGE_BOX_TITLE_ERROR, MessageBoxButton.OKCancel);
                } finally {
                    // 将rdp置为空
                    newRdp = null;
                }
            }
        }

        private void Reconnect_Click(object sender, EventArgs e) {
            System.Windows.Forms.TabControl rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
            if (rdpTabControl.SelectedIndex >= 0) {
                System.Windows.Forms.TabPage tabPage = rdpTabControl.SelectedTab;
                Rdp rdp = (Rdp)tabPage.Tag;
                var rdpScript = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)tabPage.Controls[0];
                // 关闭原连接
                DisconnectRdp(rdpScript);
                tabPage.Controls.Clear();
                // 启用新连接
                rdpScript = new AxMSTSCLib.AxMsTscAxNotSafeForScripting {
                    Height = rdpHeight,
                    Width = rdpWidth
                };
                tabPage.Controls.Add(rdpScript);
                try {
                    rdpScript.Server = rdp.Server;
                    rdpScript.UserName = rdp.Account;
                    rdpScript.ConnectingText = $"正在连接{rdp.Title}...";
                    IMsRdpClientAdvancedSettings7 iClientSetting = (IMsRdpClientAdvancedSettings7)rdpScript.AdvancedSettings;
                    iClientSetting.RDPPort = rdp.Port;
                    iClientSetting.ClearTextPassword = rdp.Password;
                    rdpScript.Connect();
                } catch (Exception Ex) {
                    MessageBox.Show("远程连接异常： " + rdp.Server + " 错误:  " + Ex.Message, Const.Constants.MESSAGE_BOX_TITLE_ERROR, MessageBoxButton.OKCancel);
                }
            }
        }

        private void Disconnect_Click(object sender, EventArgs e) {
            System.Windows.Forms.TabControl rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
            if (rdpTabControl.SelectedIndex >= 0) {
                CloseRdpAndPage(rdpTabControl, rdpTabControl.SelectedIndex);
            }
        }

        private void CloseAll_Click(object sender, EventArgs e) {
            if (MessageBoxResult.OK == MessageBox.Show("确认关闭所有连接吗?", Const.Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                CloseAllRdp();
            }
        }

        private void DisconnectRdp(AxMSTSCLib.AxMsTscAxNotSafeForScripting rdpScript) {
            if (null != rdpScript) {
                if (rdpScript.Connected.ToString() == "1") {
                    rdpScript.Disconnect();
                }
                rdpScript.Dispose();
            }
        }

        readonly int tabPageCloseSize = 15;
        //重绘关闭按钮
        public void AppRunTabControl_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e) {
            try {
                var rdpTabControl = (System.Windows.Forms.TabControl)sender;
                // 重绘tabPage 
                var myTabRect = rdpTabControl.GetTabRect(e.Index);
                //添加TabPage属性
                System.Drawing.SolidBrush bshBack;
                System.Drawing.SolidBrush bshFore;
                if (e.Index == rdpTabControl.SelectedIndex) {
                    //当前Tab页的样式,粗体，橙色背景
                    bshBack = new System.Drawing.SolidBrush(System.Drawing.Color.SkyBlue); //选中的标签颜色变为橙色
                    bshFore = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                } else {
                    //其余Tab页的样式
                    bshBack = new System.Drawing.SolidBrush(System.Drawing.Color.WhiteSmoke);
                    bshFore = new System.Drawing.SolidBrush(System.Drawing.Color.Black);
                }

                //画样式
                e.Graphics.FillRectangle(bshBack, e.Bounds);
                e.Graphics.DrawString(rdpTabControl.TabPages[e.Index].Text, e.Font, bshFore, myTabRect.X + 2, myTabRect.Y + 2);
                //再画一个矩形框

                using (var p = new System.Drawing.Pen(System.Drawing.Color.Black)) {
                    myTabRect.Offset(myTabRect.Width - (tabPageCloseSize + 3), 2);
                    myTabRect.Width = tabPageCloseSize;
                    myTabRect.Height = tabPageCloseSize;
                    e.Graphics.DrawRectangle(p, myTabRect);
                }

                //填充矩形框
                var recColor = e.State == System.Windows.Forms.DrawItemState.Selected ? System.Drawing.Color.Orange : System.Drawing.Color.DarkGray;
                using (System.Drawing.Brush b = new System.Drawing.SolidBrush(recColor)) {
                    e.Graphics.FillRectangle(b, myTabRect);
                }

                rdpTabControl.Appearance = System.Windows.Forms.TabAppearance.Normal;
                //画关闭符号
                using (var p = new System.Drawing.Pen(System.Drawing.Color.White)) {
                    //画"/"线
                    var p1 = new System.Drawing.Point(myTabRect.X + 3, myTabRect.Y + 3);
                    var p2 = new System.Drawing.Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + myTabRect.Height - 3);
                    e.Graphics.DrawLine(p, p1, p2);
                    //画"\"线
                    var p3 = new System.Drawing.Point(myTabRect.X + 3, myTabRect.Y + myTabRect.Height - 3);
                    var p4 = new System.Drawing.Point(myTabRect.X + myTabRect.Width - 3, myTabRect.Y + 3);
                    e.Graphics.DrawLine(p, p3, p4);
                }

                e.Graphics.Dispose();
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }


        public void AppRunTabControl_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            var rdpTabControl = (System.Windows.Forms.TabControl)sender;
            if (e.Button == System.Windows.Forms.MouseButtons.Right) {
                System.Drawing.Point pt = new System.Drawing.Point(e.X, e.Y);
                for (int i = 0; i < rdpTabControl.TabCount; i++) {
                    System.Drawing.Rectangle rect = rdpTabControl.GetTabRect(i);
                    if (rect.Contains(pt))
                        rdpTabControl.SelectedIndex = i;
                }

            } else if (e.Button == System.Windows.Forms.MouseButtons.Left && rdpTabControl.SelectedIndex >= 0) {
                int x = e.X, y = e.Y;
                //计算关闭区域   
                var myTabRect = rdpTabControl.GetTabRect(rdpTabControl.SelectedIndex);
                myTabRect.Offset(myTabRect.Width - (tabPageCloseSize + 3), 2);
                myTabRect.Width = tabPageCloseSize;
                myTabRect.Height = tabPageCloseSize;


                //如果鼠标在区域内就关闭选项卡   
                var isClose = x > myTabRect.X && x < myTabRect.Right && y > myTabRect.Y && y < myTabRect.Bottom;

                if (isClose != true) return;
                // 远程关闭并关闭页面
                CloseRdpAndPage(rdpTabControl, rdpTabControl.SelectedIndex);
            }
        }

        private void CloseAllRdp() {
            System.Windows.Forms.TabControl rdpTabControl = (System.Windows.Forms.TabControl)RdpWfh.Child;
            foreach (System.Windows.Forms.TabPage tabPage in rdpTabControl.TabPages) {
                var rdpScript = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)tabPage.Controls[0];
                DisconnectRdp(rdpScript);
            }
            rdpTabControl.TabPages.Clear();
        }

        private void CloseRdpAndPage(System.Windows.Forms.TabControl tabControl, int index) {
            System.Windows.Forms.TabPage tabPage = tabControl.TabPages[index];
            var rdpScript = (AxMSTSCLib.AxMsTscAxNotSafeForScripting)tabPage.Controls[0];
            if (null != rdpScript && rdpScript.Connected.ToString() == "1") {
                if (MessageBoxResult.OK == MessageBox.Show($"确认退出{tabPage.Text.Trim()}远程", Const.Constants.MESSAGE_BOX_TITLE_WARN, MessageBoxButton.OKCancel)) {
                    DisconnectRdp(rdpScript);
                    tabControl.TabPages.Remove(tabPage);
                }
            } else {
                tabControl.TabPages.Remove(tabPage);
            }
            if (index > 0) {
                tabControl.SelectedIndex = index - 1;
            }
        }
    }
}
