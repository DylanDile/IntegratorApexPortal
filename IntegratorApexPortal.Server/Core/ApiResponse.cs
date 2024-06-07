using System.Collections.Generic;
namespace ApexIntegratorApi.Core
{
    public class ApiResponse
    {
        private static ApiResponse _apiResponse;
        private Dictionary<object, object> _response = new Dictionary<object, object>();
        private Dictionary<object, object> _body = new Dictionary<object, object>();
        private bool _success = true;
        private string _message = "";

        private ApiResponse()
        {
        }

        public static ApiResponse Start()
        {
            _apiResponse = new ApiResponse();
            return _apiResponse;
        }

        public ApiResponse Data(string name, object data)
        {
            _body[name] = data;
            return this;
        }

        public ApiResponse Success(bool success, string message)
        {
            _success = success;
            _message = message;
            return this;
        }

        public ApiResponse Success(bool success)
        {
            _success = success;
            return this;
        }

        public ApiResponse Success()
        {
            _success = true;
            return this;
        }

        public ApiResponse Message(string message)
        {
            _message = message;
            return this;
        }

        public ApiResponse Fail()
        {
            _success = false;
            return this;
        }

        public Dictionary<object, object> Build()
        {
            _response["success"] = _success;
            _response["body"] = _body;
            if (!string.IsNullOrEmpty(_message))
                _response["message"] = _message;
            return _response;
        }
    }

}
