using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ragnarok.model;
using RagnarokException;
using Ragnarok.amazonhttp;
using Ragnarok.mediator;
using Ragnarok.userinteraction;
using System.Windows.Forms;

namespace Ragnarok
{
    class Program
    {
        static void Main(string[] args)
        {
            Application.Run(new GUIForm(new NoSqlMediator()));  
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
                        
         * (current.getDecimalOrderItemAggregate("item-price"));
                    }
                }
            }

        }
         */

    }
}
