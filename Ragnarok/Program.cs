using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ragnarok.model;
using Ragnarok.amazonhttp;

namespace Ragnarok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            Program.testAmazonReportRetrieval();
            Console.ReadLine();
        }

        public static void testAmazonReportRetrieval()
        {
            ReportRetrievalService s = new ReportRetrievalService();
            s.getReport();
        }

        public static void testAmazonReportParser()
        {
            using (FileStream fs = File.OpenRead("testdata/itemprice.txt"))
            {
                ICollection<AmazonOrder> amazonOrders = AmazonReportParser.parseOrderListFromReport(fs);
                using(IEnumerator<AmazonOrder> enumerator = amazonOrders.GetEnumerator()){
                    while (enumerator.MoveNext())
                    {
                        AmazonOrder current = enumerator.Current;
                        Console.WriteLine(current.getDecimalOrderItemAggregate("item-price"));
                    }
                }
            }

        }

    }
}
