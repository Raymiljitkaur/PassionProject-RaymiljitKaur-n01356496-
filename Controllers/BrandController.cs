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
        private ElectronicContext db = new ElectronicContext();
        //to get brands
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List(string brandsearchkey)
        {
            Debug.WriteLine("The parameter is " + brandsearchkey);

            string query = "Select * from brands";
            if (brandsearchkey != "")
            {
                query = query + " where BrandName like '%" + brandsearchkey + "%'";
            }

            //what data do we need?
            List<Brands> brands = db.Brands.SqlQuery(query).ToList();

            return View(brands);
        }
        public ActionResult Add()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult Add(string BrandName)
        {
            string query = "insert into brands (BrandName) values (@BrandName)";
            var parameter = new SqlParameter("@BrandName", BrandName);

            db.Database.ExecuteSqlCommand(query, parameter);
            return RedirectToAction("List");
        }
        public ActionResult Update(int id)
        {
            string query = "select * from brands where BrandID = @id";
            var parameter = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedbrands);
        }
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
        public ActionResult Show(int id)
        {
            string query = "select * from brands where BrandID = @id";
            var parameter = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, parameter).FirstOrDefault();

            return View(selectedbrands);
        }
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from brands where BrandID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Brands selectedbrands = db.Brands.SqlQuery(query, param).FirstOrDefault();
            return View(selectedbrands);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from brands where BrandID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);


         
            string refquery = "update electronics set BrandID = '' where BrandID=@id";
            db.Database.ExecuteSqlCommand(refquery, param); 

            return RedirectToAction("List");
        }
    }
}