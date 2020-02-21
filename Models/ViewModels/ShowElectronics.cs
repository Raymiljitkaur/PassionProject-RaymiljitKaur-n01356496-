using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class ShowElectronics
    {
       
        public virtual Electronic Electronics { get; set; }

        
        public List<Order> orders { get; set; }
    }
}