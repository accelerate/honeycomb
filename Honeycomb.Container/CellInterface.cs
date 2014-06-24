namespace Honeycomb.Container
{
    using System;
    using Cell;

    internal class CellInterface
    {
        private readonly CellAddress address;
        private readonly AppDomain isolatedDomain;
        private readonly CellHost host;

        public CellInterface(CellAddress address, string assemblyName, string cellTypeFullName)
        {
            this.address = address;
            isolatedDomain = prepareDomain(address);
            host = createHost(isolatedDomain, assemblyName, cellTypeFullName);
        }

        private static AppDomain prepareDomain(CellAddress address)
        {
            var domainSetup = new AppDomainSetup {ApplicationBase = AppDomain.CurrentDomain.BaseDirectory};
            var domain = AppDomain.CreateDomain(address.ToString(), null, domainSetup);
            domain.UnhandledException += isolatedDomainOnUnhandledException;
            return domain;
        }

        private static void isolatedDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs unhandledExceptionEventArgs)
        {
            throw new NotImplementedException();
        }

        private static CellHost createHost(AppDomain domain, string assemblyName, string cellTypeFullName)
        {
            var cellHost = (CellHost) domain.CreateInstanceAndUnwrap(typeof (CellHost).Assembly.FullName, typeof (CellHost).FullName);
            cellHost.HostCell(assemblyName, cellTypeFullName, outboundMessageHandler);
            return cellHost;
        }

        private static void outboundMessageHandler(Envelope envelope)
        {
            throw new NotImplementedException();
        }
    }
}