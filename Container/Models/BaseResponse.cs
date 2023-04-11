using Container.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Container.Models
{
    public class BaseResponse<T>
    {
        // public bool Success { get; set; }

        public string Description { get; set; }


        private ResultCode _code;

        [JsonProperty("code")]
        public ResultCode Code
        {
            get => _code;
            set
            {
                _code = value;
            }
        }

        public T Data { get; set; }

        public override string ToString()
        {
            return $"{nameof(Code)}: {Code}, {nameof(Description)}: {Description}, {nameof(Data)}: {Data}";
        }



    }
}
