namespace Honeycomb.Cell
{
    using System;

    public class CellHost : MarshalByRefObject
    {
        private Type cellType;
        private dynamic cell;
        private Action<Envelope> sendMessage;

        public void HostCell(string assemblyName, string cellTypeFullName, Action<Envelope> outboundMessageHandler)
        {
            sendMessage = outboundMessageHandler;
            referMessagesToHost();
            cellType = loadCell(assemblyName, cellTypeFullName);
            cell = activateCell();
        }

        private void referMessagesToHost()
        {
            Message.Host = this;
        }

        private Type loadCell(string assemblyName, string cellTypeFullName)
        {
            return AppDomain.CurrentDomain.Load(assemblyName).GetType(cellTypeFullName);
        }

        private dynamic activateCell()
        {
            return Activator.CreateInstance(cellType);
        }

        public void ForwardMessage(IMessage message)
        {
//            sendMessage(message);
        }

        public Envelope EnvelopeFor(IMessage received)
        {
            throw new NotImplementedException();
        }
    }
}