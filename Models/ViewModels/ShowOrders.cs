using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class ShowOrders
    {

        //one individual order
        public virtual Order order { get; set; }
       //list of electronics according to the order they are placed in...
        public List<Electronic> Electronics { get; set; }


        //a SEPARATE list for representing the ADD of an order to a electronic,
        //a dropdownlist of all electronics to add to the order.
        public List<Electronic> all_electronics { get; set; }

    }
}