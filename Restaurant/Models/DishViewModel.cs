using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class DishViewModel
    {
        public int Id { get; set; }
        [Required, DisplayName("Name")]
        public string Name { get; set; }
        [Required, DisplayName("Name")]
        public string Description { get; set; }
        [Required, DisplayName("Description")]
        public int Price { get; set; }
        [Required]
        public Categ Categ { get; set; }
    }
}
