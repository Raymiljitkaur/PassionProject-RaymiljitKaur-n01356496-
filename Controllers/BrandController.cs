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

namespace GadgetStore.Data
{
    public class BrandController : Controller
    {
        //to provide a connection to the database
        private ElectronicContext db = new ElectronicContext();
        //to get brands
        public ActionResult Index()
        {
            return View();
        }
        // to provide the list of the brands to list link and also to add the search functionality for thr brands
        public ActionResult List(string brandsearchkey)
        {
            Debug.WriteLine("The parameter is " + brandsearchkey);

            string query = "Select * from brands";
            if (brandsearchkey != "")
            {
                query = query + " where BrandName like '%" + brandsearchkey + "%'";
            }

            //we need a brand list to search from 
            List<Brands> brands = db.Brands.SqlQuery(query).ToList();

            return View(brands);
        }
        // to add a new brand
       // this to pull the data from the divs
        [HttpPost]
        public ActionResult Add(string BrandName)
        {
            string query = "insert into brands (BrandName) values (@BrandName)";//database query
            var parameter = new SqlParameter("@BrandName", BrandName);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
        // this is to push data to the database
        public ActionResult Add()
        {

            return View();
        }
        //to update the brand
        // here we are getting information about the particular brand
        public ActionResult Update(int id)
        {
            string query = "select * from brands where BrandID = @id";
            var parameter = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedbrands);
        }
        //here we are updating the information and also updating them in database.
        [HttpPost]
        public ActionResult Update(int id, string BrandName)
        {
            string query = "update brands set BrandName= @BrandName where BrandID = @id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@BrandName", BrandName);
            sqlparams[1] = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("List");
        }
        // to show the deatils of brand
        public ActionResult Show(int id)
        {
            string query = "select * from brands where BrandID = @id";// database query
            var parameter = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedbrands);
        }
        // here we are trying to delete the brand
        // to confirm if they want to delete 
        // here only a particular brand is selected .
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from brands where BrandID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, param).FirstOrDefault();
            return View(selectedbrands);
        }
        // here the brand will be deleted if this function is called
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from brands where BrandID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


         // here the database will be updated after the deletion of a entry.
            string refquery = "update electronics set BrandID = '' where BrandID=@id";
            db.Database.ExecuteSqlCommand(refquery, param); 

            return RedirectToAction("List");// go back to the list 
        }
    }
}