using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;

namespace Ragnarok.feeder
{
    interface IFeeder
    {
        bool feedAmazonOrders(List<AmazonOrder> orders);
        void setAuthentication(Dictionary<String, String> authentication);
    }
}
