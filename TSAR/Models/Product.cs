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
        [Required]
        public int ProductId { get; set; }

        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        //[Display(Name = "Category")]
        //public string Category { get; set; }

        [Display(Name = "Price")]
        public double Price { get; set; }  


        public double totalPrice { get; set; }

        public bool Selected { get; set; }


        public virtual Client Client { get; set; }

        public int Id { get; set; }

        public string ClientName { get; set; } //check if existing client and pull name automatically from login

        public string Email { get; set; }   //check if existing client and pull email automatically from login




    }
}