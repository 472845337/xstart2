namespace XStart2._0.Services {
    public class ServiceFactory {
        public static TypeService typeService;
        public static ColumnService columnService;
        public static ProjectService projectService;

        public static TypeService GetTypeService() {
            if (null == typeService) {
                typeService = TypeService.Instance;
            }
            return typeService;
        }

        public static ColumnService GetColumnService() {
            if (null == columnService) {
                columnService = ColumnService.Instance;
            }
            return columnService;
        }

        public static ProjectService GetProjectService() {
            if (null == projectService) {
                projectService = ProjectService.Instance;
            }
            return projectService;
        }
    }
}
