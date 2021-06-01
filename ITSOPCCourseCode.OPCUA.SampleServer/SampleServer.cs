using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;
using Opc.Ua.Server;

namespace ITSOPCCourseCode.OPCUA.SampleServer
{
    class SampleServer : StandardServer
    {
        protected override MasterNodeManager CreateMasterNodeManager(IServerInternal server, ApplicationConfiguration configuration)
        {
            Utils.Trace("Creating the Node Managers.");

            List<INodeManager> nodeManagers = new List<INodeManager>();

            // create the custom node managers.
            nodeManagers.Add(new SampleNodeManager(server, configuration));

            // create master node manager.
            return new MasterNodeManager(server, configuration, null, nodeManagers.ToArray());
        }

        protected override ServerProperties LoadServerProperties()
        {
            ServerProperties properties = new ServerProperties();

            properties.ManufacturerName = "MV Labs";
            properties.ProductName = "ITS Sample OPC UA Server";
            properties.ProductUri = "http://iiot.its/SamplePlant";
            properties.SoftwareVersion = Utils.GetAssemblySoftwareVersion();
            properties.BuildNumber = Utils.GetAssemblyBuildNumber();
            properties.BuildDate = Utils.GetAssemblyTimestamp();

            return properties;
        }
    }
}
