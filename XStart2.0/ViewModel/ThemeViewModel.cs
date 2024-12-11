using System.Collections.ObjectModel;
using XStart2._0.Bean;

namespace XStart2._0.ViewModel {
    public class ThemeViewModel : BaseViewModel {

        public string ConfirmButtonBackGround { get; set; }
        public string ConfirmButtonForeGround { get; set; }
        public string ConfirmButtonMouseOverBackGround { get; set; }
        public string ConfirmButtonMouseOverForeGround { get; set; }

        public string CancelButtonBackGround { get; set; }
        public string CancelButtonForeGround { get; set; }
        public string CancelButtonMouseOverBackGround { get; set; }
        public string CancelButtonMouseOverForeGround { get; set; }

        public string ToggleButtonCheckedBackGround { get; set; }
        public string ToggleButtonCheckedForeGround { get; set; }

        public ObservableCollection<CustomTheme> CustomThemes { get; set; }

        public CustomTheme LoadCustomTheme { get; set; }
        public string LoadCustomName { get; set; }
    }


}
