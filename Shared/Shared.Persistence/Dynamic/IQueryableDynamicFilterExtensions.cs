﻿using Shared.Persistence.Repositories;
using System.Linq.Dynamic.Core;
using System.Text;

namespace Shared.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly string[] _orders = new string[2] { "asc", "desc" };

    private static readonly string[] _logics = new string[2] { "and", "or" };

    private static readonly IDictionary<string, string> _operators = new Dictionary<string, string>
    {
        { "eq", "=" },
        { "neq", "!=" },
        { "lt", "<" },
        { "lte", "<=" },
        { "gt", ">" },
        { "gte", ">=" },
        { "isnull", "== null" },
        { "isnotnull", "!= null" },
        { "startswith", "StartsWith" },
        { "endswith", "EndsWith" },
        { "contains", "Contains" },
        { "doesnotcontain", "Contains" }
    };

    public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicQuery dynamicQuery)
    {
        if (dynamicQuery.Filter != null)
        {
            query = Filter(query, dynamicQuery.Filter);
        }

        if (dynamicQuery.Sort != null && dynamicQuery.Sort.Any())
        {
            query = Sort(query, dynamicQuery.Sort);
        }

        return query;
    }

    private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
    {
        IList<Filter> allFilters = GetAllFilters(filter);
        string[] array = allFilters.Select((Filter f) => f.Value).ToArray();
        string text = Transform(filter, allFilters);
        if (!string.IsNullOrEmpty(text) && array != null)
        {
            IQueryable<T> source = queryable;
            object[] args = array;
            queryable = source.Where(text, args);
        }

        return queryable;
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        foreach (Sort item in sort)
        {
            if (string.IsNullOrEmpty(item.Field))
            {
                throw new ArgumentException("Invalid Field");
            }

            if (string.IsNullOrEmpty(item.Dir) || !_orders.Contains(item.Dir))
            {
                throw new ArgumentException("Invalid Order Type");
            }
        }

        if (sort.Any())
        {
            string ordering = string.Join(",", sort.Select((Sort s) => s.Field + " " + s.Dir));
            return queryable.OrderBy(ordering);
        }

        return queryable;
    }

    public static IList<Filter> GetAllFilters(Filter filter)
    {
        List<Filter> list = new List<Filter>();
        GetFilters(filter, list);
        return list;
    }

    private static void GetFilters(Filter filter, IList<Filter> filters)
    {
        filters.Add(filter);
        if (filter.Filters == null || !filter.Filters.Any())
        {
            return;
        }

        foreach (Filter filter2 in filter.Filters)
        {
            GetFilters(filter2, filters);
        }
    }

    public static string Transform(Filter filter, IList<Filter> filters)
    {
        IList<Filter> filters2 = filters;
        if (string.IsNullOrEmpty(filter.Field))
        {
            throw new ArgumentException("Invalid Field");
        }

        if (string.IsNullOrEmpty(filter.Operator) || !_operators.ContainsKey(filter.Operator))
        {
            throw new ArgumentException("Invalid Operator");
        }

        int num = filters2.IndexOf(filter);
        string text = _operators[filter.Operator];
        StringBuilder stringBuilder = new StringBuilder();
        if (!string.IsNullOrEmpty(filter.Value))
        {
            if (filter.Operator == "doesnotcontain")
            {
                StringBuilder stringBuilder2 = stringBuilder;
                StringBuilder stringBuilder3 = stringBuilder2;
                StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(11, 3, stringBuilder2);
                handler.AppendLiteral("(!np(");
                handler.AppendFormatted(filter.Field);
                handler.AppendLiteral(").");
                handler.AppendFormatted(text);
                handler.AppendLiteral("(@");
                handler.AppendFormatted(num.ToString());
                handler.AppendLiteral("))");
                stringBuilder3.Append(ref handler);
            }
            else
            {
                bool flag;
                switch (text)
                {
                    case "StartsWith":
                    case "EndsWith":
                    case "Contains":
                        flag = true;
                        break;
                    default:
                        flag = false;
                        break;
                }

                if (flag)
                {
                    StringBuilder stringBuilder2 = stringBuilder;
                    StringBuilder stringBuilder4 = stringBuilder2;
                    StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(10, 3, stringBuilder2);
                    handler.AppendLiteral("(np(");
                    handler.AppendFormatted(filter.Field);
                    handler.AppendLiteral(").");
                    handler.AppendFormatted(text);
                    handler.AppendLiteral("(@");
                    handler.AppendFormatted(num.ToString());
                    handler.AppendLiteral("))");
                    stringBuilder4.Append(ref handler);
                }
                else
                {
                    StringBuilder stringBuilder2 = stringBuilder;
                    StringBuilder stringBuilder5 = stringBuilder2;
                    StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(7, 3, stringBuilder2);
                    handler.AppendLiteral("np(");
                    handler.AppendFormatted(filter.Field);
                    handler.AppendLiteral(") ");
                    handler.AppendFormatted(text);
                    handler.AppendLiteral(" @");
                    handler.AppendFormatted(num.ToString());
                    stringBuilder5.Append(ref handler);
                }
            }
        }
        else
        {
            string @operator = filter.Operator;
            if ((@operator == "isnull" || @operator == "isnotnull") ? true : false)
            {
                StringBuilder stringBuilder2 = stringBuilder;
                StringBuilder stringBuilder6 = stringBuilder2;
                StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(5, 2, stringBuilder2);
                handler.AppendLiteral("np(");
                handler.AppendFormatted(filter.Field);
                handler.AppendLiteral(") ");
                handler.AppendFormatted(text);
                stringBuilder6.Append(ref handler);
            }
        }

        if (filter.Logic != null && filter.Filters != null && filter.Filters.Any())
        {
            if (!_logics.Contains<string>(filter.Logic))
            {
                throw new ArgumentException("Invalid Logic");
            }

            return $"{stringBuilder} {filter.Logic} ({string.Join(" " + filter.Logic + " ", filter.Filters.Select((Filter f) => Transform(f, filters2)).ToArray())})";
        }

        return stringBuilder.ToString();
    }
}