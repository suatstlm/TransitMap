using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localizations.Abstraction
{
    public interface ILocalizationService
    {
        ICollection<string>? AcceptLocales { get; set; }

        Task<string> GetLocalizedAsync(string key, string? keySection = null);

        Task<string> GetLocalizedAsync(string key, ICollection<string> acceptLocales, string? keySection = null);
    }
}
