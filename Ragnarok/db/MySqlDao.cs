using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebService.Model;
using Ragnarok.model;

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

        public void insertReportsToDB(ICollection<ReportInfo> reports)
        {

        }


        public void insertOrdersToDB(ICollection<AmazonOrder> orders)
        {

        }

        public void deleteOrdersFromDB(ICollection<AmazonOrder> reports)
        {

        }

        public void deleteReportsFromDB(ICollection<ReportInfo> orders)
        {

        }


    }
}
