using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;

namespace Ragnarok.feeder
{
    interface ITargetDriver
    {
        bool feedAmazonOrders(ICollection<AmazonOrder> orders);
        bool feedAmazonOrder(AmazonOrder order);
        void setAuthentication(Dictionary<String, String> authentication);
        bool orderExistsInTarget(AmazonOrder orderId);
    }
}
