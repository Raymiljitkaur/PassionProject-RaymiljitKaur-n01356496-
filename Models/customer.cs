using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace ElectronicStore.Models
{
    public class Customer
    {
        /* customer is the entity who places the orders for the particular electronics
         some properties that describe a customer are:
         -Customer Name
         -Customer Email
         -Customer Address
         */
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }

        // here the orders table describes many in  one to  many relation.
        public ICollection<Order> Orders { get; set; }
    }
} 