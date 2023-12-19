using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using XStart.Bean;
using XStart.Config;
using XStart.Const;
using XStart.Utils;
using XStart2._0.Bean;

namespace XStart.Services {
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
        /// 应用对应的图标
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public static System.Drawing.Bitmap GetIconImage(Project project) {
            try {
                // 根据类型选择图标
                string kind = project.Kind;
                System.Drawing.Bitmap image = null;
                if (!string.IsNullOrEmpty(project.IconPath)) {
                    if (project.IconPath.StartsWith("#")) {
                        // 系统功能图标
                        image = Configs.iconDic[project.IconPath];
                    } else {
                        // 项目图标
                        if (File.Exists(project.IconPath) || Directory.Exists(project.IconPath)) {
                            if (project.IconPath.ToLower().EndsWith(".exe") || project.IconPath.ToLower().EndsWith(".ico")) {
                                image = IconUtils.GetIcon(project.IconPath, true).ToBitmap();
                            } else {
                                image = IconUtils.GetIcon(project.IconPath, true).ToBitmap();
                            }
                        } else {
                            image = Configs.ICON_APP;
                        }
                    }
                } else {
                    // IconPath为空，则取项目文件的图标
                    string iconPath = project.Path;
                    if (Project.KIND_FILE.Equals(kind) || Project.KIND_DIRECTORY.Equals(kind)) {
                        // 文件类型，判断是否exe或ico
                        if (iconPath.ToLower().EndsWith(".exe") || iconPath.ToLower().EndsWith(".ico")) {
                            image = IconUtils.GetIcon(iconPath, true).ToBitmap();
                        } else {
                            image = IconUtils.GetIcon(iconPath, true).ToBitmap();
                        }
                    } else if (Project.KIND_URL.Equals(kind)) {
                        image = IconUtils.GetIconImage(Configs.AppStartPath + Constants.ICON_URL)[0];
                    }
                }
                return image;
            } catch (Exception ex) {
                MessageBox.Show(ex.StackTrace);
                return null;
            }

        }

        /// <summary>
        /// 将拿到的图标Bitmap转换成可以绑定的BitmapImage
        /// </summary>
        /// <param name="bitmap"></param>
        /// <returns></returns>
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap bitmap) {
            BitmapImage bitmapImage = new BitmapImage();
            using (MemoryStream ms = new MemoryStream()) {
                bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
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
