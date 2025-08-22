namespace Domain.Entities.Contracts
{
    // Interface for logging operations.
    public interface ILoggerManager
    {
        void LogInfo(string message);
        void LogWarn(string message);
        void LogError(string message);
        void LogDebug(string message);
        void LogWithContext(string message, string propertyName, object value);
    }
}