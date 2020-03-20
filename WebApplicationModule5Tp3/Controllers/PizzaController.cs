using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationModule5Tp3.Utils;
using WebApplicationModule5Tp3.Model.BO;

namespace TPModule5_2.Controllers
{
    public class PizzaController : Controller
    {
        // GET: Pizza
        public ActionResult Index()
        {
            return View(FakeDb.Instance.Pizzas);
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {
            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString()})
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            return View(vm);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaViewModel vm)
        {
            try
            {
                Pizza pizza = vm.Pizza;

                pizza.Pate = FakeDb.Instance.PatesDisponible.FirstOrDefault(x => x.Id == vm.IdPate);

                pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(
                    x => vm.IdsIngredients.Contains(x.Id))
                    .ToList();

                // Insuffisant
                //pizza.Id = FakeDb.Instance.Pizzas.Count + 1;

                

                Pizza checkPizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Nom == vm.Pizza.Nom);

                int memeIngredients = FakeDb.Instance.Pizzas.Where(x => x.Ingredients.Count() == pizza.Ingredients.Count() && !x.Ingredients.Except(pizza.Ingredients).Any()).ToList().Count();


                if (memeIngredients > 0)
                {
                    ModelState.AddModelError("", "Cette pizza existe déjà");
                }
                else if (checkPizza != null)
                {
                    ModelState.AddModelError("", "Ce nom est déjà prit");
                }
                else if (pizza.Pate == null)
                {
                    ModelState.AddModelError("", "Le paramètre Pate est obligatoire");
                }
                else if (pizza.Ingredients.Count() < 2)
                {
                    ModelState.AddModelError("", "La pizza doit contenir au moins 2 ingrédients");
                }
                else if (pizza.Ingredients.Count() > 5)
                {
                    ModelState.AddModelError("", "La pizza doit contenir au maximum 5 ingrédients");
                }
                else
                {

                    pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(x => x.Id) + 1;

                    FakeDb.Instance.Pizzas.Add(pizza);

                    return RedirectToAction("Index");
                }
                return View(vm);
            }
            catch
            {
                ModelState.AddModelError("", "Erreur de compile");
                return View(vm);
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            PizzaViewModel vm = new PizzaViewModel();

            vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                .ToList();

            vm.Pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);

            if (vm.Pizza.Pate != null)
            {
                vm.IdPate = vm.Pizza.Pate.Id;
            }

            if (vm.Pizza.Ingredients.Any())
            {
                vm.IdsIngredients = vm.Pizza.Ingredients.Select(x => x.Id).ToList();
            }

            return View(vm);
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaViewModel vm)
        {
            try
            {
                vm.Pates = FakeDb.Instance.PatesDisponible.Select(
                    x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                    .ToList();
                vm.Ingredients = FakeDb.Instance.IngredientsDisponible.Select(
                    x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() })
                    .ToList();

                if (ModelState.IsValid)
                {
                    Pizza pizza = vm.Pizza;

                    pizza.Pate = FakeDb.Instance.PatesDisponible.FirstOrDefault(x => x.Id == vm.IdPate);

                    pizza.Ingredients = FakeDb.Instance.IngredientsDisponible.Where(
                        x => vm.IdsIngredients.Contains(x.Id))
                        .ToList();

                    // Insuffisant
                    //pizza.Id = FakeDb.Instance.Pizzas.Count + 1;



                    Pizza checkPizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Nom == vm.Pizza.Nom);

                    int memeIngredients = FakeDb.Instance.Pizzas.Where(x => x.Ingredients.Count() == pizza.Ingredients.Count() && !x.Ingredients.Except(pizza.Ingredients).Any()).ToList().Count();


                    if (memeIngredients > 0)
                    {
                        ModelState.AddModelError("", "Cette pizza existe déjà");
                    }
                    else if (checkPizza != null)
                    {
                        ModelState.AddModelError("", "Ce nom est déjà prit");
                    }
                    else if (pizza.Pate == null)
                    {
                        ModelState.AddModelError("", "Le paramètre Pate est obligatoire");
                    }
                    else if (pizza.Ingredients.Count() < 2)
                    {
                        ModelState.AddModelError("", "La pizza doit contenir au moins 2 ingrédients");
                    }
                    else if (pizza.Ingredients.Count() > 5)
                    {
                        ModelState.AddModelError("", "La pizza doit contenir au maximum 5 ingrédients");
                    }
                    else
                    {

                        pizza.Id = FakeDb.Instance.Pizzas.Count == 0 ? 1 : FakeDb.Instance.Pizzas.Max(x => x.Id) + 1;

                        FakeDb.Instance.Pizzas.Add(pizza);

                        return RedirectToAction("Index");
                    }
                    return View(vm);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Erreur de compile");
                
            }
            return View(vm);
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            return View(FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id));
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                Pizza pizza = FakeDb.Instance.Pizzas.FirstOrDefault(x => x.Id == id);
                FakeDb.Instance.Pizzas.Remove(pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
