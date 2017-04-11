namespace TForTea.Managers
{
    using TForTea.Repositories.Services;

    public class DatabaseManager
    {
        private LogManager logManager;

        public DatabaseManager(LogManager logManager)
        {
            this.logManager = logManager;
            this.TeaService = new TeaDBService(logManager);
        }

        public TeaDBService TeaService { get; set; }
    }
}