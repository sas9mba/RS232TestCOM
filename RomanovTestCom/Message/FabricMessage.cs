using System;
using System.Collections.Generic;
using System.Text;

namespace RomanovTestCom.Message
{
    /// <summary>
    /// Фабрика отвечает объект какого тип сообщения нужно сгенерировать
    /// </summary>
    class FabricMessage
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        MessageType msgType;

        public FabricMessage(MessageType msgType)
        {
            this.msgType = msgType;
        }

        /// <summary>
        /// Получить объект установленного типа сообщения
        /// </summary>
        /// <returns></returns>
        public IMsg GetObjMsg()
        {
            if (msgType == MessageType.ASCII)
            {
                return new ASCIIMsg();
            }
            else
            {
                return new HEXMsg();
            }
        }
        /// <summary>
        /// Установить текущий тип сообщения
        /// </summary>
        /// <param name="msgType">тип сообщения</param>
        public void SetTypeMsg(MessageType msgType)
        {
            this.msgType = msgType;
        }
    }
}
