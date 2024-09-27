
using XStart2._0.Bean;

namespace XStart2._0.Services {
    public class CustomThemeService : TableService<CustomTheme> {
        /// <summary>
        /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
        /// </summary>
        static CustomThemeService() { }

        private CustomThemeService() { }

        public static CustomThemeService Instance { get; } = new CustomThemeService();
    }
}
