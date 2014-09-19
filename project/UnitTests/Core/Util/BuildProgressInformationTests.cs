using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThoughtWorks.CruiseControl.Core.Util;

namespace ThoughtWorks.CruiseControl.UnitTests.Core.Util
{
    [TestFixture]
    public class BuildProgressInformationTests
    {
        [Test]
        [TestCase("Here is some simple text.")]
        [TestCase("Here is some \"slightly\" more complex text.")]
        [TestCase("<this <ought 'to' \"be\" '><<![CDATA[ a real chall'enge>>>.")]
        public void ProducesWellFormedXml(string input)
        {
            var bpi = new BuildProgressInformation("artifact","project");

            bpi.SignalStartRunTask(input);

            var result = bpi.GetBuildProgressInformation();

            Console.WriteLine(result);

            Assert.DoesNotThrow(
                () => XElement.Parse(result),
                "BuildProgressInformation should always be well formed XML.");

            var xml = XElement.Parse(result);

            Assert.AreEqual(
                input, 
                xml.Element("Item").Attribute("Data").Value,
                "The value of Data/Item/@Data should be the same as the input.");
        }
    }
}
