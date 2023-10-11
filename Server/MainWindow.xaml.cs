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
using System.Net;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using System.Threading;

namespace Терпила //server
{
    public partial class MainWindow : Window
    {

        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private Thread listeningThread;


        public MainWindow()
        {
            InitializeComponent();
            server = new TcpListener(IPAddress.Parse("192.168.221.228"), 1234);
            server.Start();
            listeningThread = new Thread(ListenForClient);
            listeningThread.Start();

            foreach (string imagePath in Directory.GetFiles("C:\\Users\\tihon\\source\\repos\\Server\\картинки"))
                ImagePath.Add(System.IO.Path.Combine(Directory.GetCurrentDirectory(), imagePath));

            image.DataContext = this;
            BitmapImage bitmapImage = new BitmapImage(new Uri(ImagePath[0]));
            image.Source = bitmapImage;
        }

        int currentIndex;
        List<string> ImagePath = new List<string>();

        private void ListenForClient()
        {
            client = server.AcceptTcpClient();
            stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;

            while (true)
            {
                bytesRead = 0;
                try
                {
                    bytesRead = stream.Read(buffer, 0, buffer.Length);
                }
                catch
                {
                    break;
                }
                if (bytesRead == 0)
                {
                    break;
                }
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                if (message == "ButtonPressed")
                {

                    Dispatcher.Invoke(() =>
                    {
                        
                        currentIndex++;
                        if (currentIndex >= ImagePath.Count)
                        {
                            currentIndex = 0;
                        }
                        BitmapImage bitmapImage = new BitmapImage(new Uri(ImagePath[currentIndex]));
                        image.Source = bitmapImage;
                        Label.Content += "\nГде-то в мире кто-то нажал какую-то кнопку...\nТеперь кот танцует";
                    });
                }
            }
            client.Close();
            server.Stop();
        }
    }
}