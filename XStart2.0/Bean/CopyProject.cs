
using System;

namespace XStart2._0.Bean {

    [Serializable]
    public class CopyProject {

        public string Section { get; set; }
        // 名称
        public string Name { get; set; }
        // 排序
        public int? Sort { get; set; }
        // 图标（symbol或exe 多图标的下标）
        public int? IconIndex { get; set; }
        // 加密
        public string Password { get; set; }
        // 归属类-对应Type中的section
        public string TypeSection { get; set; }// 归属类别
        // 栏目-对应Column中的section
        public string ColumnSection { get; set; }// 归属栏目
        public string Kind {get;set;}

        public string Path { get; set; }// 应用路径或链接url
        public string IconPath { get; set; }// 图标
        public string FontColor { get; set; }// 字体颜色
        public string Arguments { get; set; }// 参数
        public string RunStartPath { get; set; }// 起始
        public string HotKey { get; set; }// 热键
        public string Remark { get; set; }// 备注

        // 随应用启动
        public bool? AutoRun { get; set; }
        public string Operate { get; set; }
    }
}
