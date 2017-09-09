using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using TSAR.Models;



namespace TSAR.Models
{
    public class Product
    {
        [Key]

        public int ProductId { get; set; }

        public string ProductName { get; set; }


        public double Price { get; set; }

        public string Name { get; set; } //check if existing client and pull name automatically from login

        public string Email { get; set; }   //check if existing client and pull email automatically from login


    }
}