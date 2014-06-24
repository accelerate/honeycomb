namespace Honeycomb.Cell
{
    public class Envelope
    {
        public Envelope(CellAddress sender)
        {
            Sender = sender;
        }

        public CellAddress Sender { get; private set; }
    }
}