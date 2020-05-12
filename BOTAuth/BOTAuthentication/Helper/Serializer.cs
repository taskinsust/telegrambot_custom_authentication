using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOTAuthentication.Helper
{
    public static class Serializer
    {
        public static string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
