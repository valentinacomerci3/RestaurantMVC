using Microsoft.EntityFrameworkCore;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.EF.Repositories
{
    public class EFDishRepository : IDishRepository
    {
        private readonly DishContext ctx;

        public EFDishRepository(DishContext context)
        {
            this.ctx = context;
        }

        public bool AddItem(Dish newItem)
        {
            //Guardia
            if (newItem == null)
            {
                throw new ArgumentNullException("Invalid item");
            }
            try
            {
                ctx.Dishes.Add(newItem);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid Id");
            }
            try
            {
                var itemToDelete = GetById(id);
                if (itemToDelete != null)
                {
                    ctx.Dishes.Remove(itemToDelete);
                    ctx.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool EditItem(Dish itemToEdit)
        {
            if (itemToEdit == null)
            {
                throw new ArgumentNullException("Invalid item");
            }
            try
            {
                ctx.Entry(itemToEdit).State = EntityState.Modified;
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Dish> FetchAll()
        {
            try
            {
                return ctx.Dishes.ToList();
            }
            catch (Exception)
            {
                return null;
            }

        }

        public Dish GetById(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("invalid id");
            }
            try
            {
                return ctx.Dishes.FirstOrDefault(x => x.Id == id);
            }
            catch
            {
                return null;
            }

        }
    }
}
