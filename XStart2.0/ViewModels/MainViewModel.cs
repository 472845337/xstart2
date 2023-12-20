using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Input;
using XStart.Bean;
using XStart.Config;
using XStart.Const;
using XStart.Services;
using XStart.Utils;
using XStart2._0.Bean;
using XStart2._0.Commands;
using XStart2._0.Utils;

namespace XStart2._0.ViewModels {
    internal class MainViewModel : INotifyPropertyChanged {
        public TypeService typeService = TypeService.Instance;
        public ColumnService columnService = ColumnService.Instance;
        public ProjectService projectService = ProjectService.Instance;
        public bool InitFinished { get; set; } = false;


        public MainViewModel() {
            // 计算初始化信息
            avatar = "/Files/Images/DefaultUser.png";
            nickName = "昵称";
            // 日期
            CurrentDay = DateTime.Now.ToString("D");
            CurrentTime = DateTime.Now.ToString("T");
            // 天干地支和星期
            LunarCalendar lc = LunarCalendar.Now;
            LunarYear = lc.GetEraYear();
            LunarMonth = lc.GetEraMonth();
            LunarDay = lc.GetEraDay();
            WeekDay = lc.ChineseWeek;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
        private string lunarYear;
        public string LunarYear { get => lunarYear; set { lunarYear = value;OnPropertyChanged("LunarYear"); } }
        private string lunarMonth;
        public string LunarMonth { get => lunarMonth; set { lunarMonth = value; OnPropertyChanged("LunarMonth"); } }
        private string lunarDay;
        public string LunarDay { get => lunarDay; set { lunarDay = value; OnPropertyChanged("LunarDay"); } }
        private string weekDay;
        public string WeekDay { get => weekDay; set { weekDay = value; OnPropertyChanged("WeekDay"); } }
        #endregion
        #region 应用数据
        private ObservableDictionary<string, XStart.Bean.Type> types;
        public ObservableDictionary<string, XStart.Bean.Type> Types {
            get => types;
            set { types = value; OnPropertyChanged("Types"); }
        }
        #endregion

        #region 窗口相关
        // 主窗口高度
        private double mainHeight = 800;
        public double MainHeight { get => mainHeight;set { mainHeight = value; OnPropertyChanged("MainHeight"); } }
        // 主窗口宽度
        private double mainWidth = 450;
        public double MainWidth { get => mainWidth; set { mainWidth = value; OnPropertyChanged("MainWidth"); } }
        // 主窗口左边位置
        private double mainLeft;
        public double MainLeft { get => mainLeft; set { mainLeft = value; OnPropertyChanged("MainLeft"); } }
        // 主窗口顶部位置
        private double mainTop;
        public double MainTop { get => mainTop; set { mainTop = value; OnPropertyChanged("MainTop"); } }
        // 类别宽度（TabControl的TabItem的宽度）
        private int typeWidth;
        public int TypeWidth { get => typeWidth; set { typeWidth = value; OnPropertyChanged("TypeWidth"); } }
        // 类别名称是否展开
        private bool typeTabExpanded;
        public bool TypeTabExpanded { get => typeTabExpanded; set { typeTabExpanded = value; OnPropertyChanged("ByteTabExpanded"); } }
        // 类别名称开关图标
        private string typeTabToggleIcon;
        public string TypeTabToggleIcon { get => typeTabToggleIcon; set { typeTabToggleIcon = value; OnPropertyChanged("TypeTabToggleIcon"); } }
        #endregion
        private int selectedIndex;
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged("SelectedIndex"); } }
        private double tabControlActualHeight;
        public double TabControlActualHeight { get => tabControlActualHeight; set { tabControlActualHeight = value; OnPropertyChanged("TabControlActualHeight"); } }

    }
}
