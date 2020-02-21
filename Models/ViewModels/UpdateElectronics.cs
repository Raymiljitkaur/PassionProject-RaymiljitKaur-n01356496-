using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ElectronicStore.Models.ViewModels
{
    public class UpdateElectronics
    {
       

        public Electronic Electronics { get; set; }
        public List<Brands> Brands { get; set; }
    }
}