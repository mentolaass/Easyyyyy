using System.Runtime.InteropServices;

namespace Easyyyyy.Core
{
    public class Mouse : Win32
    {
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
