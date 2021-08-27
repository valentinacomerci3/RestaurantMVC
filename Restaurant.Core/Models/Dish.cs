using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Models
{
    public enum Categ
    {
        FirstCourse,
        SecondCourse,
        Side,
        Dessert
    }
    public class Dish
    { 
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public Categ Categ { get; set; }

    }
}
