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
        [Key]
        public int BrandID { get; set; }

        public string BrandName { get; set; }

        public ICollection<Electronic> Electronics { get; set; }
    }
}