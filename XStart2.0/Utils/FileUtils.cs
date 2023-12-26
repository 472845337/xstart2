﻿using IWshRuntimeLibrary;
using System;
using System.IO;

namespace XStart2._0.Utils {
    public class FileUtils {
        public static void CreateShortCut(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null) {
            if (!Directory.Exists(directory)) {
                Directory.CreateDirectory(directory);
            }
            string shortcutPath = GetUnexistLinkName(directory, shortcutName);

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            shortcut.TargetPath = targetPath;
            shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);
            shortcut.WindowStyle = 1;
            shortcut.Description = description;
            shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;
            shortcut.Save();
        }

        public static ShortCutUtil ReadShortCut(string path) {
            if (System.IO.File.Exists(path)) {
                WshShell shell = new WshShell();
                IWshShortcut shortCut = (IWshShortcut)shell.CreateShortcut(path);
                ShortCutUtil scu = new ShortCutUtil {
                    FullName = shortCut.FullName,
                    Arguments = shortCut.Arguments,
                    Description = shortCut.Description, Hotkey = shortCut.Hotkey, TargetPath = shortCut.TargetPath, WindowStyle = shortCut.WindowStyle, WorkingDirectory = shortCut.WorkingDirectory

                };
                return scu;
            } else {
                return null;
            }

        }

        private static string GetUnexistLinkName(string directory, string shortcutName) {
            string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
            int index = 1;
            while (System.IO.File.Exists(shortcutPath)) {
                shortcutPath = Path.Combine(directory, string.Format("{0} ({1}).lnk", shortcutName, index++));
            }
            return shortcutPath;
        }

        public static void CreateShortCutOnDesktop(string shortcutName, string targetPath, string description = null, string iconLocation = null) {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            CreateShortCut(desktop, shortcutName, targetPath, description, iconLocation);
        }

        public class ShortCutUtil {
            public string FullName { get; set; }
            public string Arguments { get; set; }
            public string Description { get; set; }
            public string Hotkey { get; set; }
            public string IconLocation { get; set; }
            public string TargetPath { get; set; }
            public int WindowStyle { get; set; }
            public string WorkingDirectory { get; set; }
        }
    }
}
