namespace Honeycomb.Cell
{
    using System;

    public class CellAddress
    {
        public CellAddress(SupervisorAddress supervisor)
        {
            Supervisor = supervisor;
        }

        public CellAddress(SupervisorAddress supervisor, Guid address) : this(supervisor)
        {
            Address = address;
        }

        public SupervisorAddress Supervisor { get; private set; }
        public Guid Address { get; private set; }

        public override string ToString()
        {
            return Supervisor + "/" + Address;
        }
    }

    public class SupervisorAddress
    {
        public SupervisorAddress(ContainerAddress container)
        {
            Container = container;
            Address = Guid.NewGuid();
        }

        public SupervisorAddress(ContainerAddress container, Guid address) : this(container)
        {
            Address = address;
        }

        public ContainerAddress Container { get; private set; }
        public Guid Address { get; private set; }

        public override string ToString()
        {
            return Container + "/" + Address;
        }
    }

    public class ContainerAddress
    {
        public ContainerAddress()
        {
            Address = Guid.NewGuid();
        }

        public ContainerAddress(Guid address)
        {
            Address = address;
        }

        public Guid Address { get; private set; }

        public override string ToString()
        {
            return Address.ToString();
        }
    }
}