using System;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Servidor_Sockets
{
    class Program
    {
        static async Task Main(string[] args)
        {
            void sendData(string value, Socket listener)
            {
                byte[] sendInfo = new byte[100];
                sendInfo = Encoding.Default.GetBytes(value);
                listener.Send(sendInfo);
            }
            void receiveData(Socket theConection)
            {
                byte[] receptionData = new byte[10000];
                string data;
                int tamArray = 0;
                tamArray = theConection.Receive(receptionData, 0, receptionData.Length, 0);
                Array.Resize(ref receptionData, tamArray);
                data = Encoding.Default.GetString(receptionData);
                Console.WriteLine($"La info recibida es: {data}");
            }
            void options()
            {
                Console.WriteLine("Ingrese 1 para encender el led Rojo \n");
                Console.WriteLine("Ingrese 2 para encender el led Amarillo \n");
                Console.WriteLine("Ingrese 3 para encender el led Verde \n");
                Console.WriteLine("Ingrese 4 para apagar los leds \n");
            }
            Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Socket conection;
            IPEndPoint connect = new IPEndPoint(IPAddress.Parse("192.168.1.5"), 6400);
            listen.Bind(connect);
            listen.Listen(10);
            
            conection = listen.Accept();
            Console.WriteLine($"Conection Aceptado");
            receiveData(conection);
            var option="";
            //while (option => a = Console.ReadLine(); != 0)
            do
            {
                
                options();
                option = Console.ReadLine();
                var recibirs = Task<string>.Run(async() =>
                {
                    receiveData(conection);
                });
                await recibirs;
                sendData(option, conection);
                Console.ReadLine();

            }
            while (option != "0") ;
            Console.ReadKey();
        }
    }
}
/*
 void sendData(string value, Socket listener)
        {
            byte[] sendInfo = new byte[100];
            sendInfo = Encoding.Default.GetBytes(value);
            listener.Send(sendInfo);
        }
        void receiveData(Socket theConection)
        {
            byte[] receptionData = new byte[10000];
            string data;
            int tamArray = 0;
            tamArray = theConection.Receive(receptionData, 0, receptionData.Length, 0);
            Array.Resize(ref receptionData, tamArray);
            data = Encoding.Default.GetString(receptionData);
            Console.WriteLine($"La info recibida es: {data}");
        }
        Socket listen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        Socket conection;
        IPEndPoint connect = new IPEndPoint(IPAddress.Parse("169.254.166.7"), 6400);
        listen.Bind(connect);
        listen.Listen(10);

        conection = listen.Accept();
        Console.WriteLine($"Conection Accepted\nPress any key to continue...");
        Console.ReadLine();
        Console.Clear();
        Console.WriteLine($"asd");
        receiveData(conection);
        Console.WriteLine($"asd2");
        int[] option = new int[4] { 0, 0, 0, 1 };
        bool go = true;
        string led;
            
        while (option[3]==1)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("1.");
            if (option[0]==1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            Console.WriteLine("███");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("2.");
            if (option[1] == 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
            }
            Console.WriteLine("███\t");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("3.");
            if (option[2] == 1)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.WriteLine("███\t");
            led = Console.ReadLine();
            if (option[Int32.Parse(led) - 1] == 0)
            {
                option[Int32.Parse(led) - 1] = 1;
            }
            else
            {
                option[Int32.Parse(led) - 1] = 0;
            }
            sendData(led, conection);
            Console.Clear();
        }
        Console.ReadKey();
      }
    }
}
*/