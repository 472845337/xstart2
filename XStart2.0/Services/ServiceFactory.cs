using XStart2._0.Bean;

namespace XStart2._0.Services {
    public class ServiceFactory {
        private static TableService<Type> typeService;
        private static TableService<Column> columnService;
        private static TableService<Project> projectService;
        private static TableService<Admin> adminService;
        private static TableService<CustomTheme> customThemeService;

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

        public static TableService<Admin> GetAdminService() {
            if (null == adminService) {
                adminService = AdminService.Instance;
            }
            return adminService;
        }

        public static TableService<CustomTheme> GetCustomThemeService() {
            if (null == customThemeService) {
                customThemeService = CustomThemeService.Instance;
            }
            return customThemeService;
        }

        public static S GetService<T, S>() where T : TableData where S : TableService<T> {
            if (typeof(T) == typeof(Type)) {
                return GetTypeService() as S;
            } else if (typeof(T) == typeof(Column)) {
                return GetColumnService() as S;
            } else if (typeof(T) == typeof(Project)) {
                return GetProjectService() as S;
            } else if (typeof(T) == typeof(Admin)) {
                return GetAdminService() as S;
            } else {
                return null;
            }
        }
    }
}
