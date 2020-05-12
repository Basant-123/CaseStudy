using CASE_STUDY.DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CASE_STUDY.Controllers
{
    public class AjaxController : Controller
    {
        // GET: Ajax
        [HttpPost]
        public ActionResult Filter(string From , string To , string Quantity)
        {
            DataTable dtblproduct = new DataTable();
            DAL dal = DAL.getInstance();
            if ((string.IsNullOrWhiteSpace(From) || string.IsNullOrWhiteSpace(To)))
            {
                if(string.IsNullOrWhiteSpace(Quantity))
                {//NO
                   
                    using (SqlConnection con = dal.GetConnection())
                    {
                        SqlDataAdapter sqlda = new SqlDataAdapter("select  * from Product", con);
                        sqlda.Fill(dtblproduct);
                    }
                }
                else
                {// Qty
                    using (SqlConnection con = dal.GetConnection())
                    {
                        string query = "select * from Product where Stock = @Stock";
                        SqlDataAdapter sqlda = new SqlDataAdapter(query, con);
                        sqlda.SelectCommand.Parameters.AddWithValue("@Stock", Quantity);
                        sqlda.Fill(dtblproduct);
                    }
                }   
            }
            else if (!string.IsNullOrWhiteSpace(Quantity))
            {
                //from , to , Quantity
                using (SqlConnection con = dal.GetConnection())
                {
                    string query = "select  * from Product where  Price BETWEEN @From AND @To and Stock = @Stock"; 
                    
                    SqlDataAdapter sqlda = new SqlDataAdapter(query, con);
                    sqlda.SelectCommand.Parameters.AddWithValue("@Stock", Quantity);
                    sqlda.SelectCommand.Parameters.AddWithValue("@To", To);
                    sqlda.SelectCommand.Parameters.AddWithValue("@From", From);

                    sqlda.Fill(dtblproduct);
                }

            }
            else
            {
                //from , to
                using (SqlConnection con = dal.GetConnection())
                {
                    string query = "select * from Product where  Price BETWEEN @From AND @To";
                    SqlDataAdapter sqlda = new SqlDataAdapter(query, con);
                    sqlda.SelectCommand.Parameters.AddWithValue("@To", To);
                    sqlda.SelectCommand.Parameters.AddWithValue("@From", From);
                  
                }
            }
            return PartialView("_ProductData", dtblproduct);
         
        }
    }
}