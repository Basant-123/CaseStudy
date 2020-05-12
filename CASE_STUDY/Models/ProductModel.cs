using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CASE_STUDY.Models
{
    public class ProductModel
    {
      
            public int ProductId { get; set; }

            [DisplayName("Product Title")]
            [Required]
            public string ProductTitle { get; set; }

            [DisplayName("Upload Image")]
            public string ProductImage { get; set; }

            [Required]
            [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
             public decimal Price { get; set; }

            [Required]
            public int Stock { get; set; }

            public HttpPostedFileBase ImageFile { get; set; }
        
    }
}