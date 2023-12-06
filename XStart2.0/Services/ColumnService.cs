
using XStart.Bean;

namespace XStart.Services {
    public class ColumnService : TableService<Column> {
        /// <summary>
        /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
        /// </summary>
        static ColumnService() { }

        private ColumnService() { }

        public static ColumnService Instance { get; } = new ColumnService();

        public int UpdateSort(string section, int sort) {
            Column updateModel = new Column { Section = section, Sort = sort };
            return Instance.Update(updateModel);
        }
    }
}
