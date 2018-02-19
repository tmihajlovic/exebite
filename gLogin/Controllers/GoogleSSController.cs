using gLogin.Google;
using gLogin.Models;
using gLogin.Models.DTO;
using gLogin.Shared;
using GoogleSpreadsheetApi;
using GoogleSpreadsheetApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gLogin.Controllers
{
    public class GoogleSSController : Controller
    {
       // IGoogleSpreadsheetServiceFactory service;

        public GoogleSSController()
        {
            //this.service = service;
        }


        //// GET: GoogleSS
        //public ActionResult GoogleSS()
        //{
        //    var currentLoggedUser = User.Identity.Name;
        //    var users = service.GetUsers();
            
        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);
            
        //    var mealData = service.GetSSData();            

        //    var todayDate = "06-Feb-2018"; //DateTime.Today.ToString("dd-MMM-yyyy");
        //    //var menuForToday = mealData.Where(m => m.Value.Date == todayDate);

        //    Dictionary<string, FoodSpreadsheet> menuForToday = new Dictionary<string, FoodSpreadsheet>();

        //    foreach (var item in mealData)
        //    {
        //        if(item.Value.Date == todayDate)
        //        {
        //            menuForToday[item.Key] = item.Value;
        //        }
        //    }

        //    foreach(var meal in menuForToday)
        //    {
        //        meal.Value.IsOrderd = service.IsOrderd(user.Key.ToString(), meal.Key);
        //    }
            

        //    return View(menuForToday);
        //}

        ////GET: Place order
        //public ActionResult PlaceOrder(string cell)
        //{
        //    var currentLoggedUser = User.Identity.Name;

        //    var users = service.GetUsers();
            
        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);

        //    service.PlaceOrder(user.Key.ToString(), cell);

        //    return RedirectToAction("GoogleSS");
        //}
        ////GET: Cancel order
        //public ActionResult CancelOrder(string cell)
        //{
        //    var currentLoggedUser = User.Identity.Name;
        //    var users = service.GetUsers();

        //    var user = users.FirstOrDefault(u => u.Value == currentLoggedUser);
        //    service.CancelOrder(user.Key.ToString(), cell);

        //    return RedirectToAction("GoogleSS");
        //}
    }
}