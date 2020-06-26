using System.Collections.Generic;

namespace DotNetServerless.Application.Common
{
    public class CommonHeaders
    {
        public static Dictionary<string, string> corsHeaders = new Dictionary<string, string>
        {
            { "Access-Control-Allow-Origin", "*" },
            { "Access-Control-Allow-Credentials", "true" }
        };
    }
}
