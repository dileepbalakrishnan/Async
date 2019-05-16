using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestingAsyncNetworkAccess
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var httpRequest = WebRequest.Create("http://www.ndtv.com");
            var ar = httpRequest.BeginGetResponse(ResponseAvailable, httpRequest);
            ar.AsyncWaitHandle.WaitOne();
        }

        private void ResponseAvailable(IAsyncResult ar)
        {
            var request = ar.AsyncState as HttpWebRequest;
            var response = request.EndGetResponse(ar);
            using (var sr = new StreamReader(response.GetResponseStream()))
            {
                var data = sr.ReadToEnd();
                Assert.IsNotNull(data);
            }
        }
    }
}
