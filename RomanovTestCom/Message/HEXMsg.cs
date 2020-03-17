using System;
using System.Linq;

namespace RomanovTestCom
{
    /// <summary>
    /// Класс собщений для ввода в формате HEX
    /// </summary>
    class HEXMsg : IMsg
    {
        private byte[] msg;

        public byte[] Bytes { get { return msg; } }

        public string GetHex()
        {
            return BitConverter.ToString(msg).ToUpper();
        }

        public bool IsCorrect(char simvol)
        {
            return Uri.IsHexDigit(simvol);
        }

        public void SetMessage(string msg)
        {
            if(msg.Length % 2 != 0)
            {
                msg = 0 + msg;
            }
            this.msg = Enumerable.Range(0, msg.Length)
            .Where(x => x % 2 == 0)
            .Select(x => Convert.ToByte(msg.Substring(x, 2), 16))
            .ToArray();
        }

    }
}
