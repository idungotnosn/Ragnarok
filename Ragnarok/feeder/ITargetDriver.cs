using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;
using Ragnarok.parser;

namespace Ragnarok.feeder
{
    interface ITargetDriver
    {
        bool feedAmazonOrders(ICollection<Order> orders);
        bool feedAmazonOrder(ParsingRulesInfo parsingRules, Order order);
        void setAuthentication(Dictionary<String, String> authentication);
        bool orderExistsInTarget(Order orderId);
    }
}
