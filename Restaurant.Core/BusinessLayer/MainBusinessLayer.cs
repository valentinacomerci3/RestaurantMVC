using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Core.BusinessLayer
{
    public class MainBusinessLayer : IBusinessLayer
    {
        private readonly IDishRepository dishRepository;
        private readonly IAccountRepository accRepository;

        public MainBusinessLayer(IDishRepository dishRepo, IAccountRepository accRepository)
        {
            this.dishRepository = dishRepo;
            this.accRepository = accRepository;
        }

        public DishResult AddNewDish(Dish newDish)
        {
            if (newDish == null)
            {
                throw new ArgumentNullException("Invalid item");
            }

            var result = dishRepository.AddItem(newDish);

            if (result)
            {
                return new DishResult
                {
                    Success = true,
                    Message = "Ok"
                };
            }
            return new DishResult
            {
                Success = false,
                Message = $"Dish with name -{newDish.Name}-  cannot be saved"
            };

        }

        public DishResult DeleteDish(Dish data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Invalid data");
            }
            var result = dishRepository.Delete(data.Id);
            if (result)
            {
                return new DishResult
                {
                    Success = true,
                    Message = ""
                };
            }
            return new DishResult
            {
                Success = false,
                Message = "Cannot delete"
            };
        }

        public DishResult EditDish(Dish data)
        {
            if (data == null)
            {
                throw new ArgumentNullException("Invalid info");
            }

            var result = dishRepository.EditItem(data);
            if (result)
            {
                return new DishResult
                {
                    Success = true,
                    Message = ""
                };
            }

            return new DishResult
            {
                Success = false,
                Message = "Cannot edit"
            };

        }

        public IEnumerable<Dish> FetchDishes()
        {
            return dishRepository.FetchAll();
        }

        public Account GetAccount(string username)
        {
            if (String.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Invalid username");
            }
            return accRepository.GetByUsername(username);
        }

        public Dish GetDishById(int id)
        {
            //Guardia
            if (id <= 0)
            {
                throw new ArgumentException("Invalid id");
            }
            return dishRepository.GetById(id);
        }
    }
}
