namespace WebAPI.MainProject.Logger
{
    using Microsoft.Extensions.Logging;
    using NLog;
    using System;
    using ILogger = Microsoft.Extensions.Logging.ILogger;
    using LogLevel = Microsoft.Extensions.Logging.LogLevel;

    public class NLogLoggerProvider : ILoggerProvider
    {
        private readonly Logger _logger;

        public NLogLoggerProvider(string configPath)
        {
            LogManager.LoadConfiguration(configPath); // Load NLog configuration
            _logger = LogManager.GetCurrentClassLogger(); // Get the logger instance
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new NLogLogger(_logger);
        }

        public void Dispose()
        {
            // Cleanup, if necessary
        }
    }

    public class NLogLogger : ILogger
    {
        private readonly Logger _logger;

        public NLogLogger(Logger logger)
        {
            _logger = logger;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            // Not implemented
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            // Map LogLevel to NLog LogLevel
            return _logger.IsEnabled(ConvertLogLevel(logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            // Log the message using NLog
            _logger.Log(ConvertLogLevel(logLevel), exception, formatter(state, exception));
        }

        private NLog.LogLevel ConvertLogLevel(LogLevel logLevel)
        {
            // Map Microsoft.Extensions.Logging.LogLevel to NLog.LogLevel
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return NLog.LogLevel.Trace;
                case LogLevel.Debug:
                    return NLog.LogLevel.Debug;
                case LogLevel.Information:
                    return NLog.LogLevel.Info;
                case LogLevel.Warning:
                    return NLog.LogLevel.Warn;
                case LogLevel.Error:
                    return NLog.LogLevel.Error;
                case LogLevel.Critical:
                    return NLog.LogLevel.Fatal;
                default:
                    return NLog.LogLevel.Info;
            }
        }
    }

}
