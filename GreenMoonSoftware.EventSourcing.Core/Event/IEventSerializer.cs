namespace GreenMoonSoftware.EventSourcing.Core.Event
{
    public interface IEventSerializer
    {
        byte[] Serialize(IEvent e);
        IEvent Deserialize(string eventType, byte[] stream);
    }
}