namespace ITSOPCCourseCode.OPCUA.SampleClient.DTO
{
    public class NodeValueChangedNotification
    {
        public string NodeId { get; }
        public object Value { get; }

        public NodeValueChangedNotification(
            string nodeId,
            object value)
        {
            NodeId = nodeId;
            Value = value;
        }
    }
}
