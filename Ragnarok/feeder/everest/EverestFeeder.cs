using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;

namespace Ragnarok.feeder.everest
{
    class EverestFeeder : IFeeder
    {
        public bool feedAmazonOrders(List<AmazonOrder> orders)
        {
            return false;
        }

        public void setAuthentication(Dictionary<String, String> authentication)
        {

        }
    }
}
