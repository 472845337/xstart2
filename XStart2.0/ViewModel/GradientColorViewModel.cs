using System.Collections.ObjectModel;
using XStart2._0.Bean;

namespace XStart2._0.ViewModel {
    class GradientColorViewModel : BaseViewModel {

        public string GradientBackground { get; set; }// 渐变色字符串

        public ObservableCollection<GradientColor> GradientColorList { get; set; }// 渐变色集合
        public double Angle { get; set; }// 旋转角度

        public bool CanAdd { get; set; }
        public string AddTip { get; set; }
        public bool CanDelete { get; set; }
        public string DeleteTip { get; set; }

        public void ChangeGradient() {
            if (null != GradientColorList) {
                if (GradientColorList.Count < 3) {
                    CanDelete = false;
                    DeleteTip = "渐变色最少2种颜色";
                } else {
                    CanDelete = true;
                    DeleteTip = "删除该渐变色块";
                }
                if (GradientColorList.Count >= 10) {
                    CanAdd = false;
                    AddTip = "最多10个渐变色";
                } else {
                    CanAdd = true;
                    AddTip = "添加渐变色块";
                }
                GradientBackground = Utils.GradientColorUtils.GetString(GradientColorList, Angle);
            } else {
                GradientBackground = string.Empty;
                CanDelete = false;
                DeleteTip = "渐变色为空";
                CanAdd = true;
                AddTip = "添加渐变色块";
            }

        }
    }
}
