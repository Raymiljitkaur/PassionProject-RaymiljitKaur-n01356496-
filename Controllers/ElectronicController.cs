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
        private ElectronicContext db = new ElectronicContext();

        public ActionResult list(string Electronicsearchkey)
        {
           
            Debug.WriteLine("The search key is " + Electronicsearchkey);

            string query = "Select * from electronics";

            if (Electronicsearchkey != "")
            {
                
                query = query + " where electronicname like '%" + Electronicsearchkey + "%'";
                Debug.WriteLine("The query is " + query);
            }

            List<Electronic> Electronics = db.Electronics.SqlQuery(query).ToList();
            return View(Electronics);

        }
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
        public ActionResult Add()
        {


            List<Brands> brands = db.Brands.SqlQuery("select * from Brands").ToList();

            return View(brands);
        }
        public ActionResult Show(int? id)
        {
            if (id == null)

            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Electronic electronic = db.Electronics.SqlQuery("select * from electronics where ElectronicID=@ElectronicID", new SqlParameter("@ElectronicID", id)).FirstOrDefault();
            if (electronic == null)
            {
                return HttpNotFound();
            }

            string query = "select * from orders inner join OrderElectronics on Orders.OrderID = OrderElectronics.Order_OrderID where Electronic_ElectronicID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            List<Order> OrderElectronics = db.orders.SqlQuery(query, param).ToList();


            ShowElectronics viewmodel = new ShowElectronics();
            viewmodel.Electronics = electronic;
            viewmodel.orders = OrderElectronics;


            return View(viewmodel);
        }
        public ActionResult Update(int id)
        {
            
            Electronic selectedelectronic = db.Electronics.SqlQuery("select * from electronics where ElectronicID = @id", new SqlParameter("@id", id)).FirstOrDefault();
            List<Brands> brands = db.Brands.SqlQuery("select * from Brands").ToList();

            UpdateElectronics ViewModel = new UpdateElectronics();
            ViewModel.Electronics = selectedelectronic;
            ViewModel.Brands = brands;

            return View(ViewModel);
        }
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
        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from electronics where ElectronicID = @id";
            SqlParameter param = new SqlParameter("@id", id);
            Electronic selectedelectronic = db.Electronics.SqlQuery(query, param).FirstOrDefault();

            return View(selectedelectronic);
        }
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