using System;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Podaj adres serwera: ");
            string ip = Console.ReadLine();
            
            Console.Write("Podaj port serwera: ");
            int port = int.Parse(Console.ReadLine());
            
            init_connection:
            char letter;
            try
            {
                TcpClient client = new TcpClient(ip, port);
                int byteCount = Encoding.ASCII.GetByteCount("connect");
                byte[] sendData = new byte[byteCount];
                sendData = Encoding.ASCII.GetBytes("connect");
                NetworkStream ns = client.GetStream();
                ns.Write(sendData, 0, sendData.Length);
                
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                goto init_connection;
            }
            
            string[] table = new string[9];
            for (int i = 0; i <= 8; i++) { table[i] = $"{i + 1}"; }
            
            while (true)
            {
                Console.Write($"{table[0]} | {table[1]} | {table[2]}\n---------\n{table[3]} | {table[4]} | {table[5]}\n---------\n{table[6]} | {table[7]} | {table[8]}\n\n");
                Console.Write("Wybierz pole: ");
                int input = int.Parse(Console.ReadLine());
                if (input <= 9)
                {
                    table[input - 1] = "X";
                }

                connection:
                try
                {
                    TcpClient client = new TcpClient(ip, port);
                    byte[] sendData = BitConverter.GetBytes(input);

                    NetworkStream ns = client.GetStream();
                    ns.Write(sendData, 0, sendData.Length);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Połączenie nie udane");
                    goto connection;
                }
            }

            
            

            // connection:
            // try
            // {
            //     while (true)
            //     {
            //         TcpClient client = new TcpClient("127.0.0.1", 2137);
            //         
            //         string messageToSend = Console.ReadLine();
            //         int byteCount = Encoding.ASCII.GetByteCount(messageToSend);
            //         byte[] sendData = new byte[byteCount];
            //         sendData = Encoding.ASCII.GetBytes(messageToSend);
            //
            //         NetworkStream stream = client.GetStream();
            //         stream.Write(sendData, 0, sendData.Length);
            //     }
            // }
            // catch (Exception e)
            // {
            //     goto connection;
            // }
        }
    }
}