using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace Easyyyyy.Core
{
    public class Mouse
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(MouseEvent dwFlags, long dx, long dy, long cButtons, long dwExtraInfo);

        private enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public static implicit operator Point(POINT point)
            {
                return new Point(point.X, point.Y);
            }
        }

        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out POINT lpPoint);

        public static Point GetCursorPosition()
        {
            POINT lpPoint;
            GetCursorPos(out lpPoint);

            return lpPoint;
        }

        public void oneLeftClick()
        {
            var cursorPosition = GetCursorPosition();
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(cursorPosition.X), Convert.ToInt32(cursorPosition.Y), 0, 0);
            mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP, Convert.ToInt32(cursorPosition.X), Convert.ToInt32(cursorPosition.Y), 0, 0);
        }

        public void doubleLeftClick()
        {
            for (int x = 0; x != 2; x++)
            {
                var cursorPosition = GetCursorPosition();
                mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN, Convert.ToInt32(cursorPosition.X), Convert.ToInt32(cursorPosition.Y), 0, 0);
                mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP, Convert.ToInt32(cursorPosition.X), Convert.ToInt32(cursorPosition.Y), 0, 0);
            }
        }
    }
}
