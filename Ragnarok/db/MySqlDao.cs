using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebService.Model;
using Ragnarok.model;
using MySql.Data.MySqlClient;

namespace Ragnarok.db
{
    class MySqlDao : IDao, IDisposable
    {
        private static String MYSQL_CONNECTION_INFORMATION = "server=127.0.0.1;uid=mike;pwd=password;database=ragnarok;";

        private MySqlConnection conn;

        /*
         * 
         * CREATE TABLE exported_orders (
             orderId varchar(255),
             exportedDate DATETIME
            ); 
            CREATE TABLE exported_reports
            (
                reportId varchar(255),
                exportedDate DATETIME
            ); 
            CREATE TABLE logging
            (
                reportId varchar(255),
                message varchar(500),
                success boolean
            );
         */

        public void Dispose()
        {
            conn.Close();
        }

        public MySqlDao()
        {

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = MYSQL_CONNECTION_INFORMATION;
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ICollection<MarketplaceWebService.Model.ReportInfo> filterReportsAlreadySynced(ICollection<MarketplaceWebService.Model.ReportInfo> reports)
        {
            IEnumerator<ReportInfo> iter = reports.GetEnumerator();
            string joined = string.Join(",", reports.Select(x => x.ReportId));
            string sql = "SELECT reportId FROM exported_reports WHERE reportId IN(" + joined + ")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            HashSet<String> previouslyUsedIds = new HashSet<String>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Object obj = rdr[0];
                    previouslyUsedIds.Add((String)obj);
                }
            }
            return reports.Where(x => !previouslyUsedIds.Contains(x.ReportId)).ToList();
        }

        public ICollection<model.AmazonOrder> filterOrdersAlreadySynced(ICollection<model.AmazonOrder> orders)
        {
            IEnumerator<AmazonOrder> iter = orders.GetEnumerator();
            string joined = string.Join(",", orders.Select(x => "\"" + x.Identifier + "\""));
            string sql = "SELECT orderId FROM exported_orders WHERE orderId IN(" + joined + ")";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            HashSet<String> previouslyUsedIds = new HashSet<String>();
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    Object obj = rdr[0];
                    previouslyUsedIds.Add((String)obj);
                }
            }
            return orders.Where(x => !previouslyUsedIds.Contains(x.Identifier)).ToList();
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
