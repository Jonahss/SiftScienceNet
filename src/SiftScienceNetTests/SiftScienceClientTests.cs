using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SiftScienceNet.Labels;
using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SiftScienceNet.Tests
{
    [TestClass()]
    public class SiftScienceClientTests
    {
        [TestMethod()]
        public void LabelTest()
        {
           



            var abuse = new ExpandoObject() as IDictionary<string, Object>;
            abuse.Add("$abuse_type", AbuseType.Payment);

  
           
            var moo = JsonConvert.SerializeObject(abuse, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            // json.Add("$abuse_type", abuse);


            var baseObject = new ExpandoObject() as IDictionary<string, Object>;
            baseObject.Add("$abuse_type", AbuseType.Content);
           

            baseObject.Add("$api_key", "mahmee");
            baseObject.Add("$is_bad", true);

            var json = JsonConvert.SerializeObject(baseObject, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });


            Assert.AreEqual("payment_abuse", json);
        }

        [TestMethod]
        public void FinalLabelTest()
        {
            var sift = new SiftScienceClient("aa494d01a7c10fc3");
            var response = sift.Label("123", true, AbuseType.Account);

            response.Wait(); // lets not bring async into this project until we've really cleaned it up

            if (!response.Result.Success)
            {
                Assert.Fail();
            }
        }
    }
}