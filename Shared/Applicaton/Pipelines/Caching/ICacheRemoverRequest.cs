﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Applicaton.Pipelines.Caching;
public interface ICacheRemoverRequest
{
    bool BypassCache { get; }

    string? CacheKey { get; }

    string[]? CacheGroupKey { get; }
}
