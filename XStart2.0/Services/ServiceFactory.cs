using XStart2._0.Bean;

namespace XStart2._0.Services {
    public class ServiceFactory {
        private static TableService<Type> typeService;
        private static TableService<Column> columnService;
        private static TableService<Project> projectService;

        public static TableService<Type> GetTypeService() {
            if (null == typeService) {
                typeService = TypeService.Instance;
            }
            return typeService;
        }

        public static TableService<Column> GetColumnService() {
            if (null == columnService) {
                columnService = ColumnService.Instance;
            }
            return columnService;
        }

        public static TableService<Project> GetProjectService() {
            if (null == projectService) {
                projectService = ProjectService.Instance;
            }
            return projectService;
        }

        public static S GetService<T, S>() where T : TableData where S:TableService<T> {
            if(typeof(T) ==  typeof(Bean.Type)) {
                return GetTypeService() as S;
            }else if (typeof(T) == typeof(Bean.Column)) {
                return GetColumnService() as S;
            }else if (typeof(T) == typeof(Bean.Project)) {
                return GetProjectService() as S;
            } else {
                return null;
            }
        }
    }
}
