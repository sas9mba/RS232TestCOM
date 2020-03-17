using System;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RomanovTestCom
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex regex = new Regex("[^0-9.-]+");
        public MainWindow()
        {
            InitializeComponent();
        }

        private ComPort currentPort;
        private IMsg currentMsg = new HEXMsg();

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            cbPorts.ItemsSource = ComPort.GetAllNamesPort();
            cbPorts.SelectedIndex = 0;
            currentPort = new ComPort(cbPorts.SelectedValue.ToString());
        }

        private void cbPorts_DropDownOpened(object sender, EventArgs e)
        {
            cbPorts.ItemsSource = ComPort.GetAllNamesPort();
        }

        private void cbPorts_DropDownClosed(object sender, EventArgs e)
        {
            currentPort = new ComPort(cbPorts.SelectedValue.ToString());
            changeBtnConnect();
        }

        /// <summary>
        /// Изменить текст кнопки подключения
        /// </summary>
        private void changeBtnConnect()
        {
            if (currentPort.IsOpen())
            {
                btnConnect.Content = "Отключиться";
                cbPorts.IsEnabled = true;
                btnSend.IsEnabled = true;
                cbPorts.IsEnabled = false;
            }
            else
            {
                btnConnect.Content = "Подключиться";
                cbPorts.IsEnabled = false;
                btnSend.IsEnabled = false;
                cbPorts.IsEnabled = true;
            }
        }

        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!currentPort.IsOpen())
                {
                    currentPort.SetPortSettings(valSpeed: Convert.ToInt32(tbSpeedVol.Text));
                    currentPort.SetSubscriber(new SerialDataReceivedEventHandler(OnDataReceived));
                    currentPort.Open();

                }
                else
                {
                    currentPort.Close();
                }
            }
            catch (Exception expt)
            {
                MessageBox.Show(expt.Message);
            }
            

            changeBtnConnect();
        }

        private void rbMsgASCII_Checked(object sender, RoutedEventArgs e)
        {
            
            if ((Boolean)rbMsgASCII.IsChecked)
            {
                currentMsg = new ASCIIMsg();
            }
            else
            {
                currentMsg = new HEXMsg();
            }
            tbMessege.Clear();
        }

        private void tbMessege_KeyDown(object sender, KeyEventArgs e)
        {
            if (InputLanguageManager.Current.CurrentInputLanguage.Name.ToLower().Equals("en-us") && currentMsg.IsCorrect(e.Key.ToString()[0]))
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }

        private async void btnSend_Click(object sender, RoutedEventArgs e)
        {
            if(currentPort.IsOpen())
            {
                currentMsg.SetMessage(tbMessege.Text);
                tbLog.AppendText(await Task.Factory.StartNew<string>(() => currentPort.Send(currentMsg)));
            }
        }

        /// <summary>
        /// Захват входящего сообщения с порта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void OnDataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            
            String result = await Task.Factory.StartNew<string>(() => ComPort.Read(sender as SerialPort));
            await Dispatcher.BeginInvoke((Action)(() => this.tbLog.AppendText(result)));
        }

        private void tbSpeedVol_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key>=Key.D0 && e.Key <= Key.D9) 
            {
                return;
            }
            else
            {
                e.Handled = true;
            }
        }
    }
}
