namespace Example.PongActor
{
    using Honeycomb.Cell;

    public class Ponger : IReceive<PingEvent>
    {
        private int pongCount;

        public void Receive(PingEvent message)
        {
            message.Reply(new PongEvent {PongCount = ++pongCount});
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