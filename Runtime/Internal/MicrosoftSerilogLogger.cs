using System;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Byrniee.Serilog4Unity.Internal
{
    /// <summary>
    /// Serilog logger implementation.
    /// </summary>
    /// <typeparam name="T">Logger type.</typeparam>
    public class MicrosoftSerilogLogger<T> : SerilogLogger<T>, ILogger<T>
    {
        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    LogTrace(formatter.Invoke(state, exception));
                    break;
                case LogLevel.Debug:
                    LogDebug(formatter.Invoke(state, exception));
                    break;
                case LogLevel.Information:
                    LogInformation(formatter.Invoke(state, exception));
                    break;
                case LogLevel.Warning:
                    LogWarning(formatter.Invoke(state, exception));
                    break;
                case LogLevel.Error:
                    LogError(formatter.Invoke(state, exception));
                    break;
                case LogLevel.Critical:
                    LogCritical(formatter.Invoke(state, exception));
                    break;
                case LogLevel.None:
                    LogTrace(formatter.Invoke(state, exception));
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            switch (logLevel)
            {
                case LogLevel.Trace:
                    return Log.IsEnabled(LogEventLevel.Verbose);
                case LogLevel.Debug:
                    return Log.IsEnabled(LogEventLevel.Debug);
                case LogLevel.Information:
                    return Log.IsEnabled(LogEventLevel.Information);
                case LogLevel.Warning:
                    return Log.IsEnabled(LogEventLevel.Warning);
                case LogLevel.Error:
                    return Log.IsEnabled(LogEventLevel.Error);
                case LogLevel.Critical:
                    return Log.IsEnabled(LogEventLevel.Fatal);
                case LogLevel.None:
                    return Log.IsEnabled(LogEventLevel.Verbose);
                default:
                    throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null);
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }
    }
}
