using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace ActionApi.Helpers
{
    public class WindowsHelper
    {
        public static Bitmap CaptureWindowByName(string procName, Rectangle cropArea)
        {
            return CaptureWindow(GetWindowByName(procName), cropArea);
        }

        public static Bitmap CaptureWindow(IntPtr handle, Rectangle cropArea)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return cropArea == Rectangle.Empty ? result : cropAtRect(result, cropArea);
        }

        public static Bitmap cropAtRect(Bitmap b, Rectangle r)
        {
            var nb = new Bitmap(r.Width, r.Height);
            using var g = Graphics.FromImage(nb);
            g.DrawImage(b, -r.X, -r.Y);
            return nb;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        private static IntPtr GetWindowByName(string procName)
        {
            var proc = Process.GetProcessesByName(procName).FirstOrDefault();
            return proc?.MainWindowHandle ?? IntPtr.Zero;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}