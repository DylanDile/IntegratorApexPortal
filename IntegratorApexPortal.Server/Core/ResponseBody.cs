namespace ApexIntegratorApi.Core
{
    public class ResponseBody
    {
        public Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();
        public Dictionary<string, object> List { get; set; } = new Dictionary<string, object>();
    }
}
