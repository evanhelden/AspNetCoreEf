using ChefsRegistry.Controllers;
using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChefsRegistry.Repository
{
    public class ChefRepository : IChefRepository
    {
        private readonly AppDbContext _context;
        private readonly ILogger<HomeController> _logger;
        private readonly LogEventsRepository _logInfoRepository;

        public ChefRepository(AppDbContext context, ILogger<HomeController> logger, LogEventsRepository logInfoRepo)
        {
            _context = context;
            _logger = logger;
            _logInfoRepository = logInfoRepo;
        }

        /// <summary>
        /// Gets a list of all Chefs in the database using EntityFramework Core Linq
        /// </summary>
        /// <returns>IEnumerable<ChefModel></returns>
        public IEnumerable<ChefModel> GetAll()
        {
            try
            {
                _context.tbl_Chefs.ToList();
                _logInfoRepository.LogInformation("Chef Repository GetAll method called", "Success", "Information");
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Chef Repository GetAll method error");
            }
            return _context.tbl_Chefs;            
        }

        /// <summary>
        /// Gets a Chef by primary key ID from the Chefs table using EntityFramework Core Linq
        /// </summary>
        /// <param name="id"></param>
        /// <returns>ChefModel</returns>
        public ChefModel GetById(int id)
        {
            var chef = new ChefModel();
            try
            {
                chef = _context.tbl_Chefs.Find(id);
                _logInfoRepository.LogInformation("Chef Repository GetById method called", "Success", "Information");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chef Repository GetById method error");
            }

            return chef;
        }

        /// <summary>
        /// Adds a Chef name to the database using EntityFramework Core Linq
        /// </summary>
        /// <param name="chef"></param>
        public void Add(ChefModel chef)
        {
            try
            {
                _context.tbl_Chefs.Add(chef);
                _context.SaveChanges();
                _logInfoRepository.LogInformation("Chef Repository Add method called", "Success", "Information");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chef Repository Add method error");
            }
        }

        /// <summary>
        /// Updates and existing chef in the database using EntityFramework Core Linq
        /// </summary>
        /// <param name="chef"></param>
        public void Update(ChefModel chef)
        {
            try
            {
                _context.tbl_Chefs.Update(chef);
                _context.SaveChanges();
                _logInfoRepository.LogInformation("Chef Repository Update method called", "Success", "Information");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chef Repository Update method error");
            }
        }

        /// <summary>
        /// Deletes an existing chef in the database using EntityFramework Core Linq
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            try
            {
                var chef = _context.tbl_Chefs.Find(id);
                if (chef != null)
                {
                    var param = new SqlParameter("@ChefID", id);
                    _context.Database.ExecuteSqlRawAsync("EXEC sp_DeleteChef @ChefID", param);
                }
                else
                {
                    _logInfoRepository.LogInformation("Chef Repository Delete method called, Chef not found.  ID: " + id.ToString(), "Warning", "Information");
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chef Repository Delete method error");
            }
        }

        /// <summary>
        /// Adds a chef to the database using EntityFramework Core Linq
        /// </summary>
        /// <param name="chef"></param>
        public void AddChef(ChefModel chef)
        {
            try
            {
                _context.tbl_Chefs.Add(chef);
                _context.SaveChanges();
                _logInfoRepository.LogInformation("Chef Repository AddChef method called for chef last name" + chef.LastName, "Success", "Information");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Chef Repository AddChef method error");
            }
        }
    }
}
