using System;
using System.Web;
using Microsoft.Practices.Unity;

namespace Data.Injection
{
    public sealed class PerHttpRequestLifetimeManager<T> : LifetimeManager, IDisposable
    {
        private readonly string _instanceKey;

        public PerHttpRequestLifetimeManager()
        {
            _instanceKey = typeof (T).AssemblyQualifiedName;
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_instanceKey];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_instanceKey] = newValue;
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Remove(_instanceKey);
            }
        }
        
        public void Dispose()
        {
            RemoveValue();
        }
    }

    public sealed class PerHttpRequestLifetimeManager : LifetimeManager, IDisposable
    {
        private readonly string _instanceKey;

        public PerHttpRequestLifetimeManager(Type type)
        {
            _instanceKey = type.AssemblyQualifiedName;
        }

        public override object GetValue()
        {
            return HttpContext.Current.Items[_instanceKey];
        }

        public override void SetValue(object newValue)
        {
            HttpContext.Current.Items[_instanceKey] = newValue;
        }

        public override void RemoveValue()
        {
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Items.Remove(_instanceKey);
            }
        }

        public void Dispose()
        {
            RemoveValue();
        }
    }
}
