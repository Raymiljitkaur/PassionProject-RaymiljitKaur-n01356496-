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
    public class ElectronicController : Controller
    {
        //setting up connection with the database
        private ElectronicContext db = new ElectronicContext();
        //function to dispaly the list and to have an option to search through the list
        public ActionResult list(string Electronicsearchkey)
        {
           
            Debug.WriteLine("The search key is " + Electronicsearchkey);

            string query = "Select * from electronics";

            if (Electronicsearchkey != "")
            {
                
                query = query + " where electronicname like '%" + Electronicsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }
            // to get the list
            List<Electronic> Electronics = db.Electronics.SqlQuery(query).ToList();
            return View(Electronics);

        }
        // function to pull the data  from the fileds of the interface
        //here all the data entered is taken into accountnad then submitted
        [HttpPost]
        public ActionResult Add(string ElectronicName, string ElectronicType, string ElectronicColor, int BrandID, string ElectronicDescription)
        {

            string query = "insert into electronics (ElectronicName, ElectronicType, ElectronicColor, BrandID, ElectronicDescription) values (@ElectronicName,@ElectronicType,@ElectronicColor,@BrandID,@ElectronicDescription)";
            SqlParameter[] sqlparams = new SqlParameter[5];
            sqlparams[0] = new SqlParameter("@ElectronicName", ElectronicName);
            sqlparams[1] = new SqlParameter("@ElectronicType", ElectronicType);
            sqlparams[2] = new SqlParameter("@ElectronicColor", ElectronicColor);
            sqlparams[3] = new SqlParameter("@BrandID", BrandID);
            sqlparams[4] = new SqlParameter("@ElectronicDescription", ElectronicDescription);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        //to push the data to the database 
        public ActionResult Add()
        {


            List<Brands> brands = db.Brands.SqlQuery("select * from Brands").ToList();

            return View(brands);
        }
        // function to show the details of the electronics along with order they are placed in...

        public ActionResult Show(int? id)
        {
            //if there is nothing to show
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Electronic electronic = db.Electronics.SqlQuery("select * from electronics where ElectronicID=@ElectronicID", new SqlParameter("@ElectronicID", id)).FirstOrDefault();
            if (electronic == null)
            {
                return HttpNotFound();
            }
            //database query to select data from electronics and orders table .
            string query = "select * from orders inner join OrderElectronics on Orders.OrderID = OrderElectronics.Order_OrderID where Electronic_ElectronicID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            List<Order> OrderElectronics = db.orders.SqlQuery(query, param).ToList();

            //using view model for showing the electronics where the orders are displayed for one electronic.
            ShowElectronics viewmodel = new ShowElectronics();
            viewmodel.Electronics = electronic;
            viewmodel.orders = OrderElectronics;


            return View(viewmodel);
        }
        // function to update i.e to make changes in the enteries of the electronics details
        public ActionResult Update(int id)
        {
            //here we are getting information about the particular electronic on a id.
            Electronic selectedelectronic = db.Electronics.SqlQuery("select * from electronics where ElectronicID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Brands> brands = db.Brands.SqlQuery("select * from Brands").ToList();
            //using view model for update electronic
            UpdateElectronics ViewModel = new UpdateElectronics();
            ViewModel.Electronics = selectedelectronic;
            ViewModel.Brands = brands;

            return View(ViewModel);
        }
        //here we are pushuing the changed values into the database and updating the electronic
        [HttpPost]
        public ActionResult Update(string ElectronicName, string ElectronicType, string ElectronicColor, int BrandID, string ElectronicDescription, int id)
        {


            string query = "update electronics set ElectronicName=@ElectronicName, ElectronicType=@ElectronicType, ElectronicColor=@ElectronicColor, ElectronicDescription=@ElectronicDescription, BrandID=@BrandID where ElectronicID= @id";
            SqlParameter[] sqlparams = new SqlParameter[6];
            sqlparams[0] = new SqlParameter("@ElectronicName", ElectronicName);
            sqlparams[1] = new SqlParameter("@ElectronicType", ElectronicType);
            sqlparams[2] = new SqlParameter("@ElectronicColor", ElectronicColor);
            sqlparams[3] = new SqlParameter("@BrandID", BrandID);
            sqlparams[4] = new SqlParameter("@ElectronicDescription", ElectronicDescription);
            sqlparams[5] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }
        //here is the functiinality to delete a electronic
        // to confirm if they want to delete 
        // here only a particular brand is selected and ask for confimation to delete.
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from electronics where ElectronicID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Electronic selectedelectronic = db.Electronics.SqlQuery(query, param).FirstOrDefault();

            return View(selectedelectronic);
        }
        //here on giving confirmation the electronic is deleted and the database is updated.
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from electronics where ElectronicID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);

            return RedirectToAction("List");
        }

    }
}