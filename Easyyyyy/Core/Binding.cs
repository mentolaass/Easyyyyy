using System.Windows.Input;

namespace Easyyyyy.Core
{
    class Binding
    {
        public int GetIntVirtualKey(Key key)
        {
            return KeyInterop.VirtualKeyFromKey(key);
        }

        public string GetStringVirtualKey(Key key)
        {
            return key.ToString();
        }
    }
}
