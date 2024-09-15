using CopyDel.Enum;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CopyDel.Models
{

    public class ScreenCapturer
    {
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);
        public Bitmap Capture(CaptureMode screenCaptureMode = CaptureMode.Window)
        {
            Rectangle bounds;

            if (screenCaptureMode == CaptureMode.Screen)
            {
                bounds = Screen.GetBounds(Point.Empty);
                CursorPosition = Cursor.Position;
            }
            else
            {
                var handle = GetForegroundWindow();
                var rect = new Rect();
                GetWindowRect(handle, ref rect);

                bounds = new Rectangle(rect.Left, rect.Top, rect.Right, rect.Bottom);
                //CursorPosition = new Point(Cursor.Position.X - rect.Left, Cursor.Position.Y - rect.Top);
            }

            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var g = Graphics.FromImage(result))
            {
                g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }

        public Point CursorPosition
        {
            get;
            protected set;
        }
    }
}
