
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicStore.Models
{
    public class Electronic
    {
       
        [Key]
        public int ElectronicID { get; set; }
        public string ElectronicName { get; set; }
        public string ElectronicType { get; set; }
        public string ElectronicColor { get; set; }
        public string ElectronicDescription { get; set; }


       

        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brands Brands { get; set; }


        public ICollection<Order> Orders { get; set; }

    }
}