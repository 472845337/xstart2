﻿using System;
using XStart2._0.Services;
using XStart2._0.Bean;

namespace XStart2._0.ViewModels {
    public class MainViewModel : BaseViewModel {
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService projectService = ProjectService.Instance;

        public MainViewModel() {
            mainHeight = 800;
            mainWidth = 450;
            // 计算初始化信息
            avatar = "/Files/Images/DefaultUser.png";
            nickName = "昵称";
        }

        #region 用户数据
        // 头像
        private string avatar;
        public string Avatar { get => avatar; set { avatar = value; OnPropertyChanged("Avatar"); } }
        // 昵称
        private string nickName;
        public string NickName { get => nickName; set { nickName = value; OnPropertyChanged("NickName"); } }
        #endregion

        #region 时间数据
        private string currentDay;
        public string CurrentDay { get => currentDay; set { currentDay = value; OnPropertyChanged("CurrentDay"); } }
        private string currentTime;
        public string CurrentTime { get => currentTime; set { currentTime = value; OnPropertyChanged("CurrentTime"); } }
        private string currentWeekDay;
        public string CurrentWeekDay { get => currentWeekDay; set { currentWeekDay = value; OnPropertyChanged("CurrentWeekDay"); } }
        #endregion
        #region 应用数据
        private ObservableDictionary<string, Bean.Type> types;
        public ObservableDictionary<string, Bean.Type> Types {
            get => types;
            set { types = value; OnPropertyChanged("Types"); }
        }
        #endregion

        #region 窗口相关
        // 主窗口高度
        private double mainHeight;
        public double MainHeight { get => mainHeight;set { mainHeight = value; OnPropertyChanged("MainHeight"); } }
        // 主窗口宽度
        private double mainWidth;
        public double MainWidth { get => mainWidth; set { mainWidth = value; OnPropertyChanged("MainWidth"); } }
        // 主窗口左边位置
        private double mainLeft;
        public double MainLeft { get => mainLeft; set { mainLeft = value; OnPropertyChanged("MainLeft"); } }
        // 主窗口顶部位置
        private double mainTop;
        public double MainTop { get => mainTop; set { mainTop = value; OnPropertyChanged("MainTop"); } }
        // 类别宽度（TabControl的TabItem的宽度）
        private double typeWidth;
        public double TypeWidth { get => typeWidth; set { typeWidth = value; OnPropertyChanged("TypeWidth"); } }
        // 类别名称是否展开
        private bool typeTabExpanded;
        public bool TypeTabExpanded { get => typeTabExpanded; set { typeTabExpanded = value; OnPropertyChanged("ByteTabExpanded"); } }
        // 类别名称开关图标
        private string typeTabToggleIcon;
        public string TypeTabToggleIcon { get => typeTabToggleIcon; set { typeTabToggleIcon = value; OnPropertyChanged("TypeTabToggleIcon"); } }
        #endregion

        #region 窗口相关设置
        private int selectedIndex;
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged("SelectedIndex"); } }
        private double tabControlActualHeight;
        public double TabControlActualHeight { get => tabControlActualHeight; set { tabControlActualHeight = value; OnPropertyChanged("TabControlActualHeight"); } }
        private bool topMost;
        public bool TopMost { get => topMost; set { topMost = value;OnPropertyChanged("TopMost"); } }
        // 音效开关
        private bool audio;
        public bool Audio { get => audio; set { audio = value;OnPropertyChanged("Audio"); } }
        private bool autoRun;
        public bool AutoRun { get => autoRun; set { autoRun = value; OnPropertyChanged("AutoRun"); } }
        private bool exitWarn;
        public bool ExitWarn { get => exitWarn; set { exitWarn = value; OnPropertyChanged("ExitWarn"); } }
        private bool closeBorderHide;
        public bool CloseBorderHide { get => closeBorderHide; set { closeBorderHide = value;OnPropertyChanged("CloseBorderHide"); } }
        private string clickType;
        public string ClickType { get => clickType; set { clickType = value; OnPropertyChanged("ClickType"); } }
        private string urlOpen;
        public string UrlOpen { get => urlOpen; set { urlOpen = value; OnPropertyChanged("UrlOpen"); } }
        private string urlOpenCustomBrowser;
        public string UrlOpenCustomBrowser { get => urlOpenCustomBrowser; set { urlOpenCustomBrowser = value; OnPropertyChanged("UrlOpenCustomBrowser"); } }
        #endregion
    }
}
