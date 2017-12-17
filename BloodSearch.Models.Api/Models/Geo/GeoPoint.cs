using BloodSearch.Core.Extensions;
using Newtonsoft.Json;
using System;

namespace BloodSearch.Models.Api.Models.Geo {

    public class GeoPoint {

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        public static GeoPoint FromYandexFormat(string value) {
            var pointSplitted = value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            var lng = pointSplitted[0].ToDouble();
            var lat = pointSplitted[1].ToDouble();

            if (lat.HasValue && lng.HasValue) {
                return new GeoPoint { Lat = lat.Value, Lng = lng.Value };
            }

            return null;
        }
    }
}