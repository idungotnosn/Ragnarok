using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using MarketplaceWebService;
using MarketplaceWebService.Model;
using System.Xml;

namespace Ragnarok.amazonhttp
{
    class ReportRetrievalService
    {

        public static void InvokeGetReport(MarketplaceWebService.MarketplaceWebService service, GetReportRequest request)
        {
            try
            {
                GetReportResponse response = service.GetReport(request);

                ResponseHeaderMetadata headerMetadata = response.ResponseHeaderMetadata;
                ResponseMetadata metadata = response.ResponseMetadata;


                Console.WriteLine("Service Response");
                Console.WriteLine("=============================================================================");
                Console.WriteLine();

                Console.WriteLine("        GetReportResponse");
                if (response.IsSetGetReportResult())
                {
                    Console.WriteLine("            GetReportResult");
                    GetReportResult getReportResult = response.GetReportResult;
                    if (getReportResult.IsSetContentMD5())
                    {
                        Console.WriteLine("                ContentMD5");
                        Console.WriteLine("                    {0}", getReportResult.ContentMD5);
                    }
                }
                if (response.IsSetResponseMetadata())
                {
                    Console.WriteLine("            ResponseMetadata");
                    ResponseMetadata responseMetadata = response.ResponseMetadata;
                    if (responseMetadata.IsSetRequestId())
                    {
                        Console.WriteLine("                RequestId");
                        Console.WriteLine("                    {0}", responseMetadata.RequestId);
                    }
                }

                Console.WriteLine("            ResponseHeaderMetadata");
                Console.WriteLine("                RequestId");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.RequestId);
                Console.WriteLine("                ResponseContext");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.ResponseContext);
                Console.WriteLine("                Timestamp");
                Console.WriteLine("                    " + response.ResponseHeaderMetadata.Timestamp);

            }
            catch (MarketplaceWebServiceException ex)
            {
                Console.WriteLine("Caught Exception: " + ex.Message);
                Console.WriteLine("Response Status Code: " + ex.StatusCode);
                Console.WriteLine("Error Code: " + ex.ErrorCode);
                Console.WriteLine("Error Type: " + ex.ErrorType);
                Console.WriteLine("Request ID: " + ex.RequestId);
                Console.WriteLine("XML: " + ex.XML);
                Console.WriteLine("ResponseHeaderMetadata: " + ex.ResponseHeaderMetadata);
            }
        }


        private static Authentication getAuthentication()
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

        public Stream getReport(){

            Authentication auth = ReportRetrievalService.getAuthentication();


            

            const string applicationName = "Ragnarok";
            const string applicationVersion = "0.1";

            
            MarketplaceWebServiceConfig marketplaceConfig = new MarketplaceWebServiceConfig();
            marketplaceConfig.ServiceURL = auth.ServiceUrl;
            
            marketplaceConfig.SetUserAgentHeader(
                applicationName,
                applicationVersion,
                "C#"
                );

            MarketplaceWebServiceClient service = new MarketplaceWebServiceClient(
                auth.AccessKeyId, 
                auth.SecretAccessKey, 
                marketplaceConfig
            );


            GetReportRequest request = new GetReportRequest();
            request.Merchant = auth.MerchantId;
            request.MWSAuthToken = "<Your MWS Auth Token>"; // Optional

            // Note that depending on the type of report being downloaded, a report can reach 
            // sizes greater than 1GB. For this reason we recommend that you _always_ program to
            // MWS in a streaming fashion. Otherwise, as your business grows you may silently reach
            // the in-memory size limit and have to re-work your solution.
            // NOTE: Due to Content-MD5 validation, the stream must be read/write.
            request.ReportId = "3002725304017077";
            request.Report = File.Open("report.xml", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            InvokeGetReport(service, request);
            using (StreamReader reader = new StreamReader(request.Report))
            {
                String contents = reader.ReadToEnd();
                Console.WriteLine(contents);
            }
            File.Delete("report.xml");
            Console.ReadLine();
            return null;
        }
    }
}
