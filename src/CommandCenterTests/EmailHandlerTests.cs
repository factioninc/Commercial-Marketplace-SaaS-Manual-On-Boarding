using CommandCenter;
using CommandCenter.Mail;
using CommandCenter.Marketplace;
using CommandCenter.Models;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommandCenterTests
{
    class EmailHandlerTests
    {
        CommandCenterEMailHelper  _emailHandler;
        
        [OneTimeSetUp]
        public void InitialSetup()
        {
            var optionsMonitor = new Mock<IOptionsMonitor<CommandCenterOptions>>();
            _emailHandler = new CommandCenterEMailHelper(optionsMonitor.Object, null);
        }

        [Test]
        public void BuildStandardDataTable()
        {
            var item = new AzureSubscriptionProvisionModel()
            {
                Location = FactionLocation.SantaClaraCA,
                PlanId = "test plan",
                SubscriptionId = Guid.NewGuid(),
                PurchaserTenantId = Guid.NewGuid(),
                TechnicalContactEmail = "test@test.com",
                TechnicalContactName = "Technical Contact",
                TechnicalContactPhone = "123-456-780",
                
            };
            item.TechnicalDetails.RequestedShareName = "Share1";
            item.TechnicalDetails.SupportCIFS = true;
            item.TechnicalDetails.SupportNFS = true;
            
            var tableString = CommandCenterEMailHelper.BuildTable(item);
            Assert.IsTrue(tableString.Length > 10);
        }

        [Test]
        public void BuildCustomDataTable()
        {
            var item = new AzureSubscriptionProvisionModel()
            {
                Location = FactionLocation.SantaClaraCA,
                PlanId = "test plan",
                SubscriptionId = Guid.NewGuid(),
                PurchaserTenantId = Guid.NewGuid(),
                CustomBundleOptions = new FactionCustomBundleModel()
                {
                    RequestedPerformanceTier = FactionCustomPerformanceTier.Archive,
                    RequestedBandwidth = "10 Gbps",
                    RequestedStorageSize = "500 TB"
                },
                TechnicalContactEmail = "test@test.com",
                TechnicalContactName = "Technical Contact",
                TechnicalContactPhone = "123-456-780",

            };
            item.TechnicalDetails.RequestedShareName = "Share1";
            item.TechnicalDetails.SupportCIFS = true;
            item.TechnicalDetails.SupportNFS = true;

            var tableString = CommandCenterEMailHelper.BuildTable(item);
            Assert.IsTrue(tableString.Length > 10);
        }


    }
}
