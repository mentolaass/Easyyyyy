using System.Runtime.InteropServices;

namespace Easyyyyy.Core
{
    public class Native
    {
        [DllImport("User32.dll")]
        public static extern bool GetAsyncKeyState(int vKey);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void mouse_event(MouseEvent dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        public enum MouseEvent
        {
            MOUSEEVENTF_LEFTDOWN = 0x02,
            MOUSEEVENTF_LEFTUP = 0x04,
            MOUSEEVENTF_RIGHTDOWN = 0x08,
            MOUSEEVENTF_RIGHTUP = 0x10,
        }
    }
}
