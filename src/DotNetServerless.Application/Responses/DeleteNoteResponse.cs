using Newtonsoft.Json;

namespace DotNetServerless.Application.Responses
{
    public class DeleteNoteResponse
    {
        [JsonProperty("status")]
        public bool Status { get; set; }
    }
}
