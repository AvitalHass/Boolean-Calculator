using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace angular_starter.Models
{
    public class Class1
    {

        [StringLength(10)]
        [Required]
        public string x { get; set; }
    }
}