using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XStart2._0.Windows;

namespace XStart2._0.Commands {
    public class ResumeCommand {
        public static void ShowResumeWindow() {
            ResumeWindow resumeWindow = new ResumeWindow();
            resumeWindow.ShowDialog();
            resumeWindow.Close();
        }
    }
}
