using ApexIntegratorApi.Core;

namespace IntegratorApexPortal.Server.Core
{
    public class ApiUnprocessedResponse
    {
        private static ApiUnprocessedResponse _apiResponse;
        private Dictionary<object, object> _response = new Dictionary<object, object>();
        private Dictionary<object, object> _body = new Dictionary<object, object>();

        private ApiUnprocessedResponse()
        {
        }

        public static ApiUnprocessedResponse Start()
        {
            _apiResponse = new ApiUnprocessedResponse();
            return _apiResponse;
        }

        public ApiUnprocessedResponse Errors(string name, object data)
        {
            _body[name] = data;
            return this;
        }

        public Dictionary<object, object> Build()
        {
            _response["errors"]  = _body;
            return _response;
        }
    }
}
