using System;
using System.Text.Encodings;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpListener listener = new TcpListener(System.Net.IPAddress.Any, 2137);
            listener.Start();
            while (true) 
            {
                Console.Write('>');
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                StreamReader sr = new StreamReader(client.GetStream());
                StreamWriter sw = new StreamWriter(client.GetStream());
                try
                {
                    byte[] buffer = new byte[1024];
                    stream.Read(buffer, 0, buffer.Length);
                    int recv = 0;
                    foreach (byte b in buffer)
                    {
                        if (b != 0)
                        {
                            recv++;
                        } 
                    }

                    int players = 0;
                    
                    try
                    {
                        string request = Encoding.UTF8.GetString(buffer, 0, buffer.Length);
                        Console.WriteLine(request);
                        if (request == "connect")
                        {
                            players++;
                            if (players <= 2)
                            {
                                byte[] sendData = BitConverter.GetBytes(players);
                                NetworkStream ns = client.GetStream();
                                ns.Write(sendData, 0 ,sendData.Length);
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        int request = BitConverter.ToInt32(buffer, 0);
                        Console.WriteLine(request);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }
    }
}