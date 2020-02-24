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
        //connection to the database.
        private ElectronicContext db = new ElectronicContext();
        // GET: Customer
        public ActionResult Index()
        {
            return View();
        }
        // to get the list of the customer to display and and to perform search functionaity on it
        public ActionResult List(string customersearchkey)
        {
            Debug.WriteLine("The parameter is " + customersearchkey);
            //to select a particular customer
            string query = "Select * from Customers";
            if (customersearchkey != "")
            {
                query = query + " where CustomerName like '%" + customersearchkey + "%'";
            }

           // to get the list of the customer
            List<Customer> customers = db.Customers.SqlQuery(query).ToList();

            return View(customers);
        }
        // to add a new customer
        //to push the data to the database
        public ActionResult Add()
        {
        
            return View();
        }
        //to pulll the data from the fields of the interface.
        [HttpPost]
        public ActionResult Add(string CustomerName,string CustomerAddress, string CustomerEmail)
        {
            //database query to add customer
            string query = "insert into Customers (CustomerName,CustomerAddress,CustomerEmail) values (@CustomerName,@CustomerAddress,@CustomerEmail)";
            SqlParameter[] sqlparams = new SqlParameter[3];
            sqlparams[0] = new SqlParameter("@CustomerName", CustomerName);
            sqlparams[1] = new SqlParameter("@CustomerEmail", CustomerEmail);
            sqlparams[2] = new SqlParameter("@CustomerAddress", CustomerAddress);
           

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        // to show the deatils of the customer
        public ActionResult Show(int id)
        {
            // database query to select a customer basis of a particular id it is being called for..
            string query = "select * from Customers where CustomerID = @id";
            var parameter = new SqlParameter("@id", id);
            Customer selectedcustomer = db.Customers.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedcustomer);//return the selected customer details
        }
        // here we are deleting the customer if need be..
        //to get customer to be deleted and ask for the confirmation
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Customers where CustomerID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Customer selectedcustomer = db.Customers.SqlQuery(query, param).FirstOrDefault();
            return View(selectedcustomer);
        }
        // on calling this function the customeer will be deleted and the database will be updated .
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