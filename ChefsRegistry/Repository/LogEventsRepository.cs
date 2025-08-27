using ChefsRegistry.Models;
using ChefsRegistry.RepositoryContracts;
using Serilog.Events;

namespace ChefsRegistry.Repository
{
    public class LogEventsRepository : ILogEventsRepository
    {
        private readonly AppDbContext _context;
        public LogEventsRepository(AppDbContext context) 
        {
            _context = context;
        }

        public IEnumerable<LogEventsModel> GetAll()
        {
            return _context.LogEvents.ToList();
        }

        /// <summary>
        /// Log Information to the LogEvents table 
        /// </summary>
        /// <param name="Message"></param>
        /// <param name="MessageTemplate"></param>
        /// <param name="Level"></param>
        public void LogInformation(string Message, string MessageTemplate, string Level)
        {
            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("yyyy-MM-dd HH:mm:ss");
            var logInfo = new Models.LogEventsModel
            {
                Message = Message,
                MessageTemplate = MessageTemplate,
                Level = Level,
                Timestamp = DateTime.UtcNow,
                Exception = null,
                Properties = null
            };
            _context.LogEvents.Add(logInfo);
            _context.SaveChanges();
        }   
    }
}
