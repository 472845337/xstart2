using System.Windows;
using Utils;
using XStart2._0.Const;

namespace XStart2._0.Helper {
    public class HideWindowHelper {
        public static int GetAnchorStyle(Window mainWindow) {
            int anchorStyle;
            if (mainWindow.Top <= SystemParameters.VirtualScreenTop) {
                anchorStyle = Constants.ANCHOR_STYLE_TOP;
            } else if (mainWindow.Left <= SystemParameters.VirtualScreenLeft) {
                anchorStyle = Constants.ANCHOR_STYLE_LEFT;
            } else if (mainWindow.Left >= SystemParameters.VirtualScreenWidth + SystemParameters.VirtualScreenLeft - mainWindow.Width) {
                anchorStyle = Constants.ANCHOR_STYLE_RIGHT;
            } else if (mainWindow.Top >= SystemParameters.VirtualScreenHeight + SystemParameters.VirtualScreenTop - mainWindow.Height) {
                anchorStyle = Constants.ANCHOR_STYLE_BOTTOM;
            } else {
                anchorStyle = Constants.ANCHOR_STYLE_NONE;
            }
            return anchorStyle;
        }
        readonly static int step = 1;
        public static void Animate2Location(Window window, Point destination) {
            double curLeft = NumberUtils.Accuracy(window.Left, 2);
            double curTop = NumberUtils.Accuracy(window.Top, 2);
            double indexX = NumberUtils.Accuracy((destination.X - curLeft) / 200, 2);
            double indexY = NumberUtils.Accuracy((destination.Y - curTop) / 200, 2);
            if (indexX == 0) {
                indexX = destination.X > curLeft ? step : -step;
            }
            if (indexY == 0) {
                indexY = destination.Y > curTop ? step : -step;
            }
            int index = 1;
            bool xStop = false;
            bool yStop = false;
            double aniLocationX = curLeft;
            double aniLocationY = curTop;
            if (indexX == 0) {
                xStop = true;
            }
            if (indexY == 0) {
                yStop = true;
            }
            while (!xStop || !yStop) {
                if (!xStop) {
                    if (indexX < 0) {
                        if (NumberUtils.Accuracy(aniLocationX + indexX, 2) <= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX = NumberUtils.Accuracy(aniLocationX + indexX, 2);
                        }
                    } else {
                        if (NumberUtils.Accuracy(aniLocationX + indexX, 2) >= destination.X) {
                            aniLocationX = destination.X;
                            xStop = true;
                        } else {
                            aniLocationX = NumberUtils.Accuracy(aniLocationX + indexX, 2);
                        }
                    }
                }
                if (!yStop) {
                    if (indexY < 0) {
                        if (NumberUtils.Accuracy(aniLocationY + indexY, 2) <= destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY = NumberUtils.Accuracy(aniLocationY + indexY, 2);
                        }
                    } else {
                        if (NumberUtils.Accuracy(aniLocationY + indexY, 2) >= destination.Y) {
                            aniLocationY = destination.Y;
                            yStop = true;
                        } else {
                            aniLocationY = NumberUtils.Accuracy(aniLocationY, 2) + indexY;
                        }
                    }
                }
                window.Left = aniLocationX;
                window.Top = aniLocationY;
                index++;
            }
        }

    }
}
