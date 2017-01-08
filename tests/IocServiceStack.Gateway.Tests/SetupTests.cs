namespace IocServiceStack.Gateway.Tests
{
    using NUnit.Framework;
    using IocServiceStack;
    using Contracts;
    using Services;

    [SetUpFixture]
    public class SetupTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
            //Configure method must be invoked, even though if there's no auto mapping required.
            var configRef = IocServiceProvider.Configure(config => { config.Services(opt => { }); });

            configRef.GetServiceFactory().Add<ICustomer, CustomerService>();

        }
    }
}
