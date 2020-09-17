using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Core.Caching
{
    public static class CacheKey
    {
        // we should deeply look into it.
        public static string GenerateCacheName(object referenceObject, string prefix)
        {
            var sb = new StringBuilder(prefix);

            foreach (PropertyInfo propInfo in referenceObject.GetType().GetProperties())
            {
                if (propInfo.CanRead)
                {
                    object value = propInfo.GetValue(referenceObject);
                    if (value != null)
                    {
                        if (value is string || value.GetType().IsValueType)
                        {
                            sb.Append("_" + value);
                        }
                        else
                        {
                            sb.Append("_-");
                        }
                    }
                    else
                    {
                        sb.Append("_-");
                    }
                }
            }
            return sb.ToString();
        }
    }
}
