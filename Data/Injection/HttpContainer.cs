using System;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Data.Injection
{
    public static class HttpContainer
    {
        public static readonly Dictionary<string, Type> RepositoryTypes = new Dictionary<string, Type>();

        public static T Get<T>()
        {
            return DependencyResolver.Current.GetService<T>();
        }

        public static object Get(Type type)
        {
            return DependencyResolver.Current.GetService(type);
        }

    }
}
