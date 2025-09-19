using System.ComponentModel.DataAnnotations;

namespace AP.Demo_Project.WebApp.Entities
{
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public long Population { get; set; }

        public string Country { get; set; }

        public City(int id,string name,long population, string country) {
            Id = id;
            Name = name;
            Population = population;
            Country = country;
        }
    }
}
