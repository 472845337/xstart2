using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Config;

namespace XStart2._0.Utils {
    class IniParserUtils {
        private static IniParser.FileIniDataParser iniParser = new IniParser.FileIniDataParser();
        private static readonly Dictionary<string, IniData> iniDataDic = new Dictionary<string, IniData>();

        public static IniData GetIniData(string filePath) {
            if (iniDataDic.TryGetValue(filePath, out IniData _iniData)) {

            } else {
                _iniData = iniParser.ReadFile(Configs.AppStartPath + filePath);
                iniDataDic.Add(filePath, _iniData);
            }
            return _iniData;
        }

        public static void SaveIniData(string filePath, string section, string key, string value) {
            IniData iniData = new IniData();
            iniData[section][key] = value;
            SaveIniData(filePath, iniData);
        }

        public static void SaveIniData(string filePath, IniData iniData) {
            if (null == iniData || iniData.Sections.Count == 0) {
                return;
            }
            if (!iniDataDic.TryGetValue(filePath, out _)) {
                GetIniData(filePath);
            }
            iniDataDic[filePath].Merge(iniData);
            iniParser.WriteFile(Configs.AppStartPath + filePath, iniDataDic[filePath]);
        }
    }
}
