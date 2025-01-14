using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using XStart2._0.Bean.Holiday;
using Utils;
using XStart2._0.Utils;
using XStart2._0.ViewModel;

namespace XStart2._0.Windows {
    /// <summary>
    /// CalendarWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CalendarWindow : Window {
        public readonly CalendarViewModel vm = new CalendarViewModel();

        private readonly System.Windows.Threading.DispatcherTimer currentMinuteTimer = new System.Windows.Threading.DispatcherTimer();
        public CalendarWindow() {
            InitializeComponent();
            Loaded += Window_Loaded;
            Closing += Window_Closing;
        }

        private void Window_Loaded(object sender, EventArgs e) {
            //SetLunarDateInfo(DateTime.UtcNow);
            CurrentMinuteTime_Tick(sender, e);
            DataContext = vm;
            // Tick 分钟间隔时发生。
            currentMinuteTimer.Tick += new EventHandler(CurrentMinuteTime_Tick);
            // Interval 获取或设置计时器刻度之间的时间段
            currentMinuteTimer.Interval = TimeSpan.FromHours(1);
            currentMinuteTimer.Start();
            // 赋值句柄
            Config.Configs.CalendarHandler = new WindowInteropHelper(this).Handle;
        }

        private void Window_Closing(object sender, EventArgs e) {
            Config.Configs.CalendarHandler = IntPtr.Zero;
            DataContext = null;
        }



        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e) {
            Calendar cal = sender as Calendar;
            if (cal.SelectedDate.HasValue) {
                DateTime dateTime = cal.SelectedDate.Value;
                SetLunarDateInfo(dateTime);
            }
        }

        private void SetLunarDateInfo(DateTime dateTime) {
            vm.Holiday = "";
            vm.SelectedDate = dateTime.ToString("yyyy-MM-dd");
            LunarCalendar lunarCalendar = new LunarCalendar(dateTime);
            vm.ChineseDate = $"{lunarCalendar.GetYear()}年 {lunarCalendar.GetMonth()}月 {lunarCalendar.GetDay()}";
            vm.EraDate = $"{lunarCalendar.GetEraYear()}年 {lunarCalendar.GetEraMonth()}月 {lunarCalendar.GetEraDay()}日";
            vm.Zodiac = lunarCalendar.ChineseZodiac;
            vm.WeekDay = lunarCalendar.ChineseWeek;
            vm.SolarTerm = string.IsNullOrEmpty(lunarCalendar.SolarTerm) ? lunarCalendar.SolarTermPrev : lunarCalendar.SolarTerm;
            Task task = new Task(() => {
                IsHolidayResponse response = GetIsHoliday(dateTime.ToString("yyyy-MM-dd"));
                if (null != response && HcaResponse.CODE_SUCCESS.Equals(response.Code) && null != response.IsHoliday) {
                    vm.Holiday = $"{((bool)response.IsHoliday ? "休" : "班")} {response.HolidayName}";
                }
            });
            task.Start();
        }

        private void CurrentMinuteTime_Tick(object sender, EventArgs e) {
            // 下一分钟
            TimeSpan timeToGo = TimeUtils.GetTimeToNext(TimeEnum.MINUTE);
            currentMinuteTimer.Interval = timeToGo;
            DateTime now = DateTime.UtcNow;
            LunarCalendar lunarCalendar = new LunarCalendar(now);
            vm.CurChineseDate = $"{lunarCalendar.GetYear()}年 {lunarCalendar.GetMonth()}月 {lunarCalendar.GetDay()}";
            vm.CurEraDate = $"{lunarCalendar.GetEraYear()}年 {lunarCalendar.GetEraMonth()}月 {lunarCalendar.GetEraDay()}日";
            vm.CurZodiac = lunarCalendar.ChineseZodiac;
            vm.CurWeekDay = lunarCalendar.ChineseWeek;
            vm.CurSolarTerm = string.IsNullOrEmpty(lunarCalendar.SolarTerm) ? lunarCalendar.SolarTermPrev : lunarCalendar.SolarTerm;
            Task task = new Task(() => {
                IsHolidayResponse response = GetIsHoliday(now.ToString("yyyy-MM-dd"));
                if (null != response && HcaResponse.CODE_SUCCESS.Equals(response.Code) && null != response.IsHoliday) {
                    vm.CurHoliday = $"{((bool)response.IsHoliday ? "休" : "班")} {response.HolidayName}";
                }
            });
            task.Start();
        }

        private IsHolidayResponse GetIsHoliday(string date) {
            IsHolidayRequest request = new IsHolidayRequest {
                Loginname = "xstart",
                Version = "1.0.0",
                Token = "nvQcZJuwHpvfXjj86ZqbCUr1FhsmN0tw1HvGswjjsDmtLJmRWWJdWWclLheT7LcZTz5xeUzcF0oJyygwIV1nmZCEpX6a4QezppT8erUn9h6Myhocn9huDI2rMuhfeGoEEY6abOd0S4YzxXCG8yqaNikGxzA7ste9lkomad9yoJvjuj5SYl36SmwBPKmlDs7AIQwt5TA2"
            };
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            request.Timestamp = Convert.ToInt64(ts.TotalMilliseconds);
            request.Sign = GetSign(request);
            request.Date = date;
            IsHolidayResponse response = null;
            try {
                string responseJson = HttpUtils.PostRequest(HcaRequest.ApiUrl + IsHolidayRequest.ApiPath, JsonConvert.SerializeObject(request), HttpUtils.ContentTypeJson, 20000);
                response = JsonConvert.DeserializeObject<IsHolidayResponse>(responseJson);
            } catch (Exception e) {
                Console.WriteLine(e.Message);
            }

            return response;
        }

        /// <summary>
        /// 生成签名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        private string GetSign<T>(T apiRequest) where T : HcaRequest {
            string checkContent = $"loginname={apiRequest.Loginname}&timestamp={apiRequest.Timestamp}&token={apiRequest.Token}&action={apiRequest.GetApiPath()}";
            byte[] s = MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(checkContent));
            string sign = "";
            for (int i = 0; i < s.Length; i++) {
                // 加密结果"x2"结果为32位,"x3"结果为48位,"x4"结果为64位
                sign += s[i].ToString("x2");
            }
            return sign;
        }
    }
}
