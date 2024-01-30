using System.Windows.Media;

namespace XStart2._0.ViewModels {
    public class ThemeViewModel : BaseViewModel {

        public SolidColorBrush ConfirmButtonBackGroundColor { get; set; }
        public SolidColorBrush ConfirmButtonForeGroundColor { get; set; }
        public SolidColorBrush ConfirmButtonMouseOverBackGroundColor { get; set; }
        public SolidColorBrush ConfirmButtonMouseOverForeGroundColor { get; set; }

        public SolidColorBrush CancelButtonBackGroundColor { get; set; }
        public SolidColorBrush CancelButtonForeGroundColor { get; set; }
        public SolidColorBrush CancelButtonMouseOverBackGroundColor { get; set; }
        public SolidColorBrush CancelButtonMouseOverForeGroundColor { get; set; }

        public SolidColorBrush ToggleButtonCheckedBackGroundColor { get; set; }
        public SolidColorBrush ToggleButtonCheckedForeGroundColor { get; set; }
    }


}
