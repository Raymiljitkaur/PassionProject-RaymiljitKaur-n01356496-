using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class ShowOrders
    {

        
        public virtual Order order { get; set; }
       
        public List<Electronic> Electronics { get; set; }

        
        public List<Electronic> all_electronics { get; set; }

    }
}