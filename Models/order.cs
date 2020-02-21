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
       
        public int OrderID { get; set; }
        public string OrderName { get; set; }
        public string OrderPayType { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderCost { get; set; }


        //Representing the "Many" in (Many Orders to Many electronics)
        public ICollection<Electronic> Electronics { get; set; }

    }
}