using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using XStart2._0.Commands;

namespace XStart2._0.ViewModel {
    public class MessageBoxViewModel : BaseViewModel {

        #region 私有变量
        private bool _showOk = false;

        private bool _showCancel = false;

        private bool _showYes = false;

        private bool _showNo = false;

        private IntPtr _icon = IntPtr.Zero;

        private Action _onCloseAction = null;

        private MessageBoxButton _messageBoxButton = MessageBoxButton.OK;

        private MessageBoxImage _messageBoxImage = MessageBoxImage.Information;
        #endregion

        #region 属性
        public string Caption { get; set; }

        public string Message { get; set; }

        public BitmapSource ImageSource { get; set; }
        #endregion

        #region 公共变量
        public MessageBoxResult MessageBoxResult = MessageBoxResult.Cancel;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="messageBoxButton">指定显示按钮</param>
        /// <param name="messageBoxImage">指定显示图标</param>
        /// <param name="closeAction">点击按钮时触发的方法</param>
        public MessageBoxViewModel(MessageBoxButton messageBoxButton, MessageBoxImage messageBoxImage, Action onClose) {
            _messageBoxButton = messageBoxButton;
            _messageBoxImage = messageBoxImage;
            _onCloseAction = onClose;
            SetDisplayButton();
            SetDisplayIcon();

            HitOK = new RelayCommand(OnHitOK, () => { return _showOk; });
            HitCancel = new RelayCommand(OnHitCancel, () => { return _showCancel; });
            HitYes = new RelayCommand(OnHitYes, () => { return _showYes; });
            HitNo = new RelayCommand(OnHitNo, () => { return _showNo; });
        }
        public ICommand HitOK { get; private set; }
        private void OnHitOK(object obj) {
            MessageBoxResult = MessageBoxResult.OK;
            _onCloseAction?.Invoke();
        }
        public ICommand HitCancel { get; private set; }
        private void OnHitCancel(object obj) {
            MessageBoxResult = MessageBoxResult.Cancel;
            _onCloseAction?.Invoke();
        }
        public ICommand HitYes { get; private set; }
        private void OnHitYes(object obj) {
            MessageBoxResult = MessageBoxResult.Yes;
            _onCloseAction?.Invoke();
        }
        public ICommand HitNo { get; private set; }
        private void OnHitNo(object obj) {
            MessageBoxResult = MessageBoxResult.No;
            _onCloseAction?.Invoke();
        }
        private void SetDisplayButton() {
            switch (_messageBoxButton) {
                case MessageBoxButton.OK:
                    _showOk = true;
                    break;
                case MessageBoxButton.OKCancel:
                    _showOk = true;
                    _showCancel = true;
                    break;
                case MessageBoxButton.YesNo:
                    _showYes = true;
                    _showNo = true;
                    break;
                case MessageBoxButton.YesNoCancel:
                    _showYes = true;
                    _showNo = true;
                    _showCancel = true;
                    break;
            }
        }
        private void SetDisplayIcon() {
            switch (_messageBoxImage) {
                case MessageBoxImage.Information:
                    _icon = SystemIcons.Information.Handle;
                    break;
                case MessageBoxImage.Warning:
                    _icon = SystemIcons.Warning.Handle;
                    break;
                case MessageBoxImage.Question:
                    _icon = SystemIcons.Question.Handle;
                    break;
                case MessageBoxImage.Error:
                    _icon = SystemIcons.Error.Handle;
                    break;
            }

            if (_icon.ToInt32() > 0) {
                ImageSource = GetResource();
            }
        }
        private BitmapSource GetResource() {
            BitmapSource source = Imaging.CreateBitmapSourceFromHIcon(_icon, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            source.Freeze();
            return source;
        }
    }
}
