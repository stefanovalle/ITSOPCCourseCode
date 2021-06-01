using System;
using Opc.Ua;
using Opc.Ua.Configuration;

namespace ITSOPCCourseCode.OPCUA.SampleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationInstance application = new ApplicationInstance();
            application.ApplicationType = ApplicationType.Server;
            application.ConfigSectionName = "ITS.SampleServer";

            try
            {

                // load the application configuration.
                application.LoadApplicationConfiguration(@"..\..\..\SampleServer.Config.xml", false).Wait();

                // check the application certificate.
                application.CheckApplicationInstanceCertificate(false, 0).Wait();
                
                // start the server.
                application.Start(new SampleServer()).Wait();

                foreach (EndpointDescription endpoint in application.Server.GetEndpoints())
                {
                    Console.WriteLine(endpoint.EndpointUrl);
                }

                Console.WriteLine("Press Enter to close the server");
                Console.ReadLine();

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
