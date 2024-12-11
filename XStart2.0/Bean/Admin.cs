
namespace XStart2._0.Bean {

    [Table("t_admin")]
    public class Admin : TableData {

        [TableParam("avator", "VARCHAR")]
        public string Avator { get; set; }
        [TableParam("avator_size", "INT")]
        public int? AvatorSize { get; set; }
        [TableParam("gif_speed_ratio", "REAL")]
        public double? GifSpeedRatio { get; set; }
    }
}
