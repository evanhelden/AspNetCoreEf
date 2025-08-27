using ChefsRegistry.Models;
using ChefsRegistry.ModelsContracts;
using ChefsRegistry.Repository;
using ChefsRegistry.RepositoryContracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace ChefsRegistry.Controllers
{
    public class ChefController : Controller
    {
        private readonly IChefRepository _chefRepo;
        private readonly ILogger<HomeController> _logger;
        private readonly LogEventsRepository _logInfoRepository;

        public ChefController(IChefRepository chefRepo, ILogger<HomeController> logger, LogEventsRepository logInfoRepo)
        {
            _chefRepo = chefRepo;
            _logger = logger;
            _logInfoRepository = logInfoRepo;
        }

        /// <summary>
        /// Gets a list of all Chefs in the database using EntityFramework Core Linq
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {                
                var chef = _chefRepo.GetAll();
                _logInfoRepository.LogInformation("ChefController Index method called", "Success", "Information");
                return View(chef);
            }
            catch (Exception ex)
            {
                _logger.LogError("ChefController Index method error: " + ex.Message.ToString());
            }

            return RedirectToAction(nameof(Index));

        }

        /// <summary>
        /// Redirect to the Registry Controller for a read only view of the Chef.  
        /// The Registry Controller uses stored procedures.
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public IActionResult View(int ID)
        {
            return RedirectToAction("ViewChef", "Registry", new { ID = ID });

        }


        /// <summary>
        /// Creates a Chef in the Chef table using EntityFramework Core Linq
        /// </summary>
        /// <param name="chefModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstName,ChefNumber")] ChefModel chefModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError("ChefController Create method error: " + GetModelStateErrors(ModelState));
                }
                
                _chefRepo.Add(chefModel);
                _logInfoRepository.LogInformation("ChefController Create method called", "Success", "Information");
            }
            catch (Exception ex)
            {
                _logger.LogError("ChefController Create method error: " + ex.Message.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Redirect to the Registry Controller for editing. The Registry Controller uses stored procedures.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int ID)
        {
            return RedirectToAction("UpdateChef", "Registry", new { ID = ID });
        }

        /// <summary>
        /// Edits a Chef in the Chef table using EntityFramework Core Linq
        /// </summary>
        /// <param name="chefModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ChefModel chefModel)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("ChefController Edit method error:  ModelState is not valid. " + GetModelStateErrors(ModelState));
                return RedirectToAction(nameof(Index));
            }

            try
            {
                _logInfoRepository.LogInformation("ChefController Edit method called", "Chef Last Name: " + chefModel.LastName, "Information");
                _chefRepo.Update(chefModel);
            }
            catch (Exception ex)
            {
                _logger.LogError("ChefController Edit method error: " + ex.Message.ToString());
            }

            return RedirectToAction(nameof(Index));

        }

        /// <summary>
        /// Displays the Chef data that is to be deleted using EntityFramework Core Linq
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int id)
        {
            var chefModel = new ChefModel();
            try
            {
                chefModel = _chefRepo.GetById(id);
                _logInfoRepository.LogInformation("ChefController Delete method called", "Chef Last Name: " + chefModel.LastName, "Information");                 
            }
            catch (Exception ex)
            {
                _logger.LogError("ChefController Delete GetById method error: " + ex.Message.ToString());
            }

            return View(chefModel);
        }

        /// <summary>
        /// Deletes a Chef from the Chef table using EntityFramework Core Linq
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var ChefModel = new ChefModel();
            try
            {
                ChefModel = _chefRepo.GetById(id);
                _logInfoRepository.LogInformation("ChefController Delete method called", "Chef Last Name: " + ChefModel.LastName, "Information");
                _chefRepo.Delete(id);
            }
            catch (Exception ex)
            {
                _logger.LogError("ChefController Delete GetById method error for Chef Last Name: " + ChefModel.LastName + "  Error: " + ex.Message.ToString());
            }
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Gets a list of Errors from the ModelState object when ModelState.IsValid returns false to display in the logs
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
