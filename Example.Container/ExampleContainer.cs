namespace Example.Container
{
    using System;
    using System.IO;
    using Honeycomb.Cell;
    using Honeycomb.Container;
    using NUnit.Framework;

    [TestFixture]
    public class ExampleContainer
    {
        [Test]
        public void CreateTwoSupervisorsAndHaveThenPingPongEachOther()
        {
            //Note that there is no compile time reference to the assemblies containing the Ping and Pong cells.
            //In this example we copy the assemblies locally.
            File.Copy("../../../Example.PingActor/bin/Debug/Example.PingActor.dll", "./Example.PingActor.dll", true);
            File.Copy("../../../Example.PongActor/bin/Debug/Example.PongActor.dll", "./Example.PongActor.dll", true);

            var pingSuper = new CellSupervisor(new ContainerAddress(Guid.NewGuid()), "Example.PingActor", "Example.PingActor.Pinger", strings => { });
            var pongSuper = new CellSupervisor(new ContainerAddress(Guid.NewGuid()), "Example.PongActor", "Example.PongActor.Ponger", strings => { });

            pingSuper.Start(1);
            pongSuper.Start(1);

            //Note these is no message bus in this example, just a test fake.
        }
    }
}