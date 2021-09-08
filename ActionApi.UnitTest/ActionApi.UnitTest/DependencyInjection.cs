using ActionApi.UnitTest.Helper;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace ActionApi.UnitTest
{

    [TestFixture()]
    public class DependencyInjection
    {
        private static ServiceProvider _serviceProvider;

        [OneTimeSetUp]
        public void Init()
        {
            _serviceProvider ??= InjectionHelper.InitDependency();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void METHOD()
        {
            //Arrange

            //Act

            //Assert
        }

        [TearDown]
        public void CleanUp()
        {
        }

    }
}