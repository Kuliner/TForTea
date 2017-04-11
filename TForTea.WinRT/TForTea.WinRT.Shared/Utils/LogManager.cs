namespace TForTea.Managers
{
    using MetroLog;
    using MetroLog.Targets;

    public class LogManager
    {
        private ILogger logger;

        public LogManager()
        {
            LogManagerFactory.DefaultConfiguration.AddTarget(LogLevel.Trace, LogLevel.Fatal, new StreamingFileTarget());
            this.Logger = LogManagerFactory.DefaultLogManager.GetLogger<LogManager>();
        }

        public ILogger Logger
        {
            get { return this.logger; }
            set { this.logger = value; }
        }
    }
}