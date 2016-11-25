using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketplaceWebService.Model;
using Ragnarok.model;
using Ragnarok.userinteraction;
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

        public void insertReportsToDB(ICollection<ReportInfo> reports, UserInteraction interaction)
        {
            
            MySqlCommand myCommand = this.conn.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = this.conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = this.conn;
            myCommand.Transaction = myTrans;

            try
            {
                foreach(ReportInfo report in reports){
                    myCommand.CommandText = "insert into exported_reports (reportId, exportedDate) VALUES (" + report.ReportId + ", now())";
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();

                interaction.setStatus("Successfully inserted "+reports.Count+" new records to the MySQL database - these have not yet been pushed to Everest though.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    interaction.setStatus("DANGER: An exception occurred while rolling back reports from MySQL : " + ex.Message);
                }
            }

        }


        public void insertOrdersToDB(ICollection<AmazonOrder> orders, UserInteraction interaction)
        {

            MySqlCommand myCommand = this.conn.CreateCommand();
            MySqlTransaction myTrans;

            // Start a local transaction
            myTrans = this.conn.BeginTransaction();
            // Must assign both transaction object and connection
            // to Command object for a pending local transaction
            myCommand.Connection = this.conn;
            myCommand.Transaction = myTrans;

            try
            {
                foreach (AmazonOrder order in orders)
                {
                    myCommand.CommandText = "insert into exported_reports (reportId, exportedDate) VALUES (" + order.Identifier + ", now())";
                    myCommand.ExecuteNonQuery();
                }
                myTrans.Commit();

                interaction.showError("Successfully inserted " + orders.Count + " new orders to the MySQL database - these have not yet been pushed to Everest though.");
            }
            catch (Exception e)
            {
                try
                {
                    myTrans.Rollback();
                }
                catch (MySqlException ex)
                {
                    interaction.showError("DANGER: An exception occurred while rolling back reports from MySQL : " + ex.Message);
                }
            }

        }

        public void deleteOrdersFromDB(ICollection<AmazonOrder> reports)
        {

        }

        public void deleteReportsFromDB(ICollection<ReportInfo> orders)
        {

        }


    }
}
