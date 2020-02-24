using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ElectronicStore.Models
{
    public class Brands
    {
        /* A brand is part of the electronic which specifies its major factor relayed to which company manufacture this electronic.
         * it uses attribut of the brand name.
         */
        [Key]
        public int BrandID { get; set; }

        public string BrandName { get; set; }
        //here the electronic has been referred as the foreign table to get and set values and also describe many in one to many relation between these two.
        public ICollection<Electronic> Electronics { get; set; }
    }
}