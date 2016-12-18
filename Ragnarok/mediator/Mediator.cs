using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RagnarokException;
using Ragnarok.amazonhttp;
using Ragnarok.db;
using Ragnarok.model;
using MarketplaceWebService.Model;
using Ragnarok.feeder;
using Ragnarok.userinteraction;


namespace Ragnarok
{
    public class Mediator
    {
        public void syncAmazonOrders(){

            if (File.Exists("reports/lock"))
            {
                throw new RagnarokConcurrencyException("Cannot perform amazon data sync - the lock file exists in the reports directory.  Another operation is likely running.");
            }

            try { 
                
                using (System.IO.File.Create("reports/lock")) {}

                using (IDisposable disposableDao = DaoFactory.createDao())
                {
                    IDao dao = (IDao)disposableDao;

                    UserInteraction interaction = null;

                    interaction.setStatus("Initializing...");

                    ITargetDriver feeder = ITargetDriverFactory.getFeeder();

                    AmazonReportParser reportParser = new AmazonReportParser();

                    ReportRetrievalService reportRetrievalService = new ReportRetrievalService();

                    interaction.setStatus("Retrieving reports from Amazon...");

                    ICollection<ReportInfo> listReportInfo = reportRetrievalService.retrieveListOfReports();

                    interaction.showListOfReports(listReportInfo);

                    interaction.setStatus("Filtering reports from Amazon already reported in SQL...");

                    listReportInfo = dao.filterReportsAlreadySynced(listReportInfo);

                    if (listReportInfo.Count == 0)
                    {
                        interaction.setStatus("The operation is complete - there are no new reports to publish to Everest");
                        return;
                    }

                    interaction.showListOfReports(listReportInfo);

                    interaction.setStatus("Transferring filtered reports to hard disk...");

                    reportRetrievalService.transferReportsToHardDisk(listReportInfo);

                    interaction.setStatus("Parsing orders from reports...");

                    ICollection<Order> orders = AmazonReportParser.parseOrderListFromReportsInPath("reports");

                    interaction.showListOfOrders(orders);

                    interaction.setStatus("Filtering out orders that were already saved in database");

                    orders = dao.filterOrdersAlreadySynced(orders);

                    if (orders.Count == 0)
                    {
                        interaction.setStatus("The operation is complete - there are no new orders to push to Everest.");
                        return;
                    }

                    interaction.setStatus("Feeding orders into Everest...");

                    interaction.setStatus("Updating DB tables...");

                    try
                    {

                        dao.insertReportsToDB(listReportInfo, interaction);

                        dao.insertOrdersToDB(orders, interaction);

                        feeder.feedAmazonOrders(orders);  // Please be ACIDic with rollbacks!!!

                    }
                    catch (Exception e)
                    {

                        interaction.setStatus("ERROR:  An exception occurred while feeding the data to MySQL/Everest.  The process will now roll back.");

                        dao.deleteOrdersFromDB(orders);

                        dao.deleteReportsFromDB(listReportInfo);

                    }

                    interaction.setStatus("Operation complete.");
                }
            }
            finally
            {
                deleteAll("reports");
            }
        }

        private void deleteAll(String path)
        {
            System.IO.DirectoryInfo di = new DirectoryInfo("reports");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }

    }
}
