using System.Collections.Generic;

namespace XStart2._0.Bean {
    /// <summary>
    /// 备份用数据
    /// </summary>
    public class BackData {

        public List<BackType> Types { get; set; }

        public class BackBase {
            public string Section { get; set; }
            public string Name { get; set; }
            public int? Sort { get; set; }
            public int? IconIndex { get; set; }
            public string Password { get; set; }
        }
        public class BackType : BackBase {
            public string FaIcon { get; set; }
            public string FaIconColor { get; set; }
            public string FaIconFontFamily { get; set; }
            public List<BackColumn> Columns { get; set; }
        }
        public class BackColumn : BackBase {

            // 所属类别
            public string TypeSection { get; set; }
            // 图标尺寸
            public int? IconSize { get; set; }
            // 是否横排
            public string Orientation { get; set; }
            // 一行多个
            public bool? OneLineMulti { get; set; }
            // 隐藏标题
            public bool? HideTitle { get; set; }

            public List<BackProject> Projects { get; set; }
        }
        public class BackProject : BackBase {

            public string TypeSection { get; set; }// 归属类别
            public string ColumnSection { get; set; }// 归属栏目
            public string Kind { get; set; }// 种类
            public string Path { get; set; }// 应用路径或链接url
            public string IconPath { get; set; }// 图标
            public string FontColor { get; set; }// 字体颜色
            public string Arguments { get; set; }// 参数
            public string RunStartPath { get; set; }// 起始
            public string HotKey { get; set; }// 热键
            public string Remark { get; set; }// 备注
            public bool? AutoRun { get; set; }// 随应用启动
        }
    }
}
