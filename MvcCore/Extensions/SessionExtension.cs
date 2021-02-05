using Microsoft.AspNetCore.Http;
using MvcCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCore.Extensions
{
    public static class SessionExtension
    {
        public static void SetObject(this ISession session, String key, object value)
        {
            String data = Toolkit.SerializaJsonObject(value);
            session.SetString(key, data);
        }
        public static T GetObject<T>(this ISession session, String key)
        {
            String data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return Toolkit.JsonToObject<T>(data);
        }

        
    }
}
