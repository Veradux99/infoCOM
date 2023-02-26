using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InfoCOM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SerialPort mySerialPort = new SerialPort("COM3");
        string resultString;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void stopButton(object sender, RoutedEventArgs e)
        {

            mySerialPort.Close();
            File.WriteAllText("text.txt", resultString);
            MessageBox.Show("text written in text.txt");
            resultString="";
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            resultString += indata;
            this.Dispatcher.Invoke(() =>
            {
                Nume.Text = resultString;
            });

        }

        private void startButton(object sender, RoutedEventArgs e)
        {
            try
            {
                mySerialPort.PortName = "COM3";
                mySerialPort.BaudRate = 9600;
                mySerialPort.Parity = Parity.None;
                mySerialPort.StopBits = StopBits.One;
                mySerialPort.DataBits = 8;
                mySerialPort.Handshake = Handshake.None;

                mySerialPort.Open();

                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            }
            catch
            {
                MessageBox.Show("Something went wrong!");
            }
        }
    }
}
