using Utils;
using XStart2._0.Bean;
using XStart2._0.Config;
using XStart2._0.Const;

namespace XStart2._0.Utils {
    public class XStartRdpUtils : RdpUtils {
        /// <summary>
        /// 创建section的服务rdp文件
        /// 
        /// </summary>
        /// <param name="app">应用</param>
        /// <param name="type">=update(Constants.OPERATE_UPDATE),会强制更新rdp,=create(Constants.OPERATE_CREATE)仅创建，如果已经存在，则不操作</param>
        public static void FreshRdp(Project project, string type) {
            string arguments = project.Arguments;
            string[] argumentArray = arguments.Split(Constants.SPLIT_CHAR);
            string server = argumentArray[0];
            string port = argumentArray[1];
            string username = argumentArray[2];
            string password = argumentArray[3];

            server += string.IsNullOrEmpty(port) ? string.Empty : (":" + port);
            if (Constants.OPERATE_UPDATE.Equals(type)) {
                UpdateProfile(Configs.AppStartPath + @$"rdp\{project.Section}.rdp", server, username, password);
            } else {
                CreateProfile(Configs.AppStartPath + @$"rdp\{project.Section}.rdp", server, username, password);
            }
        }
    }

}
