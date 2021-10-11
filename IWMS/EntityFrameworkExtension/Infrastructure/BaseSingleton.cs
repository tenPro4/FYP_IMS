using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkExtension.Infrastructure
{
    public class BaseSingleton
    {
        static BaseSingleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }

        public static IDictionary<Type, object> AllSingletons { get; }
    }
}
