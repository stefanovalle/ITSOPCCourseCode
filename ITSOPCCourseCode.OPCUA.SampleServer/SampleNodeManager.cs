using System.Collections.Generic;
using System.Threading;
using System;
using Opc.Ua;
using Opc.Ua.Server;
using System.Reflection;
using System.IO;

namespace ITSOPCCourseCode.OPCUA.SampleServer
{
    class SampleNodeManager : CustomNodeManager2
    {
        public SampleNodeManager(IServerInternal server, ApplicationConfiguration configuration) : base(server, configuration)
        {
            SystemContext.NodeIdFactory = this;

            string[] namespaceUrls = new string[2];
            namespaceUrls[0] = "http://iiot.its/SampleBike";
            namespaceUrls[1] = "http://iiot.its/SampleBike/Instance";
            SetNamespaces(namespaceUrls);
        }

        protected override void LoadPredefinedNodes(ISystemContext context, IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            NodeStateCollection predefinedNodes = new NodeStateCollection();

            Stream stream = new FileStream(@"NodeSet\samplebike.xml", FileMode.Open);

            Opc.Ua.Export.UANodeSet nodeSet = Opc.Ua.Export.UANodeSet.Read(stream);

            foreach (string namespaceUri in nodeSet.NamespaceUris)
            {
                SystemContext.NamespaceUris.GetIndexOrAppend(namespaceUri);
            }
            nodeSet.Import(SystemContext, predefinedNodes);

            for (int ii = 0; ii < predefinedNodes.Count; ii++)
            {
                AddPredefinedNode(SystemContext, predefinedNodes[ii]);
            }

            // ensure the reverse refernces exist.
            AddReverseReferences(externalReferences);

        }

        public override void CreateAddressSpace(IDictionary<NodeId, IList<IReference>> externalReferences)
        {
            LoadPredefinedNodes(SystemContext, externalReferences);
        }

    }
}
