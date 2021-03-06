﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationModule5Tp3.Model.BO;

namespace WebApplicationModule5Tp3.Model.BO
{
    public class PizzaViewModel
    {
        public Pizza Pizza { get; set; }
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

        public int IdPate { get; set; }
        public List<int> IdsIngredients { get; set; } = new List<int>();
        
    }
}