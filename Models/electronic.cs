
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
       /*Electronics is  a device which can be ordered by many customer  and will always hav one particular brand.
        * some properties that describe a electronics are:
        - name
        - type of electronic(if it is a phone or tablet or laptop)
        - color
        - Description about the electronic to be more specific
        */
        [Key]
        public int ElectronicID { get; set; }
        public string ElectronicName { get; set; }
        public string ElectronicType { get; set; }
        public string ElectronicColor { get; set; }
        public string ElectronicDescription { get; set; }


       //this describes one in one to many relation between electronic and brand

        public int BrandID { get; set; }
        [ForeignKey("BrandID")]
        public virtual Brands Brands { get; set; }

        // this describe many in many to many relation between  orders and electronics
        public ICollection<Order> Orders { get; set; }

    }
}