using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using System.Xml;
using RagnarokException;

namespace Ragnarok.amazonhttp
{
    class ReportRetrievalService
    {

        private Authentication auth;
        private MarketplaceWebServiceConfig config;
        private MarketplaceWebServiceClient serviceClient;

        public ReportRetrievalService()
        {
            this.auth = readAuthenticationFromDisk();

            string applicationName = "Ragnarok";
            string applicationVersion = "0.1";

            config = new MarketplaceWebServiceConfig();
            this.config.ServiceURL = auth.ServiceUrl;

            this.config.SetUserAgentHeader(
                applicationName,
                applicationVersion,
                "C#"
            );

            serviceClient = new MarketplaceWebServiceClient(
                auth.AccessKeyId,
                auth.SecretAccessKey,
                this.config
            );
        }

        public ICollection<ReportInfo> transferReportsToHardDisk(ICollection<ReportInfo> reportIds)
        {
            List<ReportInfo> reportsOnDisk = new List<ReportInfo>();
            foreach (ReportInfo current in reportIds)
            {
                if (!current.IsSetReportId())
                {
                    throw new AmazonWebException("The reportID was not set on an object passed to the method transferReportsToHardDisk");
                }
                GetReportRequest request = new GetReportRequest();
                request.Merchant = auth.MerchantId;
                request.ReportId = current.ReportId;
                using (request.Report = File.Open("reports/"+current.ReportId + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite)) { 
                    try
                    {
                        GetReportResponse response = serviceClient.GetReport(request);
                        if (response.IsSetGetReportResult())
                        {
                            reportsOnDisk.Add(current);
                        }
                        else
                        {
                            throw new AmazonWebException("No report result was available when attempting to get report with ID: " + request.ReportId);
                        }
                    }
                    catch (MarketplaceWebServiceException ex)
                    {
                        throw new AmazonWebException("A MarketplaceWebServiceException occurred while getting report: \nError Code: " + ex.ErrorCode + "\nMessage: " + ex.Message, ex);
                    }
                }
            }
            return reportsOnDisk;
        }

        public ICollection<ReportInfo> retrieveListOfReports()
        {
            try
            {
                GetReportListRequest request = new GetReportListRequest();
                request.Merchant = auth.MerchantId;
                GetReportListResponse response = serviceClient.GetReportList(request);
                if (response.IsSetGetReportListResult())
                {
                    return response.GetReportListResult.ReportInfo.Where
                        (reportInfo => reportInfo.IsSetReportType() 
                            && reportInfo.ReportType.Equals("_GET_FLAT_FILE_ORDERS_DATA_")).ToList<ReportInfo>();
                }
                else
                {
                    throw new AmazonWebException("The get report list result was not set from the Amazon server.");
                }
            }
            catch (MarketplaceWebServiceException ex)
            {
                throw new AmazonWebException("A MarketplaceWebServiceException occurred.  Error code: " + ex.ErrorCode + "\nMessage: " + ex.Message);
            }
        }

        private static Authentication readAuthenticationFromDisk()
        {
            Authentication auth = new Authentication();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(File.ReadAllText("auth/amazon.xml"));
            foreach (XmlNode row in xmlDoc.SelectNodes("//authentications"))
            {
                auth.AccessKeyId = row.SelectSingleNode("access-key-id").InnerText.Trim();
                auth.SecretAccessKey = row.SelectSingleNode("secret-access-key").InnerText.Trim();
                auth.MerchantId = row.SelectSingleNode("merchant-id").InnerText.Trim();
                auth.MarketplaceId = row.SelectSingleNode("marketplace-id").InnerText.Trim();
                auth.ServiceUrl = row.SelectSingleNode("service-url").InnerText.Trim();
            }
            return auth;
        }
    }
}
