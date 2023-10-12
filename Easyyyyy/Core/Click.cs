using System;
using System.Threading;

namespace Easyyyyy.Core
{
    class Click
    {

        public static void execClick(int countCPS, bool isEnabledRandom, bool isToggleEnabled, bool isLeftClick, bool isDefaultClicks, bool isToggleMode)
        {
            int timeToWait = 0;
            if (isEnabledRandom && countCPS > 5) timeToWait = (1000 / new Random().Next(countCPS - ((countCPS / 100) * 50), countCPS));
            else timeToWait = (1000 / countCPS);

            var mouse = new Core.Mouse();

            if (isToggleMode)
            {
                if (isToggleEnabled)
                {
                    // if double click
                    if (isDefaultClicks)
                    {
                        mouse.oneClick(isLeftClick);
                    }
                    else
                    {
                        mouse.doubleClick(isLeftClick);
                    }
                }

                Thread.Sleep(timeToWait);
            }
            else
            {
                // if double click
                if (isDefaultClicks)
                {
                    mouse.oneClick(isLeftClick);
                }
                else
                {
                    mouse.doubleClick(isLeftClick);
                }

                Thread.Sleep(timeToWait);
            }
        }
    }
}
