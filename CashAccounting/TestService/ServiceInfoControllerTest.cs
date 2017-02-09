using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CashAccountingSvr.Controller;
using NUnit.Framework;

namespace TestService {
    [TestFixture]
    public class ServiceInfoControllerTest {

        [Test]
        public void ServiceInfoTest() {

            ////ServiceInfoController ctl = new ServiceInfoController();
            ////HttpResponseMessage msg = ctl.ServiceInfo();
            ////Assert.IsTrue(msg.IsSuccessStatusCode);
            ////Task<string> ctRead = msg.Content.ReadAsStringAsync();
            ////string content = ctRead.Result;
            ////Assert.Greater(content.Length, 0);
            Assert.Fail("Need to learn first how to mock and unit test an HttpWebRequest....");

        }

    }
}
