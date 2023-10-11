


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Net.Sockets;
using System.Reflection.Emit;
using System.Windows.Media.TextFormatting;
using System.Net;

namespace Спамер 
{
    public partial class MainWindow : Window
    {

        private TcpClient client;
        private NetworkStream stream;

        public MainWindow()
        {
            InitializeComponent();
            client = new TcpClient();
            client.Connect("192.168.221.228", 1234);
            stream = client.GetStream();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            byte[] message = Encoding.ASCII.GetBytes("ButtonPressed");
            stream.Write(message, 0, message.Length);
            Label.Content += "\nКартинка изменена!\nВы успешно напакостили";
        }
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            stream.Close();
            client.Close();
        }


    }
}