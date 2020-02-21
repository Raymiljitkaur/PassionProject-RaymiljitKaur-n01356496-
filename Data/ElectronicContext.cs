using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace ElectronicStore.Data
{
    public class ElectronicContext : DbContext
    {
        

        public ElectronicContext() : base("name=ElectronicContext")
        {
        }


        public System.Data.Entity.DbSet<ElectronicStore.Models.Electronic> Electronics { get; set; }

        public System.Data.Entity.DbSet<ElectronicStore.Models.Brands> Brands { get; set; }

        public System.Data.Entity.DbSet<ElectronicStore.Models.Order> orders { get; set; }

        public System.Data.Entity.DbSet<ElectronicStore.Models.Customer> Customers { get; set; }
    }


    }