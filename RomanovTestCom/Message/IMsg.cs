using System;

namespace RomanovTestCom
{
    /// <summary>
    /// Интерфейс определяющий поведение для всех типов сообщений
    /// </summary>
    public interface IMsg
    {
        /// <summary>
        /// Получить сообщение в шестнадцатиричном формате
        /// </summary>
        /// <returns>HexString</returns>
        String GetHex();
        /// <summary>
        /// Проверить символ на корректность ввода в текущем типе сообщения
        /// </summary>
        /// <param name="simvol">Символ для проверки</param>
        /// <returns>true символ корректен</returns>
        Boolean IsCorrect(Char simvol);
        /// <summary>
        /// Получить сообщение в виде массива байт
        /// </summary>
        /// <returns></returns>
        Byte[] Bytes { get; }
        /// <summary>
        /// Установить текст сообщеня
        /// </summary>
        /// <param name="msg">строка с текстом сообщения</param>
        void SetMessage(String msg);

    }
}
