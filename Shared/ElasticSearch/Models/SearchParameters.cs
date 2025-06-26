

namespace ElasticSearch.Models;

public class SearchParameters
{
    public string IndexName { get; set; }

    public int From { get; set; }

    public int Size { get; set; } = 10;


    public SearchParameters()
    {
        IndexName = string.Empty;
    }

    public SearchParameters(string indexName, int from, int size)
    {
        IndexName = indexName;
        From = from;
        Size = size;
    }
}
