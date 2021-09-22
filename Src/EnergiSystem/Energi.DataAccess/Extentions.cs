using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Energi.DataAccess.MongoDB;
using Energi.DataAccess.MongoDB;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Energi.DataAccess
{
    public static class Extentions
    {
        public static async Task ForEachAsync<T>(this Task<IReadOnlyCollection<T>> list, Func<T, Task> func)
        {
            foreach (var value in list.Result)
            {
                await func(value);
            }
        }
    }
}
