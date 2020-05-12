using CASE_STUDY.DataAccess;
using CASE_STUDY.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web.Mvc;

namespace CASE_STUDY.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        [HttpGet]
        public ActionResult Display()
        {
            DataTable dtblproduct = new DataTable();
            DAL dal = DAL.getInstance();
            using (SqlConnection con = dal.GetConnection())
            {
                SqlDataAdapter sqlda = new SqlDataAdapter("select  * from Product", con);
                sqlda.Fill(dtblproduct);
            }
                
            return View(dtblproduct);
        }
  
        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View( new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel product)
        {
            string fileName = Path.GetFileNameWithoutExtension(product.ImageFile.FileName);
            string extension = Path.GetExtension(product.ImageFile.FileName);
            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
            product.ProductImage = "~/Image/" + fileName; // Relative path
            fileName = Path.Combine(Server.MapPath("~/Image/"), fileName);
            product.ImageFile.SaveAs(fileName);

            // TODO: Add insert logic here
            DAL dal = DAL.getInstance();
            using (SqlConnection con = dal.GetConnection())
            {
                string query = "Insert into Product Values(@ProductTitle,@ProductImage,@price,@Stock)";
                SqlCommand sqlCmd = new SqlCommand(query, con);
                sqlCmd.Parameters.AddWithValue("@ProductTitle", product.ProductTitle);
                sqlCmd.Parameters.AddWithValue("@ProductImage", product.ProductImage);
                sqlCmd.Parameters.AddWithValue("@Price", product.Price);
                sqlCmd.Parameters.AddWithValue("@Stock", product.Stock);

                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Display");
         
        }     
    }
}
