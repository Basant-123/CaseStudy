using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace CASE_STUDY.DataAccess
{
    public class DAL
    {
        private static DAL connInstance;

        private DAL()
        {
        }
        public static DAL getInstance()
         {
            if(connInstance == null)
            {
                connInstance = new DAL();

            }
            return connInstance;
        }
        public  SqlConnection GetConnection()
        {
            string connectionString = WebConfigurationManager.ConnectionStrings["dbconnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);
            try
            {
                conn.Open();
            }
            catch(Exception ex)
            {
                throw;
            }
      
            return conn;
        }
       
    }
}