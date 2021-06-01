using System;
using System.Collections.Generic;
using System.Text;
using Opc.Ua;
using Opc.Ua.Server;
using System.Reflection;

namespace ITSOPCCourseCode.OPCUA.SampleServer
{
    class SampleNodeManager : CustomNodeManager2
    {
        private static BikeState opcUaServer;

        public SampleNodeManager(IServerInternal server, ApplicationConfiguration configuration) : base(server, configuration)
        {
            SystemContext.NodeIdFactory = this;

            string[] namespaceUrls = new string[2];
            namespaceUrls[0] = Namespaces.SamplePlant;
            namespaceUrls[1] = Namespaces.SamplePlant + "/Instance";
            SetNamespaces(namespaceUrls);
        }

        protected override NodeStateCollection LoadPredefinedNodes(ISystemContext context)
        {
            NodeStateCollection predefinedNodes = new NodeStateCollection();
            predefinedNodes.LoadFromBinaryResource(context,
                @"..\..\..\ModelDesignOutput\SamplePlant.PredefinedNodes.uanodes",
                typeof(SampleNodeManager).GetTypeInfo().Assembly,
                true);

            return predefinedNodes;
        }

        public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            lock (Lock)
            {
                LoadPredefinedNodes(SystemContext, externalReferences);

                // find the untyped Batch Plant 1 node that was created when the model was loaded.
                BaseObjectState passiveNode = (BaseObjectState)FindPredefinedNode(new NodeId(Objects.Bike,
                    NamespaceIndexes[0]), typeof(BaseObjectState));

                // convert the untyped node to a typed node that can be manipulated within the server.
                opcUaServer = new BikeState(null);
                opcUaServer.Create(SystemContext, passiveNode);

                // replaces the untyped predefined nodes with their strongly typed versions.
                AddPredefinedNode(SystemContext, opcUaServer);
            }
        }
    }
}
