using System.Linq;
using System.Media;
using System.Windows;
using XStart2._0.ViewModel;
using XStart2._0.Windows;
using Application = System.Windows.Application;
namespace XStart2._0.Utils {
    internal class MsgBoxUtils {

        private static MessageBoxWindow CreateMessageBox(Window owner, string messageBoxText, string caption, MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage, MessageBoxResult defaultResult) {
            MessageBoxWindow messageBox = new MessageBoxWindow();
            if (messageBox.WindowStartupLocation == WindowStartupLocation.CenterOwner) {
                var ownerWindow = owner ?? Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                messageBox.Owner = ownerWindow;
                messageBox.Topmost = ownerWindow is null;
            }
            MessageBoxViewModel messageBoxHelper = new MessageBoxViewModel(messageBoxButton, messageBoxImage, () => { messageBox.Close(); }) {
                Message = messageBoxText,
                Caption = caption ?? "系统消息",
                MessageBoxResult = defaultResult
            };
            messageBox.DataContext = messageBoxHelper;
            return messageBox;
        }

        public static MessageBoxResult ShowInfo(string messageBoxText, string caption = null) {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        public static MessageBoxResult ShowAsk(string messageBoxText, string caption = null) {
            return Show(messageBoxText, caption, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
        }

        public static MessageBoxResult ShowAskWithCancel(string messageBoxText, string caption = null) {
            return Show(messageBoxText, caption, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Cancel);
        }

        public static MessageBoxResult ShowError(string messageBoxText, string caption = null) {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        public static MessageBoxResult ShowWarning(string messageBoxText, string caption = null) {
            return Show(messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.Warning, MessageBoxResult.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption = null, MessageBoxButton messageBoxButton = MessageBoxButton.OK, MessageBoxImage messageBoxImage = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None) {
            MessageBoxWindow messageBox = null;
            MessageBoxViewModel messageBoxHelper = null;
            Application.Current.Dispatcher.Invoke(() => {
                messageBox = CreateMessageBox(null, messageBoxText, caption, messageBoxButton, messageBoxImage, defaultResult);
                messageBoxHelper = messageBox.DataContext as MessageBoxViewModel;
                switch (messageBoxImage) {
                    case MessageBoxImage.Information:
                        SystemSounds.Asterisk.Play();
                        break;
                    case MessageBoxImage.Warning:
                        SystemSounds.Exclamation.Play();
                        break;
                    case MessageBoxImage.Question:
                        SystemSounds.Question.Play();
                        break;
                    case MessageBoxImage.Error:
                        SystemSounds.Hand.Play();
                        break;
                    default:
                        SystemSounds.Asterisk.Play();
                        break;
                }
                messageBox.ShowDialog();
            });
            return messageBoxHelper.MessageBoxResult;
        }
    }
}
