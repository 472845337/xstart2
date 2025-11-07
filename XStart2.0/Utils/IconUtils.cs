using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Media.Imaging;
using Utils;
using Utils.Interfaces;
using XStart2._0.Const;

namespace XStart2._0.Utils {
    class IconUtils {
        /// <summary>
        /// 获取ico文件中的图标
        /// </summary>
        /// <param name="icoPath"></param>
        /// <returns></returns>
        public static List<Bitmap> GetIconImage(string icoPath, int iconSize) {
            List<Bitmap> imageList = new List<Bitmap>();

            //第一步：获取程序中的图标数
            uint _nIcons = DllUtils.PrivateExtractIcons(icoPath, 0, 0, 0, null, null, 0, 0);

            //第二步：创建存放大/小图标的空间
            IntPtr[] phicon = new IntPtr[_nIcons];
            uint[] piconid = new uint[_nIcons];
            uint nIcons = DllUtils.PrivateExtractIcons(icoPath, 0, iconSize, iconSize, phicon, piconid, _nIcons, 0);

            //第三步：显示抽取的图标(推荐使用imageList和listview搭配显示）
            for (int i = 0; i < nIcons; i++) {
                Icon icon = Icon.FromHandle(phicon[i]);
                Bitmap bitmap = icon.ToBitmap();
                imageList.Add(bitmap);
                icon.Dispose();
                _ = DllUtils.DestroyIcon(phicon[i]);
            }
            return imageList;
        }
        public static BitmapImage GetBitmapImage(string fileName, int iconSize) {
            if (fileName.EndsWith(".ico", true, CultureInfo.CurrentCulture)
                || fileName.EndsWith(".icon", true, CultureInfo.CurrentCulture)) {
                // ICO文件类型获取图标内容
                List<Bitmap> icos = GetIconImage(fileName, iconSize);
                return ImageUtils.BitmapToBitmapImage(icos[icos.Count - 1]);
            } else {
                // 获取文件类型匹配的系统图标，比如.mp3用千千静听打开的，那么就是获取千千静听软体图标
                return ImageUtils.BitmapToBitmapImage(GetIcon(fileName, iconSize));
            }

        }
        /// <summary>
        /// 绘制方块图
        /// </summary>
        /// <param name="color"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static Image DrawImage(Color color, int width, int height) {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            Rectangle rect = new Rectangle(0, 0, width, height);//定义矩形,参数为起点横纵坐标以及其长和宽
            SolidBrush b1 = new SolidBrush(color);//定义单色画刷         
            g.FillRectangle(b1, rect);//填充这个矩形
            b1.Dispose();
            g.Dispose();
            return bmp;
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
        /// 如果iconSize超过48，但是文件中没有iconSize的图标，
        /// icon其实是一个256*256的，实际图标在左上角位置，需要对有效图片根据iconSize缩放
        /// </remarks>
        /// <param name="fileName">文件类型的扩展名或文件的绝对路径</param>
        /// <param name="type">类型 file/dir</param>
        /// <param name="isLargeIcon">是否返回大图标</param>
        /// <returns>获取到的图标</returns>
        public static Bitmap GetIcon(string fileName, int iconSize) {
            DllUtils.SHFILEINFO shfi = new DllUtils.SHFILEINFO();
            uint fileInfo = (uint)WinApi.FileInfoFlags.SHGFI_ICON;
            bool isLarge = false;
            if (Constants.ICON_SIZE_32 == iconSize) {
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_LARGEICON;
            } else if (Constants.ICON_SIZE_48 == iconSize) {
                fileInfo |= (uint)WinApi.FileInfoFlags.SHGFI_SYSICONINDEX;
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

                int size = 0x4;
                DllUtils.SHGetImageList(size, ref iidImageList, out IImageList iml); // writes iml

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
            // 检测实际的图标内容区域并缩放
            Bitmap result = ScaleIconContentToSize(bs, iconSize);
            bs.Dispose();

            return result;
        }


        private static Bitmap ScaleIconContentToSize(Bitmap original, int targetSize) {
            // 1. 找到实际的图标内容边界
            Rectangle contentBounds = FindContentBounds(original);

            // 2. 如果没有找到有效内容，直接缩放整个图像
            if (contentBounds.Width == 0 || contentBounds.Height == 0) {
                return ScaleBitmap(original, targetSize, targetSize);
            }

            // 3. 提取内容区域
            Bitmap content = new Bitmap(contentBounds.Width, contentBounds.Height);
            using (Graphics g = Graphics.FromImage(content)) {
                g.DrawImage(original, new Rectangle(0, 0, contentBounds.Width, contentBounds.Height),
                           contentBounds, GraphicsUnit.Pixel);
            }

            // 4. 将内容区域缩放到目标尺寸
            Bitmap scaledContent = ScaleBitmap(content, targetSize, targetSize);
            content.Dispose();

            return scaledContent;
        }


        private static Rectangle FindContentBounds(Bitmap bitmap) {
            int left = bitmap.Width, right = 0, top = bitmap.Height, bottom = 0;
            bool foundContent = false;

            // 使用LockBits提高性能
            BitmapData data = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                                             ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            unsafe {
                byte* ptr = (byte*)data.Scan0;

                for (int y = 0; y < data.Height; y++) {
                    for (int x = 0; x < data.Width; x++) {
                        int index = y * data.Stride + x * 4;
                        byte alpha = ptr[index + 3]; // Alpha通道

                        if (alpha > 10) { // 非完全透明
                            foundContent = true;
                            if (x < left) left = x;
                            if (x > right) right = x;
                            if (y < top) top = y;
                            if (y > bottom) bottom = y;
                        }
                    }
                }
            }

            bitmap.UnlockBits(data);

            if (!foundContent)
                return Rectangle.Empty;

            // 添加一些边距，避免裁剪太紧
            int margin = 1;
            left = Math.Max(0, left - margin);
            top = Math.Max(0, top - margin);
            right = Math.Min(bitmap.Width - 1, right + margin);
            bottom = Math.Min(bitmap.Height - 1, bottom + margin);

            return new Rectangle(left, top, right - left + 1, bottom - top + 1);
        }

        private static Bitmap ScaleBitmap(Bitmap original, int width, int height) {
            Bitmap scaled = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(scaled)) {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                // 清除背景为透明
                g.Clear(Color.Transparent);
                g.DrawImage(original, 0, 0, width, height);
            }
            return scaled;
        }
    }
}
