using ChefsRegistry.Models;
using ChefsRegistry.ModelsContracts;
using ChefsRegistry.Repository;
using ChefsRegistry.RepositoryContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ChefsRegistry.Controllers
{
    public class RegistryController : Controller
    {

        private readonly IRegistryRepository _regRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly LogEventsRepository _logInfoRepository;



        public RegistryController(IRegistryRepository regRepo, ILogger<HomeController> logger, LogEventsRepository logInfoRepo)
        {
            _regRepo = regRepo;
            _logger = logger;
            _logInfoRepository = logInfoRepo;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult ViewChef()
        {
            return View();
        }

        /// <summary>
        /// Calls the Create Chef stored procedure which populates the Chefs, Address, Restaurant and Phone tables 
        /// using EntityFramework Core and User Defined Table (UDTT) and Stored Procedure
        /// </summary>
        /// <param name="cm"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUDT(RegistryViewModel cm)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("RegistryController CreateUDT method error:  ModelState is not valid. " + GetModelStateErrors(ModelState));
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _regRepo.AddUDT(cm);
                _logInfoRepository.LogInformation("RegistryController CreateUDT method called", "Chef Last Name: " + cm.Chef.LastName, "Information");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegistryController CreateUDT method error");
            }
            return RedirectToAction("Index", "Chef");
        }

        /// <summary>
        /// Displays the Chef data in a read only screen.  Retrieves the Chefs, Address, Restaurant and Phone tables 
        /// using EntityFramework Core and User Defined Table (UDTT) and Stored Procedure
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Register/ViewChef/{id}")]
        public IActionResult ViewChef(int ID)
        {
            try
            {
                var chef = _regRepo.GetChef(ID);
                _logInfoRepository.LogInformation("RegistryController Create method called", "Chef Last Name: " + chef.LastName, "Information");
                return View(chef);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegistryController View method error: ");
            }

            return View("ViewChef");
        }

        /// <summary>
        /// Displays the Chef data in the update screen.  Retrieves the Chefs, Address, Restaurant and Phone tables 
        /// using EntityFramework Core and User Defined Table (UDTT) and Stored Procedure
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cm"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult UpdateChef(int ID, RegistryChefModel cm, string action)
        {
            try
            {
                TempData["RecordId"] = ID;
                var chef = _regRepo.GetChef(ID);
                _logInfoRepository.LogInformation("RegistryController UpdateChef method called", "Chef Last Name: " + cm.LastName, "Information");
                return View(chef);                               
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegistryController UpdateChef method error");
            }

            return View("UpdateChef");
        }

        /// <summary>
        /// Updates the Chefs, Address, Restaurant and Phone tables 
        /// using EntityFramework Core and User Defined Table (UDTT) and Stored Procedure
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="cm"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Update(RegistryChefModel cm, string action)
        {

            try
            {
                _regRepo.UpdateChef(cm, (int)TempData["RecordId"]);
                _logInfoRepository.LogInformation("RegistryController Update method called", "Chef Last Name: " + cm.LastName, "Information");
                return RedirectToAction("Index", "Chef");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RegistryController UpdateChef method error");
            }

            return View("UpdateChef");
        }

        /// <summary>
        /// Gets a list of Errors from the ModelState object when ModelState.IsValid returns false
        /// </summary>
        /// <param name="ModelState"></param>
        /// <returns></returns>
        string GetModelStateErrors(ModelStateDictionary ModelState)
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            string errorMessage = "";
            foreach (var error in errors)
            {
                errorMessage += error.ErrorMessage + " | ";
            }
            return errorMessage;
        }

    }
}
