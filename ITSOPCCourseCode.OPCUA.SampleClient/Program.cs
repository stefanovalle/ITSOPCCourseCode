using ITSOPCCourseCode.OPCUA.SampleClient.Services;
using ITSOPCCourseCode.OPCUA.SampleClient.DTO;
using System;

namespace ITSOPCCourseCode.OPCUA.SampleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var communicationService = new PlcCommunicationService();

            communicationService.StartAsync("opc.tcp://uademo.prosysopc.com:53530/OPCUA/SimulationServer").GetAwaiter().GetResult();

            var valueRead = communicationService.ReadNodeValue("ns=6;s=MyLevel");
            Console.WriteLine($"Readed value: {valueRead}");

            var nodeList = communicationService.BrowseNode("ns=6;s=MyDevice");

            double valueToWrite = 33;

            communicationService.WriteToNode(new NodeWritingRequest<double>("ns=6;s=MyLevel", valueToWrite));

        }
    }
}
