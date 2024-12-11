namespace XStart2._0.Bean {
    [Table("t_custom_theme")]
    public class CustomTheme : TableData {
        [TableParam("bg", "VARCHAR")]
        public string Bg { get; set; }
        [TableParam("fg", "VARCHAR")]
        public string Fg { get; set; }

        [TableParam("mouse_over_bg", "VARCHAR")]
        public string MouseOverBg { get; set; }
        [TableParam("mouse_over_fg", "VARCHAR")]
        public string MouseOverFg { get; set; }

        [TableParam("cancel_bg", "VARCHAR")]
        public string CancelBg { get; set; }
        [TableParam("cancel_fg", "VARCHAR")]
        public string CancelFg { get; set; }

        [TableParam("cancel_mouse_over_bg", "VARCHAR")]
        public string CancelMouseOverBg { get; set; }
        [TableParam("cancel_mouse_over_fg", "VARCHAR")]
        public string CancelMouseOverFg { get; set; }

        [TableParam("checked_bg", "VARCHAR")]
        public string CheckedBg { get; set; }
        [TableParam("checked_fg", "VARCHAR")]
        public string CheckedFg { get; set; }

    }
}
