using Microsoft.AspNetCore.Mvc;
using ChefsRegistry.Models;
using ChefsRegistry.ModelsContracts;
using ChefsRegistry.Repository;
using ChefsRegistry.RepositoryContracts;

namespace ChefsRegistry.Controllers
{
    public class LogEventsController : Controller
    {
        private readonly ILogEventsRepository _logRepo;
        private readonly ILogger<HomeController> _logger;

        public LogEventsController(ILogEventsRepository logRepo, ILogger<HomeController> logger)
        {
            _logRepo = logRepo;
            _logger = logger;
        }

        /// <summary>
        /// Displays the logging from the LogEvents table using EntityFramework Core Linq
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            try
            {
                var logs = _logRepo.GetAll();
                return View(logs);
            }
            catch (Exception ex)
            {
                _logger.LogError("Log Repository Index method error: " + ex.Message.ToString());
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
