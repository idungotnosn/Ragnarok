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
    class Mediator
    {
        public void syncAmazonOrders(){

            if (File.Exists("reports/lock"))
            {
                throw new RagnarokConcurrencyException("Cannot perform amazon data sync - the lock file exists in the reports directory.  Another operation is likely running.");
            }

            try { 
                
                using (System.IO.File.Create("reports/lock")) {}

                UserInteraction interaction = UserInteractionFactory.createUserInteraction();

                interaction.setStatus("Initializing...");

                

                IDao dao = DaoFactory.createDao();

                IFeeder feeder = FeederFactory.getFeeder();

                AmazonReportParser reportParser = new AmazonReportParser();

                ReportRetrievalService reportRetrievalService = new ReportRetrievalService();

                interaction.setStatus("Retrieving reports from Amazon...");

                ICollection<ReportInfo> listReportInfo = reportRetrievalService.retrieveListOfReports();

                interaction.showListOfReports(listReportInfo);

                interaction.setStatus("Filtering reports from Amazon already reported in SQL...");

                listReportInfo = dao.filterReportsAlreadySynced(listReportInfo);

                interaction.showListOfReports(listReportInfo);

                interaction.setStatus("Transferring filtered reports to hard disk...");

                reportRetrievalService.transferReportsToHardDisk(listReportInfo);

                interaction.setStatus("Parsing orders from reports...");

                ICollection<AmazonOrder> orders = AmazonReportParser.parseOrderListFromReportsInPath("reports");

                interaction.showListOfOrders(orders);

                interaction.setStatus("Filtering out orders that were already saved in database");

                orders = dao.filterOrdersAlreadySynced(orders);

                interaction.setStatus("Feeding orders into Everest...");

                interaction.setStatus("Updating DB tables...");

                try { 

                    dao.insertReportsToDB(listReportInfo);

                    dao.insertOrdersToDB(orders);

                    feeder.feedAmazonOrders(orders);  // Please be ACIDic with rollbacks!!!

                }
                catch (Exception) { 

                    dao.deleteOrdersFromDB(orders);

                    dao.deleteReportsFromDB(listReportInfo);

                }

                interaction.setStatus("Operation complete.");

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
