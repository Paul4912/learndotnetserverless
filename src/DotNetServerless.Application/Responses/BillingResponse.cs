using Newtonsoft.Json;

namespace DotNetServerless.Application.Responses
{
    public class BillingResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
