using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;
using MarketplaceWebService.Model;

namespace Ragnarok.db
{
    public class DummyDao : IDao, IDisposable
    {
        public ICollection<MarketplaceWebService.Model.ReportInfo> filterReportsAlreadySynced(ICollection<MarketplaceWebService.Model.ReportInfo> reports)
        {
            return reports;
        }

        public ICollection<model.Order> filterOrdersAlreadySynced(ICollection<model.Order> orders)
        {
            return orders;
        }

        public void insertReportsToDB(ICollection<ReportInfo> reports, userinteraction.UserInteraction userInteraction)
        {

        }


        public void insertOrdersToDB(ICollection<Order> orders, userinteraction.UserInteraction userInteraction)
        {

        }

        public void deleteOrdersFromDB(ICollection<Order> reports)
        {

        }

        public void deleteReportsFromDB(ICollection<ReportInfo> orders)
        {

        }

        public void Dispose()
        {

        }
    }
}
