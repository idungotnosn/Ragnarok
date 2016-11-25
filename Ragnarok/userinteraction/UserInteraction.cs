using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebService.Model;
using Ragnarok.model;

namespace Ragnarok.userinteraction
{
    public interface UserInteraction
    {
        void setStatus(String message);

        void showListOfReports(ICollection<ReportInfo> reports);

        void showListOfOrders(ICollection<AmazonOrder> orders);

        void showError(String errorMessage);
    }
}
