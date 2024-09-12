using PropertyChanged;

namespace XStart2._0.Bean {

    [AddINotifyPropertyChangedInterface]
    internal class GradientColor {
        public GradientColor(string color, float point) {
            Color = color;
            Point = point;
        }
        
        public string Color { get; set; }
        public float Point { get; set; }
    }
}
