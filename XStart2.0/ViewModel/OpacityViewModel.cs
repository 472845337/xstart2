using PropertyChanged;

namespace XStart2._0.ViewModel {
    internal class OpacityViewModel : BaseViewModel{
        // 滚动条值范围是0-10，所以不透明度要除10
        [OnChangedMethod(nameof(SetShowValue))]
        public double Opacity { get; set; }
        // 显示的值
        public double ShowOpacity { get; set; }

        private void SetShowValue() {
            // 滚动条值是0-10
            ShowOpacity = Opacity / 10.0D;
        }
    }
}
