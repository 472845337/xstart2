
namespace XStart.Services {
    public class TypeService : TableService<Bean.Type> {
        /// <summary>
        /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
        /// </summary>
        static TypeService() { }

        private TypeService() { }

        public static TypeService Instance { get; } = new TypeService();

        public int UpdateSort(string section, int sort) {
            Bean.Type updateModel = new Bean.Type { Section = section, Sort = sort };
            return Instance.Update(updateModel);
        }
    }
}
