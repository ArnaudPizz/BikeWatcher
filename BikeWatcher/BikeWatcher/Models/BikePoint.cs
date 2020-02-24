using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BikeWatcher
{
    public class BikePoints
    {
        //public string number { get; set; }
        //public string available_bikes { get; set; }
        //public string lng { get; set; }
        //public string availability { get; set; }
        //public string availabilitycode { get; set; }
        //public string bike_stands { get; set; }
        //public string last_update { get; set; }
        //public string available_bike_stands { get; set; }
        public string status { get; set; }
        //public string commune { get; set; }
        public string address { get; set; }
        //public string lat { get; set; }
        //public string last_update_fme { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
    }

    public class RootObject
    {
        public List<string> fields { get; set; }
        public List<BikePoints> values { get; set; }
        public int nb_results { get; set; }
        public string table_href { get; set; }
        public string layer_name { get; set; }
    }
}
