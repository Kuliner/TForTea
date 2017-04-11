namespace TForTea.Managers
{
    using TForTea.Repositories.Services;

    public class DatabaseManager
    {
        public DatabaseManager(string connectionString, LogManager logManager)
        {
            this.TeaService = new TeaDBService(connectionString, logManager);
        }

        public TeaDBService TeaService { get; set; }
    }
}