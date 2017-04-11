namespace TForTea.Models
{
    using Newtonsoft.Json;
    using Prism.Mvvm;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Tea : BindableBase
    {
        private double rate;

        public event Action RatingChanged;

        public int BrewTemperature { get; set; }

        public int BrewTimeInMinutes { get; set; }

        public string Description { get; set; }

        [JsonPropertyAttribute(PropertyName = "Id")]
        public string Guid { get; set; }

        [Key]
        [JsonIgnoreAttribute]
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
                this.SetProperty(ref this.rate, value);
            }
        }

        public string Type { get; set; }
    }
}