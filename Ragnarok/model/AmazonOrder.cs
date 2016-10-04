using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.model
{
    class AmazonOrder : ExtensibleDataModel
    {
        private List<AmazonOrderItem> orderItems;

        public AmazonOrder()  : base(){
            this.orderItems = new List<AmazonOrderItem>();
        }

        public void addOrderItem(AmazonOrderItem item)
        {
            this.orderItems.Add(item);
        }

        public decimal getDecimalOrderItemAggregate(String key)
        {
            decimal result = 0.0m;
            foreach(AmazonOrderItem item in this.orderItems){
                result += item.getDecimalValue(key);
            }
            return result;
        }

        public int getIntegerOrderItemAggregate(String key)
        {
            int result = 0;
            foreach (AmazonOrderItem item in this.orderItems)
            {
                result += item.getIntegerValue(key);
            }
            return result;
        }
        
    }
}
