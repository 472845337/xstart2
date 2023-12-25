﻿
using System;
using XStart.Const;

namespace XStart.Bean {

    [Serializable]
    [Table("t_app")]
    public class Project : TableData {

        public const string KEY_TYPE_SECTION = "TypeSection";
        public const string KEY_COLUMN_SECTION = "ColumnSection";
        public const string KEY_KIND = "Kind";// 类型
        public const string KEY_PATH = "Path";// 路径
        public const string KEY_ICON_PATH = "IconPath";// 图标
        public const string KEY_FONT_COLOR = "FontColor";// 字体颜色
        public const string KEY_ARGUMENTS = "Arguments";// 参数
        public const string KEY_RUN_START_PATH = "RunStartPath";
        public const string KEY_HOT_KEY = "HotKey";// 热键
        public const string KEY_REMARK = "Remark";// 备注
        public const string KEY_AUTO_RUN = "AutoRun";// 自启动

        public const string KIND_FILE = "file";// 文件
        public const string KIND_DIRECTORY = "directory";// 目录
        public const string KIND_URL = "url";// 链接
        public const string KIND_SYSTEM = "system";// 系统
        public const string KIND_REMOTE = "remote";// 远程

        // 归属类-对应Type中的section
        [TableParam("type_section", "VARCHAR")]
        public string TypeSection { get; set; }// 归属类别
        // 栏目-对应Column中的section
        [TableParam("column_section", "VARCHAR")]
        public string ColumnSection { get; set; }// 归属栏目
        [TableParam("kind", "VARCHAR")]
        public string Kind { get; set; }// 种类
        [TableParam("path", "VARCHAR")]
        public string Path { get; set; }// 应用路径或链接url
        [TableParam("icon_path", "VARCHAR")]
        public string IconPath { get; set; }// 图标
        [TableParam("font_color", "VARCHAR")]
        public string FontColor { get; set; }// 字体颜色
        [TableParam("arguments", "VARCHAR")]
        public string Arguments { get; set; }// 参数
        [TableParam("run_start_path", "VARCHAR")]
        public string RunStartPath { get; set; }// 起始
        [TableParam("hot_key", "VARCHAR")]
        public string HotKey { get; set; }// 热键
        [TableParam("remark", "VARCHAR")]
        public string Remark { get; set; }// 备注

        // 随应用启动
        private bool? autoRun;
        [TableParam("auto_run", "BIT")]
        public bool? AutoRun { get => autoRun; set { autoRun = value; OnPropertyChanged("AutoRun"); } }
        // 系统应用不可随应用启动
        public bool CanAutoRun { get; set; } = true;
        public string ToolTipContent {
            get {
                if (SystemAppParam.MSTSC.Equals(Path)) {
                    string[] argumentArray = Arguments.Split(Constants.SPLIT_CHAR);
                    return $"远程->{argumentArray[0]}:{argumentArray[1]}";
                } else {
                    return Path;
                }
            }
        }

        private System.Windows.Media.Imaging.BitmapImage icon;
        public System.Windows.Media.Imaging.BitmapImage Icon { get => icon; set { icon = value;OnPropertyChanged("Icon"); } }
    }
}
