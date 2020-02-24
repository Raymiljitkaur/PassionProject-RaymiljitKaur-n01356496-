using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class UpdateElectronics
    {
       /* when we are adding a new electronics we also need a brand to add it
        * so here we are calling the list of the brands and electronics*/

        public Electronic Electronics { get; set; }
        public List<Brands> Brands { get; set; }
    }
}