using CommandCenter.Models;
using CommandCenter.Utilities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCenterTests
{
    class ExtensionTests
    {
        [Test]
        public void GetEnumDataAttributeName()
        {
            var value1 = FactionLocation.SantaClaraCA;
            var value2 = FactionLocation.NorthernVA;

            var name1 = value1.GetDisplayName();
            var name2 = value2.GetDisplayName();

            Assert.AreEqual("Santa Clara, CA", name1);
            Assert.AreEqual("Northern VA", name2);

        }

        [Test]
        public void GetPropertyDisplayName()
        {
            var item = new AzureSubscriptionProvisionModel()
            {
                Location = FactionLocation.SantaClaraCA
            };

            var displayName1 = item.GetDisplayName(nameof(item.Location));
            var displayName2 = item.GetDisplayName(nameof(item.TechnicalContactEmail));

            Assert.AreEqual("Location", displayName1);
            Assert.AreEqual("Technical contact email", displayName2);
        }
    }
}
