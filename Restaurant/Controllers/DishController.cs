using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Interfaces;
using Restaurant.Core.Models;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Controllers
{
    
        [Authorize]
        public class DishController : Controller
        {
            private readonly IBusinessLayer bl;

            public DishController(IBusinessLayer businessLayer)
            {
                this.bl = businessLayer;
            }

            [Route("Dish/Index", Order = 1)]
       
            
            public IActionResult Index()
            {
                var model = bl.FetchDishes();
                return View(model);
            }

            //GET Dish/Details/{id}
            [HttpGet("dish/details/{id}")]
            public IActionResult Details(int id)
            {
                //VALIDATION
                if (id <= 0)
                {
                    return View("Error", new ErrorViewModel());
                }

                //GET DISH TO EDIT
                var dsh = bl.GetDishById(id);

                //Restituzione della vista (check del modello)
                if (dsh == null)
                {
                    return View("NotFound", new NotFoundViewModel()
                    {
                        EntityId = id,
                        Message = "Something wrong"
                    });
                }
                return View(dsh);
            }

            [Authorize(Policy = "AccountAdministrator")]
            //GET
            public IActionResult Create()
            {
                return View();
            }

            [Authorize(Policy = "AccountAdministrator")]
            [HttpPost]
            public IActionResult Create(DishViewModel data)
            {
                //validazione
                if (data == null)
                {
                    return View("Error", new ErrorViewModel());
                }
                if (ModelState.IsValid)
                {
                    //chiamata al business layer
                    DishResult result = bl.AddNewDish(new Dish
                    {
                        Name = data.Name,
                        Description = data.Description,
                        Price = data.Price,
                        Categ = data.Categ
                    });
                    //restituzione della view
                    if (result.Success)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View();
            }

            public IActionResult Edit(int id)
            {
                if (id <= 0)
                {
                    return View("Error", new ErrorViewModel());
                }

                //GETDISHTOEDIT
                var model = bl.GetDishById(id);
                if (model == null)
                {
                    return View("NotFound", new NotFoundViewModel { EntityId = id, Message = "Sorry, not found" });
                }
                var empViewModel = new DishViewModel
                {
                    Id = model.Id,
                    Name = model.Name,
                    Description = model.Description,
                    Price = model.Price,
                    Categ = model.Categ
                };
                return View(empViewModel);
            }

            [HttpPost]
            public IActionResult Edit(DishViewModel data)
            {
                if (data == null)
                {
                    return View("Error", new ErrorViewModel());
                }
                if (ModelState.IsValid)
                {
                    var result = bl.EditDish(new Dish
                    {
                        Name = data.Name,
                        Description = data.Description,
                        Price = data.Price,
                        Categ = data.Categ
                    }
                    );
                    if (result.Success)
                    {
                        return RedirectToAction("Index");
                    }
                }

                return View();
            }

            public IActionResult Delete(int id)
            {
                
                var model = bl.GetDishById(id);
               
                return View(model);
            }

            [HttpPost]
            public IActionResult Delete(Dish data)
            {
                var dshToDelete = bl.GetDishById(data.Id);

                var result = bl.DeleteDish(dshToDelete);
                if (result.Success)
                {
                    return RedirectToAction("Index");
                }

                return View();
            }
        }
   
}
