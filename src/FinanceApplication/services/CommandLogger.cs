namespace FinanceApplication.services
{
    public class CommandLogger
    {
        private readonly string _logFilePath;

        public CommandLogger(string logFilePath = "logs.txt")
        {
            _logFilePath = logFilePath;
        }

        public void LogCommand(string action)
        {
            string logMessage = $"{DateTime.Now} - {action}\n";
            File.AppendAllText(_logFilePath, logMessage);
        }
    }
}