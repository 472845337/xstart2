using XStart2._0.Windows;

namespace XStart2._0.Commands {
    public class ResumeCommand {
        public static bool? ShowResumeWindow() {
            ResumeWindow resumeWindow = new ResumeWindow();
            bool? result = resumeWindow.ShowDialog();
            resumeWindow.Close();
            return result;
        }
    }
}
