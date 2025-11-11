using System;
using System.IO;
using System.Windows.Media.Imaging;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.Services {
    public class XStartService {

        public static ObservableDictionary<string, Bean.Type> TypeDic = new ObservableDictionary<string, Bean.Type>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="path"></param>
        /// <param name="iconPath"></param>
        /// <returns></returns>
        public static BitmapImage GetIconImage(string kind, string path, string iconPath, int size) {
            try {
                BitmapImage image = null;
                if (!string.IsNullOrEmpty(iconPath)) {
                    if (iconPath.StartsWith(Constants.SYSTEM_PROJECT_CHAR)) {
                        // 系统功能图标
                        image = Configs.GetIcon(size, iconPath);
                    } else {
                        // 项目图标
                        if (File.Exists(iconPath) || Directory.Exists(iconPath)) {
                            image = IconUtils.GetBitmapImage(iconPath, size);
                        } else {
                            image = Configs.GetIcon(size, SystemProjectParam.APP);
                        }
                    }
                } else {
                    // IconPath为空，则取项目文件的图标
                    iconPath = path;
                    if ((Project.KIND_FILE.Equals(kind) || Project.KIND_DIRECTORY.Equals(kind)) && (File.Exists(iconPath) || Directory.Exists(iconPath))) {
                        image = IconUtils.GetBitmapImage(iconPath, size);
                    } else if (Project.KIND_URL.Equals(kind)) {
                        image = Configs.GetIcon(size, SystemProjectParam.URL);
                    } else {
                        image = Configs.GetIcon(size, SystemProjectParam.APP);
                    }
                }
                return image;
            } catch (Exception ex) {
                MsgBoxUtils.ShowError(null, ex.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// 应用对应的图标
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static BitmapImage GetIconImage(Project project) {
            return GetIconImage(project.Kind, project.Path, project.IconPath, project.IconSize);
        }

        /// <summary>
        /// 根据路径生成对应的类型
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string KindOfPath(string path) {
            string kind;
            if (path.ToLower().StartsWith("https://") || path.ToLower().StartsWith("http://") || path.ToLower().StartsWith("www.") || path.ToLower().StartsWith("steam://")) {
                kind = Project.KIND_URL;
            } else if (path.StartsWith(Constants.SYSTEM_PROJECT_CHAR)) {
                kind = Project.KIND_SYSTEM;
            } else {
                if (File.Exists(path)) {
                    kind = Project.KIND_FILE;
                } else {
                    kind = Project.KIND_DIRECTORY;
                }
            }
            return kind;
        }

        public static void AddNewData<T>(T t) where T : TableData {
            t.Section = Guid.NewGuid().ToString();
            int sort = 0;
            // 取最后一个的sort+1
            if (t is Bean.Type _type) {
                if (TypeDic.Count > 0) {
                    sort = (int)TypeDic[TypeDic.Count - 1].Sort + 1;
                }
                TypeDic.Add(t.Section, _type);
            } else if (t is Column _column) {
                var columnDic = TypeDic[_column.TypeSection].ColumnDic;
                if (columnDic.Count > 0) {
                    sort = (int)columnDic[columnDic.Count - 1].Sort + 1;

                }
                columnDic.Add(t.Section, _column);
            } else if (t is Project _project) {
                var projectDic = TypeDic[_project.TypeSection].ColumnDic[_project.ColumnSection].ProjectDic;
                if (projectDic.Count > 0) {
                    sort = (int)projectDic[projectDic.Count - 1].Sort + 1;

                }
                projectDic.Add(t.Section, _project);
            }
            t.Sort = sort;
            // 保存应用信息
            ServiceFactory.GetService<T, TableService<T>>().Insert(t);
        }


    }
}
