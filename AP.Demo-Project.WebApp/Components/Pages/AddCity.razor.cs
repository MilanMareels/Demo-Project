using Microsoft.AspNetCore.Components;
using AP.Demo_Project.WebApp.Models;

namespace AP.Demo_Project.WebApp.Components.Pages
{
    public partial class AddCity : ComponentBase
    {
        private CityModel cityModel = new();
        private string? serverErrorMessage;

        // Voorbeeldlijst van landen voor de dropdown (select)
        private List<Country> countries = new()
    {
        new Country(1, "België"),
        new Country(2, "Nederland"),
        new Country(3, "Frankrijk")
    };

        private async Task HandleValidSubmit()
        {
            serverErrorMessage = null;
            Console.WriteLine($"Stad {cityModel.Name} is succesvol opgeslagen!");
        }

        public record Country(int Id, string Name);
    }
}
