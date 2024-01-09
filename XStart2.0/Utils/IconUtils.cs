using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using XStart2._0.Const;
using XStart2._0.Interfaces;

namespace XStart2._0.Utils {
    class IconUtils {
        /// <summary>
        /// 获取exe文件中的图标
        /// </summary>
        /// <param name="exePath"></param>
        /// <returns></returns>
        public static List<Bitmap> GetIconImage(string exePath) {
            List<Bitmap> imageList = new List<Bitmap>();

            //第一步：获取程序中的图标数
            int iconCount = DllUtils.ExtractIconEx(exePath, -1, null, null, 0);

            //第二步：创建存放大/小图标的空间
            var largeIcons = new IntPtr[iconCount];
            var smallIcons = new IntPtr[iconCount];

            //第三步：抽取所有的大小图标保存到largeIcons和smallIcons中
            DllUtils.ExtractIconEx(exePath, 0, largeIcons, smallIcons, iconCount);

            //第四步：显示抽取的图标(推荐使用imageList和listview搭配显示）
            for (int i = 0; i < iconCount; i++) {
                IntPtr intPtr = largeIcons[i];
                if (intPtr != IntPtr.Zero) {
                    var icon = Icon.FromHandle(largeIcons[i]);
                    imageList.Add(icon.ToBitmap());
                    icon.Dispose();
                    // 销毁图标
                    DllUtils.DestroyIcon(largeIcons[i]);
                    // 向控件发送关闭指令
                    DllUtils.SendMessage(largeIcons[i], WinApi.WM_CLOSE, 0, 0);
                }
            }
            return imageList;
        }

        /// <summary>
        /// 获取文件类型或目录的关联图标
        /// </summary>
        /// <remarks>
        /// 目前获取到的图标大小有 32*32,48*48,72*72,128*128,256*256
        /// 其中 
        /// 32*32->SHGFI_LARGEICON
        /// 48*48->SHGFI_OPENICON
        /// 72*72/128*128/256*256->SHGFI_SHELLICONSIZE
        /// </remarks>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="type">类型 file/dir</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Bitmap GetIcon(string fileName, double iconSize) {
            DllUtils.SHFILEINFO shfi = new DllUtils.SHFILEINFO();
            uint fileInfo = (uint)WinApi.FileInfoFlags.SHGFI_ICON;
            bool isLarge = false;
            if (Constants.ICON_SIZE_32 == iconSize) {
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_LARGEICON;
            } else if (Constants.ICON_SIZE_48 == iconSize) {
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_OPENICON;
            } else {
                isLarge = true;
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_SHELLICONSIZE;
            }
            if (File.Exists(fileName)) {
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_USEFILEATTRIBUTES;
            }
            IntPtr _IconIntPtr = DllUtils.SHGetFileInfo(fileName, 0, ref shfi, (uint)Marshal.SizeOf(shfi), fileInfo);
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon icon;
            if (isLarge) {
                int iconIndex = shfi.iIcon;

                // Get the System IImageList object from the Shell:
                Guid iidImageList = new Guid("46EB5926-582E-4017-9FDF-E8998DAA0950");

                IImageList iml;
                int size = 0x4;
                DllUtils.SHGetImageList(size, ref iidImageList, out iml); // writes iml

                IntPtr hIcon = IntPtr.Zero;
                int ILD_TRANSPARENT = 1;
                iml.GetIcon(iconIndex, ILD_TRANSPARENT, ref hIcon);
                icon = Icon.FromHandle(hIcon).Clone() as Icon;
            } else {
                icon = Icon.FromHandle(shfi.hIcon).Clone() as Icon;
            }
            Bitmap bs = icon.ToBitmap();
            icon.Dispose();
            DllUtils.DestroyIcon(shfi.hIcon); //释放资源
            DllUtils.SendMessage(shfi.hIcon, WinApi.WM_CLOSE, 0, 0);
            return bs;
        }

        public static BitmapImage GetBitmapImage(string fileName, double iconSize) {
            return ImageUtils.BitmapToBitmapImage(GetIcon(fileName, iconSize));
        }
        /// <summary>
        /// 绘制方块图
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static System.Drawing.Image DrawImage(Color color, int width, int height) {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = new Rectangle(0, 0, width, height);//定义矩形,参数为起点横纵坐标以及其长和宽
            SolidBrush b1 = new SolidBrush(color);//定义单色画刷         
            g.FillRectangle(b1, rect);//填充这个矩形
            b1.Dispose();
            g.Dispose();
            return bmp;
        }
    }
}
