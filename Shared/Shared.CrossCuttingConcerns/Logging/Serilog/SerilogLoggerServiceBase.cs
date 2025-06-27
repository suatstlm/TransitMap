
namespace Shared.CrossCuttingConcerns.Logging.Serilog;

public abstract class SerilogLoggerServiceBase : Abstraction.ILogger
{
    protected global::Serilog.ILogger? Logger { get; set; }

    protected SerilogLoggerServiceBase(global::Serilog.ILogger logger)
    {
        Logger = logger;
    }

    public void Critical(string message)
    {
        Logger?.Fatal(message);
    }

    public void Debug(string message)
    {
        Logger?.Debug(message);
    }

    public void Error(string message)
    {
        Logger?.Error(message);
    }

    public void Information(string message)
    {
        Logger?.Information(message);
    }

    public void Trace(string message)
    {
        Logger?.Verbose(message);
    }

    public void Warning(string message)
    {
        Logger?.Warning(message);
    }
}