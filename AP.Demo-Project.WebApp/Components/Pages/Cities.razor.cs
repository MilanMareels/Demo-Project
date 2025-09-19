using AP.Demo_Project.WebApp.Entities;
using Microsoft.AspNetCore.Components;

namespace AP.Demo_Project.WebApp.Components.Pages
{
    public partial class Cities : ComponentBase
    {
        private List<City> cities;

        protected override void OnInitialized()
        {
            // Dummy data voor test totdat API klaar is
            cities = new List<City>()
            {
                new City(1, "Antwerpen", 255000, "Belgie"),
                new City(2, "Merksem", 90000, "Belgie"),
                new City(3, "Berchem", 50000, "Belgie"),
                new City(4, "Ekeren", 20000, "Belgie")
            };
        }

        private void DeleteCity(int id)
        {
            // Hier komt de API call
            var cityToRemove = cities.FirstOrDefault(c => c.Id == id);
            if (cityToRemove != null)
            {
                cities.Remove(cityToRemove);
                StateHasChanged(); // UI refresh
            }
        }
    }
}
