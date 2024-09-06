using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;

namespace XStart2._0.Utils {
    internal class FontUtils {
        private static List<string> fontNameList;
        internal static List<string> GetSystemFonts() {
            if (fontNameList == null) {
                fontNameList = new List<string>();
                InstalledFontCollection installedFontCollection = new InstalledFontCollection();
                foreach (FontFamily fontFamily in installedFontCollection.Families) {
                    if (fontFamily.IsStyleAvailable(FontStyle.Regular)) {
                        fontNameList.Add(fontFamily.Name);
                    }
                }
                fontNameList.Reverse();
            }
            return fontNameList;
        }

        internal static bool IsSystemFont(string fontName) {
            if (null == fontNameList) {
                fontNameList = GetSystemFonts();
            }
            return fontNameList.Contains(fontName);
        }
    }
}
