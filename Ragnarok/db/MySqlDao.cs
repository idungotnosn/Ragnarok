using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ragnarok.db
{
    class MySqlDao : IDao
    {
        public ICollection<MarketplaceWebService.Model.ReportInfo> filterReportsAlreadySynced(ICollection<MarketplaceWebService.Model.ReportInfo> reports)
        {
            return reports;
        }

        public ICollection<model.AmazonOrder> filterOrdersAlreadySynced(ICollection<model.AmazonOrder> orders)
        {
            return orders;
        }
    }
}
