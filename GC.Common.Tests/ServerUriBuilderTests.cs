using GG.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GC.Common.Tests
{
    [TestClass]
    public class ServerUriBuilderTests
    {
        [TestMethod]
        public void ShouldBuildCorrectServerUri()
        {
            var serverUri = ServerUriBuilder.PrepareServerUri("http://test:{0}", "2020").ToString();

            Assert.AreEqual("http://test:2020", serverUri);
        }

        [TestMethod]
        public void ShouldReturnDefaultUriWhenPortIsntGiven()
        {
            var serverUri = ServerUriBuilder.PrepareServerUri("http://test:{0}", "").ToString();

            Assert.AreEqual("http://test:8080", serverUri);
        }
    }
}
