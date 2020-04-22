using Microsoft.AspNet.SignalR;
using System;

namespace Meeting_App
{
    public class Requestlog : Hub
    {
        public static void PostToClient(string data)
        {
            try
            {
                var chat = GlobalHost.ConnectionManager.GetHubContext("Requestlog");
                if (chat != null)
                    chat.Clients.All.postToClient(data);
            }
            catch (Exception ex)
            {

            }
        }
        public void PostToClient(string name, string message)
        {
            try
            {
                var chat = GlobalHost.ConnectionManager.GetHubContext("Requestlog");
                if (chat != null)
                   chat.Clients.All.postToClient(name, message);
            }
            catch (Exception ex)
            {

            }
            
        }
    }
}