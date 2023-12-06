using XStart.Bean;

namespace XStart.Services {
    public class ProjectService : TableService<Project> {
        /// <summary>
        /// 显式的静态构造函数用来告诉C#编译器在其内容实例化之前不要标记其类型
        /// </summary>
        static ProjectService() { }

        private ProjectService() { }

        public static ProjectService Instance { get; } = new ProjectService();

        internal void UpdateSort(string appSection, int sort) {
            Project project = new Project { Section = appSection, Sort = sort };
            Instance.Update(project);
        }
    }
}
