using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ragnarok.model;
using RagnarokException;
using Ragnarok.amazonhttp;

namespace Ragnarok
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Hello world!");
            Mediator mediator = new Mediator();
            try { 
                mediator.syncAmazonOrders();
            }
            catch (RagnarokConcurrencyException e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine("Stuff happened");
            Console.ReadLine();
             
        }

        /*
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
         */

    }
}
