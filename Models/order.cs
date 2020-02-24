using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ElectronicStore.Models
{
    public class Order
    {
       /* Here the order is placed by the customer which includes many electronics
        * properties that describe a order are:
        -name
        -payment type
        -date
        -cost( as in the total of the order)
        */
        public int OrderID { get; set; }
        public string OrderName { get; set; }
        public string OrderPayType { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderCost { get; set; }//in canadian dollars


        //Describes the "Many" in (Many Orders to Many electronics)
        public ICollection<Electronic> Electronics { get; set; }

    }
}