using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using RagnarokException;
using Ragnarok.amazonhttp;
using Ragnarok.db;
using MarketplaceWebService.Model;


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
            IDao dao = DaoFactory.createDao();

            ReportRetrievalService reportRetrievalService = new ReportRetrievalService();

            ICollection<ReportInfo> listReportInfo = reportRetrievalService.retrieveListOfReports();

            listReportInfo = dao.filterReportsAlreadySynced(listReportInfo);

            reportRetrievalService.transferReportsToHardDisk(listReportInfo);
                
                // 5 - Parse all of those reports with AmazonReportParser, get list of AmazonOrders

                // 6 - Find all amazon orders in that list that were already submitted to Everest through SQLDAO

                // 7 - Remove amazon orders found in step 6 from the amazon order list

                // 8 - Submit all amazon orders that were not removed in step 7 to Everest

                // 9 - Delete all files in report xml directory
            }
            finally
            {
                //deleteAll("reports");
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
