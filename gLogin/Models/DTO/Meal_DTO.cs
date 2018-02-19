using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gLogin.Models.DTO
{
    public class Meal_DTO : Meal
    {
        public bool IsOrderd { get; set; }
        public string Cell { get; set; }

    }
}