using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebService.Model;
using Ragnarok.model;

namespace Ragnarok.userinteraction
{
    class CommandLineInteraction : UserInteraction
    {
        public void setStatus(String message)
        {
            Console.WriteLine(message);

        }

        public void showListOfReports(ICollection<ReportInfo> reports)
        {
            Console.WriteLine("These are the reports that have not yet been filtered out:");
            Console.WriteLine("ReportID\tReportType");
            foreach (ReportInfo report in reports)
            {
                Console.WriteLine(report.ReportId+"\t"+report.ReportType);
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        public void showListOfOrders(ICollection<AmazonOrder> orders)
        {
            Console.WriteLine("These are the amazon orders that have not yet been filtered out:");
            foreach(AmazonOrder order in orders){
                Console.WriteLine(order.ToString());
            }
            Console.WriteLine("Press enter to continue.");
            Console.ReadLine();
        }

        public void showError(String errorMessage)
        {
            Console.WriteLine("An error occurred:" + errorMessage);
            Console.ReadLine();
        }
    }
}
