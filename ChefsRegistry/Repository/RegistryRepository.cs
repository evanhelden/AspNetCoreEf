using ChefsRegistry.Controllers;
using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient.Server;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ChefsRegistry.Repository
{
    public class RegistryRepository : IRegistryRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<RegistryRepository> _logger;
        private readonly LogEventsRepository _logInfoRepository;

        public RegistryRepository(AppDbContext context, IConfiguration configuration, ILogger<RegistryRepository> logger, LogEventsRepository logInfoRepo)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
            _logInfoRepository = logInfoRepo;
        }

        /// <summary>
        /// Repository method calls CreateChef stored procedure passing in UDTT created from RegistryChefModel
        /// </summary>
        /// <param name="chef"></param>
        public void AddUDT(RegistryViewModel chef)
        {
            try
            {
                List<SqlDataRecord> records = new List<SqlDataRecord>();
                var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));

                using (connection)
                {
                    connection.Open();
                    SqlDataRecord chefRec = new SqlDataRecord(
                    new SqlMetaData("FirstName", SqlDbType.NVarChar, 30),
                    new SqlMetaData("LastName", SqlDbType.NVarChar, 50),
                    new SqlMetaData("MiddleName", SqlDbType.NVarChar, 50),
                    new SqlMetaData("Name", SqlDbType.NVarChar, 20),
                    new SqlMetaData("StreetAddress", SqlDbType.NVarChar, 50),
                    new SqlMetaData("CityTownVillage", SqlDbType.NVarChar, 50),
                    new SqlMetaData("PostalZipCode", SqlDbType.NVarChar, 50),
                    new SqlMetaData("StateProvinceRegion", SqlDbType.NVarChar, 50),
                    new SqlMetaData("Number", SqlDbType.NVarChar, 20));

                    chefRec.SetString(0, chef.Chef.FirstName);
                    chefRec.SetString(1, chef.Chef.LastName);
                    chefRec.SetString(2, chef.Chef.ChefNumber);
                    chefRec.SetString(3, chef.Restaurant.Name);
                    chefRec.SetString(4, chef.Address.StreetAddress);
                    chefRec.SetString(5, chef.Address.CityTownVillage);
                    chefRec.SetString(6, chef.Address.PostalZipCode);
                    chefRec.SetString(7, chef.Address.StateProvinceRegion);
                    chefRec.SetString(8, chef.Phone.Number);
                    records.Add(chefRec);

                    var command = new SqlCommand("CreateChef", connection);

                    using (command)
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter parameter = command.Parameters.AddWithValue("@RegModel", records);
                        parameter.SqlDbType = SqlDbType.Structured;
                        parameter.TypeName = "dbo.RegistryModel";

                        command.ExecuteNonQuery();
                        _logInfoRepository.LogInformation("Registry Repository CreateChef method called", "Success for Chef Last Name: " + chef.Chef.LastName, "Information");

                    }
                }
            }
            catch (Exception ex)            {
                _logger.LogError(ex, "Registry Repository CreateChef method error");
            }         
        }

        /// <summary>
        /// Repository method calls CreateChef stored procedure passing in UDTT created from RegistryChefModel
        /// </summary>
        /// <param name="chef"></param>
        public void Add(RegistryViewModel chef)
        {
            var sqlParams = new[]
            {
                new SqlParameter("@FirstName", chef.Chef.FirstName),
                new SqlParameter("@LastName", chef.Chef.LastName),
                new SqlParameter("@MiddleName", chef.Chef.ChefNumber),
                new SqlParameter("@Name", chef.Restaurant.Name),
                new SqlParameter("@StreetAddress",chef.Address.StreetAddress ),
                new SqlParameter("@CityTownVillage", chef.Address.CityTownVillage),
                new SqlParameter("@PostalZipCode", chef.Address.PostalZipCode),
                new SqlParameter("@StateProvinceRegion", chef.Address.StateProvinceRegion),
                new SqlParameter("@Number", chef.Phone.Number)
            };

            try
            {
                var query = "EXEC CreateChef @FirstName, @LastName, @MiddleName, @Name, @StreetAddress, @CityTownVillage, @PostalZipCode, @StateProvinceRegion, @Number";
                _context.Database.ExecuteSqlRaw(query, sqlParams);
                _context.SaveChanges();
                _logInfoRepository.LogInformation("Registry Repository Add method called", "Success for Chef Last Name: " + chef.Chef.LastName, "Information");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registry Repository Add method error");
            }
        }

        /// <summary>
        /// Gets the Chef data based in primary key ID returning a UDTT 
        /// </summary>
        /// <param name="ChefID"></param>
        /// <returns></returns>
        public RegistryChefModel GetChef(int ChefID)     
        {            
            try
            {
                var param = new SqlParameter("@ChefID", SqlDbType.Int)
                {
                    Value = ChefID
                };
                var results = _context.RegistryChefModels
                    .FromSqlRaw("EXEC dbo.GetChef @ChefID", param)
                    .AsNoTracking()
                    .ToList();

                return results.FirstOrDefault();

            }
            catch (Exception ex)
            {
                _logInfoRepository.LogInformation("Registry Repository Add method called",ex.Message.ToString(), "Information");

                _logger.LogError(ex, "Registry Repository Add method error");

                return null;
            }
        }

        /// <summary>
        /// Updates the Chefs information using a UDTT (RegistryChefModel)
        /// </summary>
        /// <param name="chef"></param>
        /// <param name="ID"></param>
        public void UpdateChef(RegistryChefModel chef, int ID)
        {
            try
            {
                List<SqlDataRecord> records = new List<SqlDataRecord>();
                var connection = new SqlConnection(_configuration.GetConnectionString("SqlConnection"));

                using (connection)
                {
                    connection.Open();
                    SqlDataRecord chefRec = new SqlDataRecord(
                    new SqlMetaData("FirstName", SqlDbType.NVarChar, 30),
                    new SqlMetaData("LastName", SqlDbType.NVarChar, 50),
                    new SqlMetaData("MiddleName", SqlDbType.NVarChar, 50),
                    new SqlMetaData("Name", SqlDbType.NVarChar, 20),
                    new SqlMetaData("StreetAddress", SqlDbType.NVarChar, 50),
                    new SqlMetaData("CityTownVillage", SqlDbType.NVarChar, 50),
                    new SqlMetaData("PostalZipCode", SqlDbType.NVarChar, 50),
                    new SqlMetaData("StateProvinceRegion", SqlDbType.NVarChar, 50),
                    new SqlMetaData("Number", SqlDbType.NVarChar, 20));

                    chefRec.SetString(0, chef.FirstName);
                    chefRec.SetString(1, chef.LastName);
                    chefRec.SetString(2, chef.ChefNumber);
                    chefRec.SetString(3, chef.Name);
                    chefRec.SetString(4, chef.StreetAddress);
                    chefRec.SetString(5, chef.CityTownVillage);
                    chefRec.SetString(6, chef.PostalZipCode);
                    chefRec.SetString(7, chef.StateProvinceRegion);
                    chefRec.SetString(8, chef.Number);
                    records.Add(chefRec);

                    var command = new SqlCommand("UpdateChef", connection);

                    using (command)
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        SqlParameter parameter = command.Parameters.AddWithValue("@RegModel", records);
                        parameter.SqlDbType = SqlDbType.Structured;
                        parameter.TypeName = "dbo.RegistryModel";

                        SqlParameter parameter2 = command.Parameters.AddWithValue("@ChefID", ID);
                        parameter2.SqlDbType = SqlDbType.Int;

                        command.ExecuteNonQuery();
                        _logInfoRepository.LogInformation("Registry Repository UpdateChef method called", "Success for Chef Last Name: " + chef.LastName, "Information");
                     
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Registry Repository UpdateChef method error");
            }
        }
    }
}
