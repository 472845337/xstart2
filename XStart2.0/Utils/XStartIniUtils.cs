using System.Collections.Generic;
using XStart.Config;

namespace XStart.Utils {
    internal static class XStartIniUtils {


        //类的构造函数，传递INI文件名  
        public static long IniWriteValue(string iniPath, string section, string key, string value) {
            // 如果文件不存在，创建文件
            string realPath = Configs.AppStartPath + iniPath;
            return IniUtils.IniWriteValue(realPath, section, key, value);
        }

        //读取section中某个值  
        public static string IniReadValue(string iniPath, string section, string key) {
            return IniUtils.IniReadValue(Configs.AppStartPath + iniPath, section, key);
        }
        // 封装的方法中，最有价值的是获取所有Sections和所有的Keys，网上关于这个的代码大部分是错误的，这里给出一个正确的方法：  
        /// 返回该配置文件中所有Section名称的集合  
        public static List<string> ReadSections(string iniPath) {
            return IniUtils.ReadSections(Configs.AppStartPath + iniPath);
        }

        // 获取节点的所有KEY值  

        public static List<string> ReadKeys(string iniPath, string sectionName) {
            return IniUtils.ReadKeys(Configs.AppStartPath + iniPath, sectionName);
        }

        ///   <summary> 
        ///   删除这个项现有的字串。 
        ///   </summary> 
        ///   <param name="iniPath"></param>
        ///   <param name="sectionName"></param>
        ///   <param name="keyName"></param> 
        public static void DeleteKey(string iniPath, string sectionName, string keyName) {
            IniUtils.DeleteKey(Configs.AppStartPath + iniPath, sectionName, keyName);
        }

        ///   <summary> 
        ///   删除这个小节的所有设置项。 
        ///   </summary> 
        ///   <param name="iniPath"> 要删除的小节名。这个字串不区分大小写。 </param>
        ///   <param name="section"></param> 
        public static void EraseSection(string iniPath, string section) {
            IniUtils.EraseSection(Configs.AppStartPath + iniPath, section);
        }
    }
}
