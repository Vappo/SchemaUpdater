using System;
using System.Collections.Generic;

namespace SchemaUpdater.Types
{
    public interface IDbTypeMap
    {
        IReadOnlyDictionary<Type, string> DbTypeMap { get; }
    }
}