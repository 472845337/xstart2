using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;
using XStart2._0.Utils;

namespace XStart2._0.Services {
    public class XStartService {

        public static ObservableDictionary<string, Bean.Type> TypeDic = new ObservableDictionary<string, Bean.Type>();

        public static Sections GetSectionByName(string name) {
            Sections section = new Sections();
            string[] nameArray = name.Split('_');
            if (nameArray.Length == 2) {
                section.TypeSection = nameArray[0];
                section.SuffixName = nameArray[1];
            } else if (nameArray.Length == 3) {
                section.TypeSection = nameArray[0];
                section.ColumnSection = nameArray[1];
                section.SuffixName = nameArray[2];
            } else if (nameArray.Length > 3) {
                section.TypeSection = nameArray[0];
                section.ColumnSection = nameArray[1];
                section.AppSection = nameArray[2];
                section.SuffixName = nameArray[3];
            } else {
                throw new Exception("不可解析的Section名");
            }
            return section;
        }

        /// <summary>
        /// 对快捷启动中的数据进行排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, T>> SortXStartData<T>(Dictionary<string, T> dic) where T : TableData {
            List<KeyValuePair<string, T>> list = new List<KeyValuePair<string, T>>(dic);
            list.Sort(delegate (KeyValuePair<string, T> s1, KeyValuePair<string, T> s2) {
                return (int)s1.Value.Sort - (int)s2.Value.Sort;
            });
            return list;
        }

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
                    if (iconPath.StartsWith("#")) {
                        // 系统功能图标
                        image = Configs.GetIconDicBySize(size)[iconPath];
                    } else {
                        // 项目图标
                        if (File.Exists(iconPath) || Directory.Exists(iconPath)) {
                            image = IconUtils.GetBitmapImage(iconPath, size);
                        } else {
                            image = Configs.GetIconDicBySize(size)[SystemProjectParam.APP];
                        }
                    }
                } else {
                    // IconPath为空，则取项目文件的图标
                    iconPath = path;
                    if (Project.KIND_FILE.Equals(kind) || Project.KIND_DIRECTORY.Equals(kind)) {
                        image = IconUtils.GetBitmapImage(iconPath, size);
                    } else if (Project.KIND_URL.Equals(kind)) {
                        image = Configs.GetIconDicBySize(size)[SystemProjectParam.URL];
                    }
                }
                return image;
            } catch (Exception ex) {
                MessageBox.Show(ex.StackTrace);
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
            if (path.ToLower().StartsWith("https://") || path.ToLower().StartsWith("http://") || path.ToLower().StartsWith("www.")) {
                kind = Project.KIND_URL;
            } else if (path.StartsWith("#")) {
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

        public static void AddNewApp(Project project) {
            project.Section = Guid.NewGuid().ToString();
            // 取最后一个app的sort+1
            var appList = TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic;
            int sort = (null == appList || appList.Count == 0) ? 0 : (int)appList[appList.Count - 1].Sort + 1;
            project.Sort = sort;
            // 保存应用信息
            ProjectService.Instance.Insert(project);
            TypeDic[project.TypeSection].ColumnDic[project.ColumnSection].ProjectDic.Add(project.Section, project);
        }
    }
}
