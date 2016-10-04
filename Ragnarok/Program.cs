using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Ragnarok
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello world!");
            Console.ReadLine();
            Program.testAmazonReportParser();
        }

        public static void testAmazonReportParser()
        {
            using (FileStream fs = File.OpenRead("testdata/reportexample.txt"))
            {
                AmazonReportParser.parseOrders(fs);
            }

        }

    }
}
