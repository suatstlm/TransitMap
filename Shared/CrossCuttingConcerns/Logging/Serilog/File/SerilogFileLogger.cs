using CrossCuttingConcerns.Logging.Configurations;
using Serilog;
using Serilog.Events;

namespace CrossCuttingConcerns.Logging.Serilog.File;
public class SerilogFileLogger : SerilogLoggerServiceBase
{
    public SerilogFileLogger(FileLogConfiguration configuration)
        : base(null)
    {
        base.Logger = new LoggerConfiguration().WriteTo.File(Directory.GetCurrentDirectory() + configuration.FolderPath + ".txt", LogEventLevel.Verbose, "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}", null, 5000000L, null, buffered: false, shared: false, null, RollingInterval.Day, rollOnFileSizeLimit: false, null).CreateLogger();
    }
}