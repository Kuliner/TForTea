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

            App.Current.UnhandledException += Current_UnhandledException;
        }

        private void Current_UnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            this.logger.Error(e.Exception.StackTrace);
        }

        public ILogger Logger
        {
            get { return this.logger; }
            set { this.logger = value; }
        }
    }
}