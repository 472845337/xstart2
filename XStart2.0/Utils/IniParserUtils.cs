using IniParser.Model;
using System;
using System.Collections.Generic;
using System.Text;
using XStart2._0.Config;

namespace XStart2._0.Utils {
    class IniParserUtils {
        private static IniParser.FileIniDataParser iniParser = new IniParser.FileIniDataParser();
        private static readonly Dictionary<string, IniData> iniDataDic = new Dictionary<string, IniData>();

        public static IniData GetIniData(string filePath) {
            if (iniDataDic.TryGetValue(filePath, out IniData _iniData)) {

            } else {
                _iniData = iniParser.ReadFile(Configs.AppStartPath + filePath, Encoding.UTF8);
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
            iniParser.WriteFile(Configs.AppStartPath + filePath, iniDataDic[filePath], Encoding.UTF8);
        }

        /// <summary>
        /// Config配置放入IniData中
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="section">INI的section头</param>
        /// <param name="key">INI的关键字</param>
        /// <param name="from">原值</param>
        /// <param name="to">新值</param>
        public static void ConfigIniData<T>(IniData iniData, string section, string key, ref T from, T to) {
            bool isChange = false;
            if (from is bool fromBool && to is bool toBool) {
                if (fromBool != toBool) {
                    isChange = true;
                }
            } else if (from is string fromStr && to is string toStr) {
                if (!fromStr.Equals(toStr)) {
                    isChange = true;
                }
            } else if (from is int fromInt && to is int toInt) {
                if (fromInt != toInt) {
                    isChange = true;
                }
            } else if (from is uint fromUint && to is uint toUint) {
                if (fromUint != toUint) {
                    isChange = true;
                }
            } else if (from is double fromDouble && to is double toDouble) {
                if (fromDouble != toDouble) {
                    isChange = true;
                }
            } else if (null == from && null != to) {
                isChange = true;
            } else if (null != from && null == to) {
                isChange = true;
            }
            if (isChange) {
                iniData[section][key] = Convert.ToString(to);
                from = to;
            }
        }
    }
}
