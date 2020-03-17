using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace RomanovTestCom.Common
{
    public static class Checks
    {
        public static bool KeyIsNumber(Key key)
        {
            return key >= Key.D0 && key <= Key.D9;
        }
    }
}
