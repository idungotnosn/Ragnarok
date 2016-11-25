using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ragnarok.model;
using MarketplaceWebService.Model;

namespace Ragnarok.db
{
    interface IDao
    {

        ICollection<ReportInfo> filterReportsAlreadySynced(ICollection<ReportInfo> reports);

        void insertReportsToDB(ICollection<ReportInfo> reports, userinteraction.UserInteraction userInteraction);

        ICollection<AmazonOrder> filterOrdersAlreadySynced(ICollection<AmazonOrder> orders);

        void insertOrdersToDB(ICollection<AmazonOrder> orders, userinteraction.UserInteraction userInteraction);

        void deleteOrdersFromDB(ICollection<AmazonOrder> reports);

        void deleteReportsFromDB(ICollection<ReportInfo> orders);

    }
}
