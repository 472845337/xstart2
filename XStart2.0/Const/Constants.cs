namespace XStart2._0.Const {
    class Constants {
        public const string APP_NAME = "XStart2_APP";

        public const string URL_OPEN_DEFAULT = "UrlDefault";// 系统默认浏览器打开
        public const string URL_OPEN_EDGE = "UrlEdge";// 微软浏览器打开
        public const string URL_OPEN_CUSTOM = "UrlCustom";// 自定义浏览器打开

        public const string CLICK_TYPE_SINGLE = "1";// 单击
        public const string CLICK_TYPE_DOUBLE = "2";// 双击

        public const string RDP_MODEL_SYSTEM = "1";// 系统远程方式
        public const string RDP_MODEL_CUSTOM = "2";// 个性远程方式

        public const string APP_ICON = "Files/Icons/xstart2.ico";
        public const string ABOUT_FILE = "Files/Doc/about.txt";
        public const string AUDIO_CHANGE = "Files/Audio/Change.wav";
        public const string AUDIO_START = "Files/Audio/Start.wav";
        public const string CITY_JSON_FILE = "Files/Citys/city.json";

        public const string SET_FILE = "Config/Ini/Set.ini";

        public const string SECTION_USER = "User";
        public const string SECTION_LOCATION = "Location";
        public const string SECTION_SIZE = "Size";
        public const string SECTION_CONFIG = "Config";
        public const string SECTION_SYSTEM_APP = "SystemApp";
        public const string SECTION_WEATHER = "Weather";
        public const string SECTION_THEME = "Theme";

        public const string KEY_USER_AVATAR = "Avatar";
        public const string KEY_USER_NICKNAME = "NickName";
        public const string KEY_TYPE_TAB_EXPAND = "TypeTabExpand";
        public const string KEY_CLICK_TYPE = "ClickType";
        public const string KEY_RDP_MODEL = "RdpModel";
        public const string KEY_TOP_MOST = "TopMost";
        public const string KEY_AUDIO = "Audio";
        public const string KEY_AUTO_RUN = "AutoRun";
        public const string KEY_EXIT_WARN = "ExitWarn";
        public const string KEY_STYLE = "Style";
        public const string KEY_OPEN_TYPE = "OpenType";// 启动后打开
        public const string KEY_URL_OPEN = "UrlOpen";
        public const string KEY_URL_OPEN_CUSTOM_BROWSER = "UrlOpenCustomBrowser";
        public const string KEY_ICON_SIZE = "IconSize";// 图标尺寸
        public const string KEY_ORIENTATION = "Orientation";// 排列方式，横排或竖排
        public const string KEY_HIDE_TITLE = "HideTitle";// 隐藏标题
        public const string KEY_ONE_LINE_MULTI = "OneLineMulti";// 一行多个
        public const string KEY_CLOSE_BORDER_HIDE = "CloseBorderHide";// 靠边自动隐藏
        public const string KEY_DEL_COUNT = "DelCount";// 删除次数
        public const string KEY_SYSTEM_PROJECT_OPEN_PAGE = "SystemProjectOpenPage";
        public const string KEY_ADD_MULTI = "AddMulti";

        public const string KEY_LEFT = "Left";
        public const string KEY_TOP = "Top";
        public const string KEY_HEIGHT = "Height";
        public const string KEY_WIDTH = "Width";
        public const string KEY_THEME_NAME = "Name";
        public const string KEY_THEME_CUSTOM = "Custom";

        public const int DEL_COUNT_LIMIT = 100;

        public const string EDGE_PATH = "C:/Program Files (x86)/Microsoft/Edge/Application/msedge.exe";

        public const string TYPE = "type";
        public const string COLUMN = "column";
        public const string PROJECT = "project";

        public const double MAIN_HEIGHT = 800;
        public const double MAIN_WIDTH = 400;
        public const double MAIN_LEFT = 300;
        public const double MAIN_TOP = 50;

        public const double TYPE_EXPAND_WIDTH = 100;
        public const double TYPE_COLLAPSE_WIDTH = 28;

        public const uint VOLUME_WAVE_STEP = 6553;
        public const int VOLUME_STEP = 5;

        public const string DEVICE_NAME_MIC = "microphone";
        public const string DEVICE_NAME_LINE_IN = "linein";
        public const string DEVICE_NAME_CD_PLAYER = "cd player";

        public const char SPLIT_CHAR = '¤';
        public const string SYSTEM_PROJECT_CHAR = "#";

        public const string OPERATE_CREATE = "create";// 创建
        public const string OPERATE_UPDATE = "update";// 更新
        public const string OPERATE_REMOVE = "remove";// 移除
        public const string OPERATE_CUT = "cut";// 剪切
        public const string OPERATE_COPY = "copy";// 剪切

        public const string FONT_FAMILY_FA_SOLID = "pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Solid";
        public const string FONT_FAMILY_FA_REGULAR = "pack://application:,,,/Resources/Fonts/#Font Awesome 6 Free Regular";
        public const string FONT_FAMILY_FA_BRANDS = "pack://application:,,,/Resources/Fonts/#Font Awesome 6 Brands Regular";
        public const string FONT_FAMILY_FA_V4 = "pack://application:,,,/Resources/Fonts/#FontAwesome";
        public const string FA_NAME_SOLID = "Solid";
        public const string FA_NAME_REGULAR = "Regular";
        public const string FA_NAME_BRANDS = "Brands";
        public const string FA_NAME_V4 = "V4";

        public const string MESSAGE_BOX_TITLE_ERROR = "错误";
        public const string MESSAGE_BOX_TITLE_WARN = "警告";
        public const string MESSAGE_BOX_TITLE_INFO = "提醒";

        public const string AVATAR_PATH_DEFAULT = "Files/Images/Avatar/DefaultUser.png";
        public const string AVATAR_PATH_NOTEXIST = "Files/Images/Avatar/NotExist.png";

        public const string NICKNAME_DEFAULT = "我的昵称";

        public const int ICON_SIZE_32 = 32;
        public const int ICON_SIZE_48 = 48;
        public const int ICON_SIZE_72 = 72;
        public const int ICON_SIZE_128 = 128;
        public const int ICON_SIZE_256 = 256;

        public const string STYLE_DEFAULT = "Default";

        public const string ICON_SIZE_STR_SMALL = "Small";
        public const string ICON_SIZE_STR_MID = "Mid";
        public const string ICON_SIZE_STR_LARGE = "Large";
        public const string ICON_SIZE_STR_HUGE = "Huge";
        public const string ICON_SIZE_STR_JUMBO = "Jumbo";

        public const string ORIENTATION_HORIZONTAL = "Horizontal";
        public const string ORIENTATION_VERTICAL = "Vertical";

        #region 天气
        public const string KEY_WEATHER_API = "Api";
        public const string KEY_WEATHER_PROVINCE = "Province";
        public const string KEY_WEATHER_CITY = "City";
        public const string KEY_WEATHER_COUNTRY = "Country";
        public const string KEY_LAST_CITYS = "LastCitys";
        public const string KEY_WEATHER_IMG_THEME = "Theme";
        public const string WEATHER_IMG_THEME_DEFAULT = "yikeyun";// 默认天气主题是易客云自已的
        public const string WEATHER_IMG_THEME_GIF = "gif";

        public const string WEATHER_API_YKY = "易客云";
        public const string WEATHER_API_GAODE = "高德";
        public const string WEATHER_API_SENIVERSE = "心知";
        public const string WEATHER_API_Q = "和风";
        public const string WEATHER_API_OPEN = "OpenWeather";
        public const string WEATHER_API_ACCU = "AccuWeather";
        #region 易客云
        public const string KEY_WEATHER_YKY_APP_ID = "YkyAppId";
        public const string KEY_WEATHER_YKY_APP_SECRET = "YkyAppSecret";
        public const string KEY_WEATHER_YKY_URL = "YkyUrl";
        public const string WEATHER_YKY_API_URL = "http://v1.yiketianqi.com";// API地址
        public const string WEATHER_YKY_REGISTER_URL = "http://www.tianqiapi.com/user/register";// api注册地址
        #endregion
        #region 高德
        public const string KEY_WEATHER_GAODE_URL = "GaodeUrl";
        public const string KEY_WEATHER_GAODE_APP_KEY = "GaodeAppKey";
        public const string WEATHER_GAODE_API_URL = "https://restapi.amap.com";
        public const string WEATHER_GAODE_REGISTER_URL = "https://lbs.amap.com/dev/";
        #endregion
        #region 心知
        public const string KEY_WEATHER_SENIVERSE_URL = "SeniverseUrl";
        public const string KEY_WEATHER_SENIVERSE_APP_KEY = "SeniverseAppKey";
        public const string WEATHER_SENIVERSE_API_URL = "https://api.seniverse.com";
        public const string WEATHER_SENIVERSE_REGISTER_URL = "https://www.seniverse.com/";
        #endregion
        #region 和风
        public const string KEY_WEATHER_Q_URL = "QUrl";
        public const string KEY_WEATHER_Q_APP_KEY = "QAppKey";
        public const string WEATHER_Q_API_URL = "https://devapi.qweather.com";
        public const string WEATHER_Q_REGISTER_URL = "https://dev.qweather.com/";
        #endregion
        #region OpenWeather
        public const string KEY_WEATHER_OPEN_URL = "OpenUrl";
        public const string KEY_WEATHER_OPEN_APP_KEY = "OpenAppKey";
        public const string WEATHER_OPEN_API_URL = "http://api.openweathermap.org";
        public const string WEATHER_OPEN_REGISTER_URL = "https://openweathermap.org/";
        #endregion
        #region AccuWeather
        public const string KEY_WEATHER_ACCU_URL = "AccuUrl";
        public const string KEY_WEATHER_ACCU_APP_KEY = "AccuAppKey";
        public const string WEATHER_ACCU_API_URL = "http://dataservice.accuweather.com";
        public const string WEATHER_ACCU_REGISTER_URL = "https://developer.accuweather.com/";
        #endregion
        #endregion


        public const string WINDOW_THEME_BLUE = "blue";
        public const string WINDOW_THEME_GREEN = "green";
        public const string WINDOW_THEME_ORANGE = "orange";
        public const string WINDOW_THEME_RED = "red";
        public const string WINDOW_THEME_GRAY = "gray";
        public const string WINDOW_THEME_PURPLE = "purple";
        public const string WINDOW_THEME_BLACK = "black";
        public const string WINDOW_THEME_CUSTOM = "custom";
    }
}
