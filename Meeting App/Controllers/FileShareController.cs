using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Meeting_App.Controllers
{
    public class FileShareController : ApiController
    {
        // GET: api/FileShare
        public string Get(int Port , string IP)
        {
            int port = Port;
            IPAddress IpAddress = IPAddress.Parse(IP);
            
            for (; ; )
            {
                Task.Factory.StartNew(() => HandleIncomingFile(port, IpAddress));
                Debug.WriteLine("Listening on port " + port);
                return ("Listening on port " + port);
            }
          
        }

        private void HandleIncomingFile(int port, IPAddress ipAddress)
        {
              try
                {
                    TcpListener tcpListener = new TcpListener(ipAddress, port);
                    tcpListener.Start();
                    while (true)
                    {
                        Socket handlerSocket = tcpListener.AcceptSocket();
                        if (handlerSocket.Connected)
                        {
                            string fileName = string.Empty;
                            NetworkStream networkStream = new NetworkStream(handlerSocket);
                            int thisRead = 0;
                            int blockSize = 1024;
                            Byte[] dataByte = new Byte[blockSize];
                            lock (this)
                            {
                                string folderPath = @"c:\";
                                handlerSocket.Receive(dataByte);
                                int fileNameLen = BitConverter.ToInt32(dataByte, 0);
                                fileName = Encoding.ASCII.GetString(dataByte, 4, fileNameLen);
                                Stream fileStream = File.OpenWrite(folderPath + fileName);
                                fileStream.Write(dataByte, 4 + fileNameLen, (1024 - (4 + fileNameLen)));
                                while (true)
                                {
                                    thisRead = networkStream.Read(dataByte, 0, blockSize);
                                    fileStream.Write(dataByte, 0, thisRead);
                                    if (thisRead == 0)
                                        break;
                                }
                                fileStream.Close();
                            }
                            //if (NewFileRecieved != null)
                            //{
                            //    NewFileRecieved(this, fileName);
                            //}
                            handlerSocket = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                     //  (ex.Message.ToString());
                }
            }
        public delegate void FileRecievedEventHandler(object source, string fileName);

      //  public event FileRecievedEventHandler NewFileRecieved;

        //private void Form1_NewFileRecieved(object sender, string fileName)
        //{
        //    this.BeginInvoke(
        //   new Action(
        //   delegate ()
        //   {
        //       //("New File Recieved\n" + fileName);
        //       System.Diagnostics.Process.Start("explorer", @"c:\");
        //   }));
        //}


        // GET: api/FileShare/5
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/FileShare
        public string Post(int Port, string IP, string FileName)
        {
             string shortFileName = "";
            // string fileName = "";

            string ipAddress = IP;
            int port = Port;
            string fileName = FileName;
            Task.Factory.StartNew(() => SendFile(ipAddress, port, fileName, shortFileName));
           return ("File Sent");
        }

        public void SendFile(string remoteHostIP, int remoteHostPort, string longFileName, string shortFileName)
        {
            try
            {
                if (!string.IsNullOrEmpty(remoteHostIP))
                {
                    byte[] fileNameByte = Encoding.ASCII.GetBytes(shortFileName);
                    byte[] fileData = File.ReadAllBytes(longFileName);
                    byte[] clientData = new byte[4 + fileNameByte.Length + fileData.Length];
                    byte[] fileNameLen = BitConverter.GetBytes(fileNameByte.Length);
                    fileNameLen.CopyTo(clientData, 0);
                    fileNameByte.CopyTo(clientData, 4);
                    fileData.CopyTo(clientData, 4 + fileNameByte.Length);
                    TcpClient clientSocket = new TcpClient(remoteHostIP, remoteHostPort);
                    if (clientSocket.Connected == true)
                    {
                        NetworkStream networkStream = clientSocket.GetStream();
                        networkStream.Write(clientData, 0, clientData.GetLength(0));
                        networkStream.Close();
                    }
                    else
                    {
                      //("Not connected");
                    }
                }
            }
            catch (Exception ex)
            {
              //  MessageBox.Show(ex.ToString());
            }
        }
    

    
    }
}
