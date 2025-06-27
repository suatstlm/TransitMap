using Shared.Localizations.Abstraction;
using System.Data;
using YamlDotNet.RepresentationModel;

namespace Shared.Localizations.Resource.Yaml;
public class ResourceLocalizationManager : ILocalizationService
{
    private const string _defaultLocale = "en";

    private const string _defaultKeySection = "index";

    private readonly Dictionary<string, Dictionary<string, (string path, YamlMappingNode? content)>> _resourceData = new Dictionary<string, Dictionary<string, (string, YamlMappingNode)>>();

    public ICollection<string>? AcceptLocales { get; set; }

    public ResourceLocalizationManager(Dictionary<string, Dictionary<string, string>> resources)
    {
        foreach (KeyValuePair<string, Dictionary<string, string>> resource in resources)
        {
            resource.Deconstruct(out var key, out var value);
            string key2 = key;
            Dictionary<string, string> dictionary = value;
            if (!_resourceData.ContainsKey(key2))
            {
                _resourceData.Add(key2, new Dictionary<string, (string, YamlMappingNode)>());
            }

            foreach (KeyValuePair<string, string> item2 in dictionary)
            {
                item2.Deconstruct(out key, out var value2);
                string key3 = key;
                string item = value2;
                _resourceData[key2].Add(key3, (item, null));
            }
        }
    }

    public Task<string> GetLocalizedAsync(string key, string? keySection = null)
    {
        return GetLocalizedAsync(key, AcceptLocales ?? throw new NoNullAllowedException("AcceptLocales"), keySection);
    }

    public Task<string> GetLocalizedAsync(string key, ICollection<string> acceptLocales, string? keySection = null)
    {
        string localizationFromResource;
        if (acceptLocales != null)
        {
            foreach (string acceptLocale in acceptLocales)
            {
                localizationFromResource = getLocalizationFromResource(key, acceptLocale, keySection);
                if (localizationFromResource != null)
                {
                    return Task.FromResult(localizationFromResource);
                }
            }
        }

        localizationFromResource = getLocalizationFromResource(key, "en", keySection);
        if (localizationFromResource != null)
        {
            return Task.FromResult(localizationFromResource);
        }

        return Task.FromResult(key);
    }

    private string? getLocalizationFromResource(string key, string locale, string? keySection = "index")
    {
        if (string.IsNullOrWhiteSpace(keySection))
        {
            keySection = "index";
        }

        if (_resourceData.TryGetValue(locale, out Dictionary<string, (string, YamlMappingNode)> value) && value.TryGetValue(keySection, out var value2))
        {
            if (value2.Item2 == null)
            {
                lazyLoadResource(value2.Item1, out value2.Item2);
            }

            if (value2.Item2.Children.TryGetValue(new YamlScalarNode(key), out YamlNode value3))
            {
                return value3.ToString();
            }
        }

        return null;
    }

    private void lazyLoadResource(string path, out YamlMappingNode? content)
    {
        using StreamReader input = new StreamReader(path);
        YamlStream yamlStream = new YamlStream();
        yamlStream.Load(input);
        content = (YamlMappingNode)yamlStream.Documents[0].RootNode;
    }
}