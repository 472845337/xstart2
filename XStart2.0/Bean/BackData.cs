using System.Collections.Generic;

namespace XStart.Bean {
    /// <summary>
    /// 备份用数据
    /// </summary>
    public class BackData {

        public List<BackType> Types { get; set; }

        public class BackType {
            // section
            public string Section { get; set; }
            // 名称
            public string Name { get; set; }
            // 排序
            public int? Sort { get; set; }
            // 图标（symbol或exe 多图标的下标）
            public int? IconIndex { get; set; }
            // 密码
            public string Password { get; set; }

            public List<BackColumn> Columns { get; set; }
        }
        public class BackColumn {
            // section
            public string Section { get; set; }
            // 名称
            public string Name { get; set; }
            // 排序
            public int? Sort { get; set; }
            // 图标（symbol或exe 多图标的下标）
            public int? IconIndex { get; set; }
            // 所属类别
            public string TypeSection { get; set; }
            // 口令
            public string Password { get; set; }

            public List<BackProject> Projects { get; set; }
        }
        public class BackProject {
            // section
            public string Section { get; set; }
            // 名称
            public string Name { get; set; }
            // 排序
            public int? Sort { get; set; }
            // 图标（symbol或exe 多图标的下标）
            public int? IconIndex { get; set; }
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
