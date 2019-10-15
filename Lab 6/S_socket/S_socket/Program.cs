using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace S_socket
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                bool status = true;
                string servermsg = "";
                string clientmsg = "";

                TcpListener tcplsitner = new TcpListener(8080);
                tcplsitner.Start();
                Console.WriteLine("Server Running..");

                Socket socketForClient = tcplsitner.AcceptSocket();
                Console.WriteLine("Client Accepted...");
                NetworkStream networkStream = new NetworkStream(socketForClient);

                StreamWriter writer = new StreamWriter(networkStream);
                StreamReader reader = new StreamReader(networkStream);

                while (status)
                {
                    if (socketForClient.Connected)
                    {
                        servermsg = reader.ReadLine();
                        Console.WriteLine("Client: "+servermsg);
                        if (servermsg=="bye" || servermsg=="Bye")
                        {
                            reader.Close(); 
                            writer.Close(); 
                            networkStream.Close();

                            return;
                        }
                        Console.WriteLine("Server:");
                        clientmsg = Console.ReadLine();
                        writer.WriteLine(clientmsg);
                        writer.Flush();
                    }
                }

                reader.Close();
                writer.Close();
                networkStream.Close();
                Console.WriteLine("Exiting Application");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                
            }
        }
    }
}
