using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace AppsGallery1
{
    [Binding]
    public class AppsGallery1Steps
    {
        private string _theUrl;
         private string _theResponse; 


        [Given(@"the alteryx is running at ""(.*)""")]
        public void GivenTheAlteryxIsRunningAt(string p0)
        {
            _theUrl = p0;
        }
        
        [When(@"I Invoke the GET at ""(.*)""")]
        public void WhenIInvokeTheGETAt(string p0)
        {
            string fullUrl = _theUrl + "/" + p0;
            WebRequest request = WebRequest.Create(fullUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string responseFromServer = reader.ReadToEnd();
            Console.WriteLine(responseFromServer);
            _theResponse = responseFromServer;
        }
        
        [Then(@"I see at least (.*) active apps")]
        public void ThenISeeAtLeastActiveApps(int expectedapps)
        {
            var dict = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Dictionary<string, dynamic>>(_theResponse);
            int count = dict["recordCount"];
            Console.Write("No of Apps present in the gallery :" + count);
            Assert.That(count, Is.AtLeast(expectedapps));
        }
    }
}
