using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class ShowElectronics
    {
       /* here we are using this electronics and orders table*/

        //one individual electronic
        public virtual Electronic Electronics { get; set; }

        //list  of many orders that have this electronics
        public List<Order> orders { get; set; }
    }
}