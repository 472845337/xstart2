using System.Windows;
using XStart2._0.Windows;

namespace XStart2._0.Commands {
    public class ResumeCommand {
        public static bool? ShowResumeWindow(Window parentWindow) {
            ResumeWindow resumeWindow = new ResumeWindow() { Owner = parentWindow };
            bool? result = resumeWindow.ShowDialog();
            resumeWindow.Close();
            return result;
        }
    }
}
