using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;

namespace Ragnarok.feeder.everest
{
    class EverestTargetDriver : ITargetDriver
    {
        public bool feedAmazonOrders(ICollection<AmazonOrder> orders)
        {
            return false;
        }

        public void setAuthentication(Dictionary<String, String> authentication)
        {

        }

        public bool feedAmazonOrder(AmazonOrder order)
        {
            return false;
        }

        public bool orderExistsInTarget(AmazonOrder amazonOrder)
        {
            return false;
        }
    }
}
