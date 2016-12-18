using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;
using Ragnarok.parser;

namespace Ragnarok.feeder.everest
{
    class EverestTargetDriver : ITargetDriver
    {
        public bool feedAmazonOrders(ICollection<Order> orders)
        {
            return false;
        }

        public void setAuthentication(Dictionary<String, String> authentication)
        {

        }

        public bool feedAmazonOrder(ParsingRulesInfo parsingRules, Order order)
        {
            Order everestOrder = convertAmazonOrderToEverestOrder(parsingRules, order);
            return false;
        }

        private Order convertAmazonOrderToEverestOrder(ParsingRulesInfo parsingRulesInfo, Order amazonOrder)
        {
            Order everestOrder = new Order();
            convertAmazonModelToEverestModel(parsingRulesInfo, amazonOrder, everestOrder);
            foreach (OrderItem orderItem in amazonOrder.OrderItems)
            {
                OrderItem everestOrderItem = new OrderItem();
                convertAmazonModelToEverestModel(parsingRulesInfo, orderItem, everestOrderItem);
                everestOrder.addOrderItem(everestOrderItem);
            }

                foreach(ParsingRule parsingRule in parsingRulesInfo.ParsingRules.Values){
                    if (parsingRule.AggregateMappingName == null)
                    {
                        continue;
                    }
                    if (parsingRule.ParsingType == ParsingType.DecimalType)
                    {
                        decimal aggregate = 0;
                        foreach (OrderItem orderItem in everestOrder.OrderItems)
                        {
                            aggregate += orderItem.getDecimalValue(parsingRule.EverestColumnName);
                        }
                        everestOrder.putDecimalValue(parsingRule.AggregateMappingName, aggregate);
                    }
                    if (parsingRule.ParsingType == ParsingType.IntegerType)
                    {
                        int aggregate = 0;
                        foreach (OrderItem orderItem in everestOrder.OrderItems)
                        {
                            aggregate += orderItem.getIntegerValue(parsingRule.EverestColumnName);
                        }
                        everestOrder.putIntegerValue(parsingRule.AggregateMappingName, aggregate);
                    }
            }
            return everestOrder;
        }

        private void convertAmazonModelToEverestModel(ParsingRulesInfo parsingRulesInfo, ExtensibleDataModel amazonModel, ExtensibleDataModel everestModel)
        {
            foreach (ParsingRule rulesInfo in parsingRulesInfo.ParsingRules.Values)
            {
                if (!amazonModel.containsColumnName(rulesInfo.AmazonColumnName))
                {
                    continue;
                }
                if (rulesInfo.ParsingType == ParsingType.StringType)
                {
                    everestModel.putStringValue(rulesInfo.EverestColumnName, amazonModel.getStringValue(rulesInfo.AmazonColumnName));
                }
                else if (rulesInfo.ParsingType == ParsingType.DecimalType)
                {
                    everestModel.putDecimalValue(rulesInfo.EverestColumnName, amazonModel.getDecimalValue(rulesInfo.AmazonColumnName));
                }
                else if (rulesInfo.ParsingType == ParsingType.IntegerType)
                {
                    everestModel.putIntegerValue(rulesInfo.EverestColumnName, amazonModel.getIntegerValue(rulesInfo.AmazonColumnName));
                }
            }
        }

        public bool orderExistsInTarget(Order amazonOrder)
        {
            return false;
        }
    }
}
