using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using APIMLKeys.Models;
using APIMLKeys.Controllers;
using System.Web;

namespace APIMLKeys.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Valid()
        {

            string clientID = "4598330715002431";
            string clientSecret = "sjOvDNxSseLtQ2XUgImnzEPJth5g1yHX";

            var webRequest = WebRequest.CreateHttp("http://localhost:52972/api/Validate/?clientID=" + clientID + "&clientSecret=" + clientSecret);
            webRequest.Method = "GET";

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var WebStatusCode = webResponse.StatusCode;


            Assert.AreEqual(200, (int)WebStatusCode);
        }


        [TestMethod]
        public void Invalid()
        {

            string clientID = "123456";
            string clientSecret = "ABCDEFG";

            var webRequest = WebRequest.CreateHttp("http://localhost:52972/api/Validate/?clientID=" + clientID + "&clientSecret=" + clientSecret);
            webRequest.Method = "GET";

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var WebStatusCode = webResponse.StatusCode;


            Assert.AreEqual(203, (int)WebStatusCode);
        }
        [TestMethod]
        public void empty()
        {

            string clientID = "";
            string clientSecret = "";

            var webRequest = WebRequest.CreateHttp("http://localhost:52972/api/Validate/?clientID=" + clientID + "&clientSecret=" + clientSecret);
            webRequest.Method = "GET";

            var webResponse = (HttpWebResponse)webRequest.GetResponse();
            var WebStatusCode = webResponse.StatusCode;


            Assert.AreEqual(206, (int)WebStatusCode);
        }
    }
}
