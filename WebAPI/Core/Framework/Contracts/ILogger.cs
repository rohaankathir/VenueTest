namespace Core.Framework.Contracts
{
    public interface ILogger
    {
        void InsertLog(LogLevel logLevel, string fullMessage);
    }
    public enum LogLevel
    {
        Debug = 10,
        Information = 20,
        Warning = 30,
        Error = 40,
        Fatal = 50
    }
}
