using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Honeycomb.Cell
{
    public interface IMessage
    {
    }

    public static class Message
    {
        internal static CellHost Host { get; set; }

        public static Envelope Envelope(this IMessage received)
        {
            return Host.EnvelopeFor(received);
        }

        public static void Reply(this IMessage received, IMessage replyMessage)
        {
            throw new NotImplementedException();
        }

        public static void Send(IMessage message)
        {
            Host.ForwardMessage(message);
        }
    }
}
