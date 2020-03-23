using RomanovTestCom.Message;
using System;
using System.IO.Ports;
using System.Threading;

namespace RomanovTestCom
{
    public class ComPort
    {
        /// <summary>
        /// COM порт
        /// </summary>
        SerialPort port;
        public ComPort(String namePort)
        {
            port = new SerialPort(namePort);
        }

        ~ComPort()
        {
            this.Close();
        }

        /// <summary>
        /// Назначить подписчика на событие
        /// </summary>
        /// <param name="subscriber"></param>
        public void SetSubscriber(SerialDataReceivedEventHandler subscriber)
        {
            port.DataReceived += subscriber;
        }

        /// <summary>
        /// Получить имена всех доступных портов
        /// </summary>
        /// <returns></returns>
        public static String[] GetAllNamesPort()
        {
            return SerialPort.GetPortNames();
        }

        /// <summary>
        /// Проверка открытия порта
        /// </summary>
        /// <returns>Статус открытия порта true - порт открыт</returns>
        public Boolean IsOpen()
        {
            return port.IsOpen;
        }

        /// <summary>
        /// Установить параметры порта
        /// </summary>
        /// <param name="valSpeed"></param>
        /// <param name="countBit"></param>
        /// <param name="ParityRule"></param>
        /// <param name="countStopBit"></param>
        public void SetPortSettings(Int32 countBit = 8, Int32 valSpeed =9600, Int32 ParityRule=0, int countStopBit = 1)
        {
            port.StopBits = (StopBits)countStopBit;
            port.BaudRate = valSpeed;
            port.DataBits = countBit;
            port.Parity = (Parity)ParityRule;
        }

        /// <summary>
        /// Открыть соединение с портом
        /// </summary>
        public void Open()
        {
            if (!port.IsOpen)
            {
                port.Open();
            }
        }

        /// <summary>
        /// Закрыть соединение с портом
        /// </summary>
        public void Close()
        {
            if(port.IsOpen)
            {
                port.Close();
            }
        }

        /// <summary>
        /// Отправить сообщение на текущий порт
        /// </summary>
        /// <typeparam name="T">Тип ограниченный интерфейсом IMsg реализующим преобразования HEX и ASCII</typeparam>
        /// <param name="msg">Объект сообщения которое необходимо отправить</param>
        /// <returns>Статус операции</returns>
        public ComResponse Send<T>(T msg) where T : IMsg
        {
            try
            {
                byte[] msgBytes = new byte[msg.Bytes.Length];
                String msgStr = msg.GetHex();
                msg.Bytes.CopyTo(msgBytes, 0);
                Monitor.Enter(port);
                port.Write(msgBytes, 0, msgBytes.Length);
                return new ComResponse(port.PortName, $"Отправлено сообщение: {msgStr} \n", true, msgBytes); ;
            }
            catch (Exception)
            {
                return new ComResponse(port.PortName, $"Не удалось отправить сообщение: {msg.GetHex()} \n");
            }
            finally
            {
                Monitor.Exit(port);
            }
        }

        /// <summary>
        /// Прочитать сообщение с указанного порта
        /// </summary>
        /// <param name="port">порт с сообщением</param>
        /// <returns>статус операции</returns>
        public static ComResponse Read(SerialPort port)
        {
            try
            {
                var buffer = new byte[port.BytesToRead];
                port.Read(buffer, 0, buffer.Length);
                return new ComResponse(port.PortName, $"С порта {port.PortName} принято сообщение: {BitConverter.ToString(buffer).ToUpper()} \n", true, buffer);
            }
            catch (Exception)
            {
                return new ComResponse(port.PortName, $"Не удалось принять сообщение с порта: {port.PortName} \n");
            }

        }

    }
}
