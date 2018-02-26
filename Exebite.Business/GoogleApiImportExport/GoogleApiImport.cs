using Exebite.Model;
using Exebite.GoogleSpreadsheetApi.RestaurantConectorsInterfaces;
using GoogleSpreadsheetApi.Kasa;
using System.Linq;

namespace Exebite.Business.GoogleApiImportExport
{
    public class GoogleApiImport : IGoogleDataImporter
    {
        private IRestarauntService _restarauntService;
        //conectors
        ILipaConector _lipaConector;
        IHedoneConector _hedoneConector;
        ITeglasConector _teglasConector;
        IKasaConector _kasaConector;
        ICustomerService _customerService;
        
        public GoogleApiImport(IRestarauntService restarauntService, ICustomerService customerService,
            ILipaConector lipaConector, ITeglasConector teglasConector, IHedoneConector hedoneConector, IKasaConector kasaConector)
        {
            _restarauntService = restarauntService;
            //conectors to a new sheets
            _lipaConector = lipaConector;
            _hedoneConector = hedoneConector;
            _teglasConector = teglasConector;
            _kasaConector = kasaConector;
            _customerService = customerService;
        }
        
        /// <summary>
        /// Update daily menu for restorants
        /// </summary>
        public void UpdateRestorauntsMenu()
        {
            Restaurant lipaRestoraunt = _restarauntService.GetRestaurantByName("Restoran pod Lipom");
            Restaurant hedoneRestoraunt = _restarauntService.GetRestaurantByName("Hedone");
            Restaurant indexHauseRestoraunt = _restarauntService.GetRestaurantByName("Index House");
            Restaurant teglasRestoraunt = _restarauntService.GetRestaurantByName("Teglas");
            Restaurant extraFoodRestoraunt = _restarauntService.GetRestaurantByName("Extra food");

            // Get daily menu and update info in database
            //Lipa
            lipaRestoraunt.Foods = _lipaConector.LoadAllFoods();
            _restarauntService.UpdateRestourant(lipaRestoraunt);
            lipaRestoraunt.DailyMenu = _lipaConector.GetDailyMenu();
            _restarauntService.UpdateRestourant(lipaRestoraunt);
            //Teglas
            teglasRestoraunt.Foods = _teglasConector.LoadAllFoods();
            _restarauntService.UpdateRestourant(teglasRestoraunt);
            teglasRestoraunt.DailyMenu = _teglasConector.GetDailyMenu();
            _restarauntService.UpdateRestourant(teglasRestoraunt);
            //Hedone
            hedoneRestoraunt.Foods = _hedoneConector.LoadAllFoods();
            _restarauntService.UpdateRestourant(hedoneRestoraunt);
            hedoneRestoraunt.DailyMenu = _hedoneConector.GetDailyMenu();
            _restarauntService.UpdateRestourant(hedoneRestoraunt);

            //Index and extra food to be implemented

            //indexHauseRestoraunt.DailyMenu = _indexHouse.GetDailyMenu();
            //_restarauntService.UpdateRestourant(indexHauseRestoraunt);
            
            //extraFoodRestoraunt.DailyMenu = _extraFood.GetDailyMenu();
            //_restarauntService.UpdateRestourant(extraFoodRestoraunt);
            
        }

        /// <summary>
        /// Imports users from kasa sheet
        /// </summary>
        public void ImportUsersFromKasa()
        {
            var customerListSheet = _kasaConector.GetCustomersFromKasa();
            var customerListDB = _customerService.GetAllCustomers();

            foreach(var customer in customerListSheet)
            {
                if(customerListDB.FirstOrDefault(u => u.Name == customer.Name) == null)
                {
                    _customerService.CreateCustomer(customer);
                }
            }

        }
    }
}
