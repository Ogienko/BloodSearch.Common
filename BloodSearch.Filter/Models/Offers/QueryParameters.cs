using System;

namespace BloodSearch.Filter.Models.Offers {

    public class QueryParameters {

        /// <summary>
        /// Тип
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Категория крови
        /// </summary>
        public string Cat { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public string Ct { get; set; }

        public string Sort { get; set; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public string P { get; set; }

        /// <summary>
        /// Количество элементов на странице
        /// </summary>
        public string Ps { get; set; }
    }
}