﻿using ITSOPCCourseCode.OPCUA.SampleClient.Services;
using ITSOPCCourseCode.OPCUA.SampleClient.DTO;
using System;
using System.Collections.Generic;

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

            var node = communicationService.ReadNode("ns=6;s=MyLevel");

            var nodeList = communicationService.BrowseNode("ns=6;s=MyDevice");

            double valueToWrite = 33;

            communicationService.WriteToNode(new NodeWritingRequest<double>("ns=6;s=MyLevel", valueToWrite));

            //Subscription
            var items = new List<string>();
            items.Add("ns=6;s=MyLevel");

            communicationService.SubscribeToNodeChanges(items, 1000);

            communicationService
                .NodeValueChanged += PlcCommunicationService_NodeValueChanged;

            Console.Read();

        }

        private static void PlcCommunicationService_NodeValueChanged(object sender, NodeValueChangedNotification e)
        {
            Console.WriteLine($"Node {e.NodeId} with value {e.Value}");
        }
    }
}
