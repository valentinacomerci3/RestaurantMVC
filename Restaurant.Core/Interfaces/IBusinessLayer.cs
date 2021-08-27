using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.Interfaces
{
    public interface IBusinessLayer
    {
        IEnumerable<Dish> FetchDishes();
        Dish GetDishById(int id);
        DishResult AddNewDish(Dish newEmp);
        DishResult EditDish(Dish data);
        DishResult DeleteDish(Dish data);

        Account GetAccount(string username);
    }
}
