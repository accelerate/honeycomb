namespace Example.PingActor
{
    using Honeycomb.Cell;

    public class Pinger : IReceive<PongEvent>
    {
        private int pingCount;

        public Pinger()
        {
            Message.Send(new PingEvent {PingCount = ++pingCount});
        }

        public void Receive(PongEvent message)
        {
            if (pingCount > 10) return;

            Message.Send(new PingEvent {PingCount = ++pingCount});
        }
    }

    public class PingEvent : IMessage
    {
        public int PingCount { get; set; }
    }

    public class PongEvent : IMessage
    {
        public int PongCount { get; set; }
    }
}