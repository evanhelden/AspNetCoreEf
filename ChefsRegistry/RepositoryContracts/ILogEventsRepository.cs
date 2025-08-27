using ChefsRegistry.Models;

namespace ChefsRegistry.RepositoryContracts
{
    public interface ILogEventsRepository
    {
        IEnumerable<LogEventsModel> GetAll();
        void LogInformation(string Message, string MessageTemplate, string Level);
    }
}
