namespace ChefsRegistry.ModelsContracts
{
    public interface ILogEventsModel
    {
        public int ID { get; set; }
        public string Message { get; set; }
        public string MessageTemplate { get; set; }
        public string Level { get; set; }
        public DateTime Timestamp { get; set; }
        public string Exception { get; set; }
        public string Properties { get; set; }
    }
}
