using Newtonsoft.Json;
using System.Collections.Generic;

namespace BloodSearch.Core.Models {

    public class BaseResponse {

        [JsonProperty("success")]
        public bool Success { get; set; } = true;

        [JsonProperty("err-messages")]
        public List<KeyMsg> ErrMessages { get; set; } = new List<KeyMsg>();
    }

    /// <summary>
    /// Класс описывает сообщение об ошибке
    /// </summary>
    public class KeyMsg {

        /// <summary>
        /// Ключ (код)
        /// </summary>
        [JsonProperty]
        public int Key { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        [JsonProperty]
        public string Message { get; set; }
    }
}