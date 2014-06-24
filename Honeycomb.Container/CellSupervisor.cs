namespace Honeycomb.Container
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Cell;

    public class CellSupervisor
    {
        private readonly SupervisorAddress address;
        private readonly string assemblyName;
        private readonly string cellTypeFullName;
        private readonly string[] receivers;
        private CellInterface[] cellInterfaces;

        static CellSupervisor()
        {
            AppDomain.CurrentDomain.ReflectionOnlyAssemblyResolve += onReflectionOnlyAssemblyResolve;
        }

        public CellSupervisor(ContainerAddress containerAddress, string assemblyName, string cellTypeFullName, Action<string[]> publishReceivers)
        {
            address = new SupervisorAddress(containerAddress);
            this.assemblyName = assemblyName;
            this.cellTypeFullName = cellTypeFullName;
            receivers = findReceivers(assemblyName, cellTypeFullName);
            publishReceivers(receivers);
        }

        private static Assembly onReflectionOnlyAssemblyResolve(object sender, ResolveEventArgs args)
        {
            return Assembly.ReflectionOnlyLoad(args.Name);
        }

        public void Start(int initialThreadCount)
        {
            cellInterfaces = instantiateActors(address, assemblyName, cellTypeFullName, initialThreadCount);
        }

        private static string[] findReceivers(string assemblyName, string cellTypeFullName)
        {
            var domainSetup = new AppDomainSetup {ApplicationBase = AppDomain.CurrentDomain.BaseDirectory};
            AppDomain domain = AppDomain.CreateDomain(Guid.NewGuid().ToString(), null, domainSetup);
            domain.ReflectionOnlyAssemblyResolve += onReflectionOnlyAssemblyResolve;
            try
            {
                var reflector = (CellReflector) domain.CreateInstanceAndUnwrap(typeof (CellReflector).Assembly.FullName, typeof (CellReflector).FullName);
                return reflector.GetReceivers(assemblyName, cellTypeFullName);
            }
            finally
            {
                AppDomain.Unload(domain);
            }
        }

        private static CellInterface[] instantiateActors(SupervisorAddress supervisorAddress, string assemblyName, string cellTypeFullName,
                                                         int initialThreadCount)
        {
            return Enumerable.Range(1, initialThreadCount)
                             .Select(i => new CellInterface(new CellAddress(supervisorAddress), assemblyName, cellTypeFullName))
                             .ToArray();
        }

        public void Scale(int threadCount)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}