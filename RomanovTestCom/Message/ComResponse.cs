using System;
using System.Collections.Generic;
using System.Text;

namespace RomanovTestCom.Message
{
    /// <summary>
    /// Класс содержит результат взаимодействия с портом
    /// </summary>
    public class ComResponse
    {
        /// <summary>
        /// Имя порта с которым ведется взаимодействие
        /// </summary>
        public String NamePort { get; set; }
        /// <summary>
        /// Ответ в байтах
        /// </summary>
        public byte[] MessageByte
        {
            get
            { if (IsOk) return message; else return new Byte[] { }; }
            set
            { message = value; } 
        }
        /// <summary>
        /// Успешено ли взаимодействие
        /// </summary>
        public Boolean IsOk { get; set; }
        public String TextAnswer { get; set; }

        private Byte[] message;

        public ComResponse(String namePort, String textAnswer, Boolean isOk = false, byte[] responeByte = null)
        {
            NamePort = namePort;
            IsOk = isOk;
            message = responeByte;
            TextAnswer = textAnswer;
        }
    }
}
