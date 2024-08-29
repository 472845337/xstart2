
namespace XStart2._0.Bean {

    [Table("t_admin")]
    public class Admin : TableData {

        [TableParam("avator", "VARCHAR")]
        public string Avator { get; set; }
    }
}
