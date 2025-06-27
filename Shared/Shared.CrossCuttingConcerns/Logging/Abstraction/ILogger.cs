using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.CrossCuttingConcerns.Logging.Abstraction;
public interface ILogger
{
    void Trace(string message);

    void Critical(string message);

    void Information(string message);

    void Warning(string message);

    void Debug(string message);

    void Error(string message);
}
