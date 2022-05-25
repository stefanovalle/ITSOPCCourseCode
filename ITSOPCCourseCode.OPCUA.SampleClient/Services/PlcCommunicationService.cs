using ITSOPCCourseCode.OPCUA.SampleClient.DTO;
using Opc.Ua;
using Opc.Ua.Client;
using Opc.Ua.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITSOPCCourseCode.OPCUA.SampleClient.Services
{
    public class PlcCommunicationService
    {
        private Session Session;
        private bool IsSessionStarted = false;
        public event EventHandler<NodeValueChangedNotification> NodeValueChanged;

        public PlcCommunicationService()
        { }

        public async Task StartAsync(string endpointUrl)
        {
            if (!IsSessionStarted)
            {
                ApplicationInstance application = new ApplicationInstance
                {
                    ApplicationName = "OPC-UA Sample",
                    ApplicationType = ApplicationType.Client,
                    ConfigSectionName = "OpcUA"
                };

                var config = new ApplicationConfiguration()
                {
                    ApplicationName = "UADEMO.prosysopc.com:OPCUA:SimulationServer",
                    ApplicationUri = "urn:UADEMO.prosysopc.com:OPCUA:SimulationServer",
                    ApplicationType = ApplicationType.Client,
                    SecurityConfiguration = new SecurityConfiguration
                    {
                        ApplicationCertificate = new CertificateIdentifier { StoreType = @"X509Store", StorePath = @"CurrentUser\UA_MachineDefault", SubjectName = "DC=UADEMO.prosysopc.com, O=Prosys OPC, CN=SimulationServer@UADEMO" },
                        TrustedIssuerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Certificate Authorities" },
                        TrustedPeerCertificates = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\UA Applications" },
                        RejectedCertificateStore = new CertificateTrustList { StoreType = @"Directory", StorePath = @"%CommonApplicationData%\OPC Foundation\CertificateStores\RejectedCertificates" },
                        AutoAcceptUntrustedCertificates = true,
                        AddAppCertToTrustedStore = true
                    },
                    TransportConfigurations = new TransportConfigurationCollection(),
                    TransportQuotas = new TransportQuotas { OperationTimeout = 15000 },
                    ClientConfiguration = new ClientConfiguration { DefaultSessionTimeout = 60000 },
                    TraceConfiguration = new TraceConfiguration()
                };
                await config.Validate(ApplicationType.Client).ConfigureAwait(false);

                // Handle application certificate
                config.CertificateValidator.CertificateValidation += (_, e) => e.Accept = true;

                try
                {
                    var selectedEndpoint = CoreClientUtils.SelectEndpoint(endpointUrl, false, 60000); // 15s timeout
                    var endpointConfiguration = EndpointConfiguration.Create(config);
                    var endpoint = new ConfiguredEndpoint(null, selectedEndpoint, endpointConfiguration);
                    Session = await Session.Create(
                        config,
                        endpoint,
                        false,
                        application.ApplicationName,
                        60000,
                        new UserIdentity(new AnonymousIdentityToken()),
                        null
                    ).ConfigureAwait(false);
                    IsSessionStarted = true;
                }
                catch (Exception e)
                {
                    Console.Error.Write(e);
                }
            }
        }

        public Task StopAsync()
        {
            if (IsSessionStarted)
            {
                Session?.Close();
                IsSessionStarted = false;
            }

            return Task.CompletedTask;
        }

        public ReferenceDescriptionCollection BrowseNode(string nodeId)
        {
            byte[] cp, revisedCp;
            ReferenceDescriptionCollection readed, nextReaded;
            Session.Browse(
                null,
                null,
                new NodeId(nodeId),
                0u,
                BrowseDirection.Forward,
                ReferenceTypeIds.HierarchicalReferences,
                true,
                (uint)NodeClass.Variable | (uint)NodeClass.Object | (uint)NodeClass.Method,
                out cp,
                out readed);

            while (cp != null)
            {
                Session.BrowseNext(null, false, cp, out revisedCp, out nextReaded);
                readed.AddRange(nextReaded);
                cp = revisedCp;
            }
            return readed;
        }

        public void WriteToNode<T>(NodeWritingRequest<T> writingRequest) where T : IConvertible
        {
            StatusCodeCollection statusCodes;
            DiagnosticInfoCollection diagnosticInfos;

            Session.Write(
                null,
                new WriteValueCollection(
                    new List<WriteValue> {
                        new WriteValue()
                        {
                            NodeId = new NodeId(writingRequest.NodeId),
                            Value = new DataValue(new Variant(writingRequest.Value)),
                            AttributeId = Attributes.Value
                        }
                    }),
                out statusCodes,
                out diagnosticInfos
            );

            if (statusCodes.Any(StatusCode.IsBad))
                throw new Exception("Unable to write"); // TODO BETTER EXCEPTION TO UNDERSTAND REASON WHY WRITING COMMAND IS FAILED
        }

        public object ReadNodeValue(string nodeId)
        {
            return Session.ReadValue(nodeId).Value;
        }
        public Node ReadNode(string nodeId)
        {
            return Session.ReadNode(nodeId);
        }
        public void SubscribeToNodeChanges(IEnumerable<string> nodeIds, int updateDelay)
        {
            var subscription = new Subscription(Session.DefaultSubscription) { PublishingInterval = updateDelay };

            var items = nodeIds.Select(x => new MonitoredItem { StartNodeId = x }).ToList();

            foreach (var item in items)
            {
                item.Notification += OnChange;
            }

            subscription.AddItems(items);

            Session.AddSubscription(subscription);

            subscription.Create();
        }

        private void OnChange(MonitoredItem item, MonitoredItemNotificationEventArgs e)
        {
            foreach (DataValue value in item.DequeueValues())
            {
                var v = value.Value;

                NodeValueChanged(this, new NodeValueChangedNotification(item.StartNodeId.ToString(), v));
            }
        }

    }
}
