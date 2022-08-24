using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPI.Infrastructure.Utils
{
    public static class JsonHelper
    {
        public static string SerializeObject(this object? obj)
        {
            var options =
                new JsonSerializerOptions
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                };
            return JsonSerializer.Serialize(obj, options);
        }
    }
}