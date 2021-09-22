﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Energi.DataAccess.MongoDB
{
    public class MongoDbSettings
    {
        public string Host { get; init; }
        public int Port { get; init; }
        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}
