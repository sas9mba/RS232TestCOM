using System;
using System.Text;

namespace RomanovTestCom
{
    /// <summary>
    /// Класс сообщения для в вода в формате ASCII
    /// </summary>
    class ASCIIMsg : IMsg
    {
        byte[] msg;
        public byte[] Bytes { get { return msg; }}

        public string GetHex()
        {
            return BitConverter.ToString(msg).ToUpper();
        }

        public bool IsCorrect(char simvol)
        {
            return true;
        }

        public void SetMessage(string msg)
        {
            this.msg = Encoding.ASCII.GetBytes(msg);
        }
    }
}
