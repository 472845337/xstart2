using System.Runtime.InteropServices;

namespace XStart2._0.Interfaces {
    [StructLayout(LayoutKind.Sequential)]
    public struct RECT {
        private int _Left;
        private int _Top;
        private int _Right;
        private int _Bottom;
    }
}
