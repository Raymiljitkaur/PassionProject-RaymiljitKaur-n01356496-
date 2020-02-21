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

namespace ElectronicStore.Controllers
{
    public class OrderController : Controller
    {
        private ElectronicContext db = new ElectronicContext();

        
        public ActionResult List(string ordersearchkey)
        {
            Debug.WriteLine("The parameter is " + ordersearchkey);

            string query = "Select * from orders";
            if (ordersearchkey != "")
            {
                query = query + " where OrderName like '%" + ordersearchkey + "%'";
            }

         
            List<Order> orders = db.orders.SqlQuery(query).ToList();

            return View(orders);
        }
        public ActionResult New()
        {

            return View();
        }


        [HttpPost]
        public ActionResult Add(string OrderName, string OrderPayType, DateTime OrderDate, int OrderCost)
        {
            string query = "insert into Orders (OrderName, OrderPayType, OrderDate, OrderCost) values (@OrderName, @OrderPayType, @OrderDate, @OrderCost)";

            SqlParameter[] sqlparams = new SqlParameter[4];
            sqlparams[0] = new SqlParameter("@OrderName", OrderName);
            sqlparams[1] = new SqlParameter("@OrderPayType", OrderPayType);
            sqlparams[2] = new SqlParameter("@OrderDate", OrderDate);
            sqlparams[3] = new SqlParameter("@OrderCost", OrderCost);
           



            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }


        public ActionResult Show(int id)
        {
            
            string main_query = "select * from Orders where OrderID = @id";
            var pk_parameter = new SqlParameter("@id", id);
            Order Order = db.orders.SqlQuery(main_query, pk_parameter).FirstOrDefault();

           
            string aside_query = "select * from electronics inner join OrderElectronics on Electronics.ElectronicID = OrderElectronics.Electronic_ElectronicID where OrderElectronics.Order_OrderID=@id";
            var fk_parameter = new SqlParameter("@id", id);
            List<Electronic> orderedelectronics = db.Electronics.SqlQuery(aside_query, fk_parameter).ToList();

            string all_electronics_query = "select * from electronics";
            List<Electronic> AllElectronic = db.Electronics.SqlQuery(all_electronics_query).ToList();

            
            ShowOrders viewmodel = new ShowOrders();
            viewmodel.order = Order;
            viewmodel.Electronics = orderedelectronics;
            viewmodel.all_electronics = AllElectronic;

            return View(viewmodel);
        }


       
        [HttpPost]
        public ActionResult Attachelectronic(int id, int ElectronicID)
        {
            Debug.WriteLine("order id is" + id + " and ElectronicId is " + ElectronicID);

           
            string check_query = "select * from Electronics inner join OrderElectronics on OrderElectronics.Electronic_ElectronicID = Electronics.ElectronicID where Electronic_ElectronicID=@ElectronicID and Order_OrderID=@id";
            SqlParameter[] check_params = new SqlParameter[2];
            check_params[0] = new SqlParameter("@id", id);
            check_params[1] = new SqlParameter("@ElectronicID", ElectronicID);
            List<Electronic> electronics = db.Electronics.SqlQuery(check_query, check_params).ToList();
           
            if (electronics.Count <= 0)
            {


               
                string query = "insert into OrderElectronics (Electronic_ElectronicID, Order_OrderID) values (@ElectronicID, @id)";
                SqlParameter[] sqlparams = new SqlParameter[2];
                sqlparams[0] = new SqlParameter("@id", id);
                sqlparams[1] = new SqlParameter("@ElectronicID",ElectronicID);


                db.Database.ExecuteSqlCommand(query, sqlparams);
            }

            return RedirectToAction("Show/" + id);

        }


       
        [HttpGet]
        public ActionResult Detachelectronic(int id, int ElectronicID)
        {
            
            Debug.WriteLine("order id is" + id + " and ElectronicID is " + ElectronicID);

            string query = "delete from OrderElectronics where Electronic_ElectronicID=@ElectronicID and Order_OrderID=@id";
            SqlParameter[] sqlparams = new SqlParameter[2];
            sqlparams[0] = new SqlParameter("@ElectronicID", ElectronicID);
            sqlparams[1] = new SqlParameter("@id", id);

            db.Database.ExecuteSqlCommand(query, sqlparams);

            return RedirectToAction("Show/" + id);
        }

        public ActionResult DeleteConfirm(int id)
        {
            string query = "select * from Orders where OrderID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            Order order = db.orders.SqlQuery(query, param).FirstOrDefault();
            return View(order);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            string query = "delete from Orders where OrderID=@id";
            SqlParameter param = new SqlParameter("@id", id);
            db.Database.ExecuteSqlCommand(query, param);



            return RedirectToAction("List");
        }

        public ActionResult Update(int id)
        {
            string query = "select * from Orders where OrderID = @id";
            var parameter = new SqlParameter("@id", id);
            Order order = db.orders.SqlQuery(query, parameter).FirstOrDefault();

            return View(order);
        }
        [HttpPost]
        public ActionResult Update(int id, string OrderName, string OrderPayType, DateTime OrderDate, int OrderCost)
        {
            string query = "update Orders set OrderName=@OrderName, OrderPayType=@OrderPayType, OrderDate=@OrderDate, OrderCost=@OrderCost where OrderID = @id";

            SqlParameter[] sqlparams = new SqlParameter[5];

            sqlparams[0] = new SqlParameter("@OrderName", OrderName);
            sqlparams[1] = new SqlParameter("@OrderPayType", OrderPayType);
            sqlparams[2] = new SqlParameter("@OrderDate", OrderDate);
            sqlparams[3] = new SqlParameter("@OrderCost", OrderCost);
            sqlparams[4] = new SqlParameter("@id", id);


            db.Database.ExecuteSqlCommand(query, sqlparams);
            return RedirectToAction("List");
        }

    }
}