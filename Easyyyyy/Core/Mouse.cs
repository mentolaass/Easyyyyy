using System.Runtime.InteropServices;

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

        public void oneClick(bool isLeftClick)
        {
            if (isLeftClick)
            {
                mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            } else
            {
                mouse_event(MouseEvent.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                mouse_event(MouseEvent.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            }
        }

        public void doubleClick(bool isLeftClick)
        {
            for (int x = 0; x != 2; x++)
            {
                if (isLeftClick)
                {
                    mouse_event(MouseEvent.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MouseEvent.MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                } else
                {
                    mouse_event(MouseEvent.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                    mouse_event(MouseEvent.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                }
            }
        }
    }
}
