namespace Magnum.ProtocolBuffers.Serialization
{
    public interface IMessageDescriptor<TMessage> :
        IMessageDescriptor where TMessage : class, new()
    {
        void Serialize(CodedOutputStream outputStream, TMessage message);
        new TMessage Deserialize(CodedInputStream inputStream);
    }

    public interface IMessageDescriptor
    {
        void Serialize(CodedOutputStream outputStream, object message);
        object Deserialize(CodedInputStream inputStream);
    }
}