﻿namespace Shared.ElasticSearch.Models;
public class ElasticSearchGetModel<T>
{
    public string ElasticId { get; set; }

    public T Item { get; set; }

    public ElasticSearchGetModel()
    {
        ElasticId = string.Empty;
        Item = default(T);
    }

    public ElasticSearchGetModel(string elasticId, T item)
    {
        ElasticId = elasticId;
        Item = item;
    }
}
