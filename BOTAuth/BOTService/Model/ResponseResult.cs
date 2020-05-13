using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BOTService.Model
{
    public class ResponseResult
    {
        public ResponseResult(bool isSuccess, string message)
        {
            this.IsSuccess = isSuccess;
            this.Message = message;
        }

        [JsonProperty("ok", Required = Required.Always)]
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Get error message
        /// </summary>
        [JsonProperty("description", Required = Required.Default)]
        public string Message { get; set; }
    }
}
