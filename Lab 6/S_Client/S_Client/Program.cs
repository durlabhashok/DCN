using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.IO;


namespace S_Client
{
    class Program
    {
        static void Main(string[] args)
        {
            bool status = true;
            TcpClient socketForServer;
            try
            {
                socketForServer = new TcpClient("localhost",8080);
                Console.WriteLine("Connected with Server...");


            }
            catch
            {
                
                 Console.WriteLine("Failed to connect");
                 return;
            }

            NetworkStream networkStream = socketForServer.GetStream();
            StreamReader reader = new StreamReader(networkStream);
            StreamWriter writer = new StreamWriter(networkStream);

            try
            {
                string servermsg = "";
                string clientmsg = "";

                while (status)
                {
                    Console.WriteLine("Client: ");
                    clientmsg = Console.ReadLine();

                    if (clientmsg == "bye" || clientmsg == "Bye")
                    {
                        status = false;

                        writer.WriteLine("bye");
                        writer.Flush();

                        return;
                    }
                    else
                    {
                        writer.WriteLine(clientmsg);
                        writer.Flush();
                        servermsg = reader.ReadLine();
                        Console.WriteLine("Server: " + servermsg);
                    }
                }

            }
            catch
            {

                Console.WriteLine("Error from server app");
            }
            reader.Close();
            writer.Close();
            networkStream.Close();

        }
    }
}
