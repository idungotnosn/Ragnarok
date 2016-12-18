using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.userinteraction;
using Ragnarok.feeder;
using Ragnarok.amazonhttp;
using Ragnarok.model;
using MarketplaceWebService.Model;

namespace Ragnarok.mediator
{
    public class NoSqlMediator
    {
        public void syncAmazonOrders(UserInteraction interaction)
        {
            interaction.setStatus("Initializing...");

            ITargetDriver targetDriver = ITargetDriverFactory.getFeeder();

            AmazonReportParser reportParser = new AmazonReportParser();

            ReportRetrievalService reportRetrievalService = new ReportRetrievalService();

            interaction.setStatus("Retrieving reports from Amazon...");

            ICollection<ReportInfo> listReportInfo = reportRetrievalService.retrieveListOfReports();

            interaction.showListOfReports(listReportInfo);

            interaction.setStatus("Filtering reports from Amazon already reported in SQL...");

            if (listReportInfo.Count == 0)
            {
                 interaction.setStatus("The operation is complete - there are no reports to publish to Everest");
                 return;
            }

            interaction.showListOfReports(listReportInfo);

            interaction.setStatus("Transferring filtered reports to hard disk...");

            reportRetrievalService.transferReportsToHardDisk(listReportInfo);

            interaction.setStatus("Parsing orders from reports...");

            ICollection<Order> orders = AmazonReportParser.parseOrderListFromReportsInPath("reports");

            interaction.showListOfOrders(orders);

            foreach (Order order in orders)
            {
                interaction.setStatus("Now reviewing order with ID " + order.Identifier);
                if (targetDriver.orderExistsInTarget(order))
                {
                    interaction.setStatus("Order with ID " + order.Identifier + " already exists in target, skipping.");
                    continue;
                }
                foreach (OrderItem orderItem in order.getOrderItems()) {
                     String customUrl = orderItem.getStringValue("customized-page");
                     if (!String.IsNullOrWhiteSpace(customUrl))
                     {
                         order.putStringValue("customized-page", interaction.getCustomMessageFromUrl(customUrl));
                     }
                 }
                if (!interaction.confirmOrder(order))
                {
                    interaction.setStatus("Order was cancelled by user, not exporting order with ID " + order.Identifier);
                    continue;
                }
                interaction.setStatus("Now sending order with ID " + order.Identifier + " to Amazon...");
                if (!targetDriver.feedAmazonOrder(AmazonReportParser.getParsingRulesInfo(), order))
                {
                    interaction.showError("Did not successfully send order with ID " + order.Identifier + " to Everest!");
                }
              }

            }
        }


    }

