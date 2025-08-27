using ChefsRegistry.ModelsContracts;
using ChefsRegistry.Repository;

namespace ChefsRegistry.Models
{
    public class LogEventsModel : ILogEventsModel
    {
        private AppDbContext @object;


        public LogEventsModel() { }

        public LogEventsModel(AppDbContext @object)
        {
            this.@object = @object;
        }
        public int ID { get; set; }
        public string Message { get; set; }
        public string? MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Exception { get; set; }
        public string? Properties { get; set; }

    }
}
