using Newtonsoft.Json;
using SQLite;
using ViewManagement;

namespace TForTea.WinRT.Models
{
    public class Tea : ViewModelBaseWrapper
    {
        private double rate;

        public int BrewTemperature { get; set; }

        public int BrewTimeInMinutes { get; set; }

        public string Description { get; set; }

        [JsonPropertyAttribute(PropertyName = "Id")]
        public string Guid { get; set; }

        [JsonIgnoreAttribute]
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Image { get; set; }

        public string Name { get; set; }

        public double Rate
        {
            get
            {
                return this.rate;
            }

            set
            {
                rate = value;
                RaisePropertyChanged();
            }
        }

        public string Type { get; set; }
    }
}