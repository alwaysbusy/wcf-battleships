using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using BattleshipsLib;
using System.ServiceModel.Description;

namespace BattleshipsHost
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri baseAddress = new Uri("http://localhost:1337/Battleships");
            ServiceHost selfHost = new ServiceHost(typeof(BattleshipsService), baseAddress);

            try
            {
                selfHost.AddServiceEndpoint(typeof(IBattleships), new WSHttpBinding(), "BattleshipsService");

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;
                selfHost.Description.Behaviors.Add(smb);

                selfHost.Open();
                Console.WriteLine("Service is running");
                Console.WriteLine("Press <ENTER> to terminate");
                Console.ReadLine();

                selfHost.Close();
            }
            catch (CommunicationException ce)
            {
                Console.WriteLine("An exception occurred: {0}", ce.Message);
                selfHost.Abort();
            }
        }
    }
}
