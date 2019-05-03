using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project.Models
{
    public class ProductViewModel
    {
        [Display(Name = "Add Image")]
        public HttpPostedFileBase Image { get; set; }
        [Display(Name = "Product Image")]
        public string ImagePath { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }

        public int Quantity { get; set; }

        

        public int Price { get; set; }

    }
}