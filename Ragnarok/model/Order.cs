using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.model
{
    public class Order : ExtensibleDataModel
    {
        private List<OrderItem> orderItems;

        private String identifier;

        public List<OrderItem> OrderItems
        {
            get
            {
                return this.orderItems;
            }
        }

        public Order()  : base(){
            this.orderItems = new List<OrderItem>();
        }

        public String Identifier
            {
                get { return identifier; }
                set { identifier = value; }
            }

        public void addOrderItem(OrderItem item)
        {
            this.orderItems.Add(item);
        }

        public ICollection<OrderItem> getOrderItems()
        {
            return this.orderItems;
        }

        public decimal getDecimalOrderItemAggregate(String key)
        {
            decimal result = 0.0m;
            foreach(OrderItem item in this.orderItems){
                result += item.getDecimalValue(key);
            }
            return result;
        }

        public int getIntegerOrderItemAggregate(String key)
        {
            int result = 0;
            foreach (OrderItem item in this.orderItems)
            {
                result += item.getIntegerValue(key);
            }
            return result;
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());
            foreach(OrderItem item in this.orderItems){
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }
        
    }
}
