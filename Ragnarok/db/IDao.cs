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

        ICollection<Order> filterOrdersAlreadySynced(ICollection<Order> orders);

        void insertOrdersToDB(ICollection<Order> orders, userinteraction.UserInteraction userInteraction);

        void deleteOrdersFromDB(ICollection<Order> reports);

        void deleteReportsFromDB(ICollection<ReportInfo> orders);

    }
}
