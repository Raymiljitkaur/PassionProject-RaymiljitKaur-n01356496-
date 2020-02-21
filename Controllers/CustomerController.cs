using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElectronicStore.Data;
using ElectronicStore.Models;
using ElectronicStore.Models.ViewModels;
using System.Diagnostics;
using System.IO;

namespace ElectronicStore.Controllers
{
    public class CustomerController : Controller
    {
        private ElectronicContext db = new ElectronicContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string customersearchkey)
        {
            Debug.WriteLine("The parameter is " + customersearchkey);

            string query = "Select * from Customers";
            if (customersearchkey != "")
            {
                query = query + " where CustomerName like '%" + customersearchkey + "%'";
            }

            //what data do we need?
            List<Customer> customers = db.Customers.SqlQuery(query).ToList();

            return View(customers);
        }
        public ActionResult Add()
        {
        
            return View();
        }
        [HttpPost]
        public ActionResult Add(string CustomerName,string CustomerAddress, string CustomerEmail)
        {
            string query = "insert into Customers (CustomerName,CustomerAddress,CustomerEmail) values (@CustomerName,@CustomerAddress,@CustomerEmail)";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@CustomerName", CustomerName);
            sqlparams[1] = new SqlParameter("@CustomerEmail", CustomerEmail);
            sqlparams[2] = new SqlParameter("@CustomerAddress", CustomerAddress);
           

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        public ActionResult Show(int id)
        {
            string query = "select * from Customers where CustomerID = @id";
            var parameter = new SqlParameter("@id", id);
            Customer selectedcustomer = db.Customers.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedcustomer);
        }
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Customers where CustomerID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Customer selectedcustomer = db.Customers.SqlQuery(query, param).FirstOrDefault();
            return View(selectedcustomer);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Customers where CustomerID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }
    }
}