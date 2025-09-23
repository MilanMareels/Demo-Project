using AP.Demo_Project.Application.CQRS.City;
using AP.Demo_Project.WebApp.DTO;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace AP.Demo_Project.WebApp.Components.Pages
{
    public partial class CityUpdate
    {
        [Parameter] public int Id { get; set; }
        private CityDTO city;

        // REMOVE these lines
        // [Inject] private HttpClient Http { get; set; }
        // [Inject] private NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadCity();
        }

        private async Task LoadCity()
        {
            try
            {
                city = await Http.GetFromJsonAsync<CityDTO>($"api/v1/City/{Id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading city: {ex.Message}");
            }
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                var updateDto = new CityUpdateDTO
                {
                    Id = city.Id,
                    Name = city.Name,
                    Population = city.Population,
                    CountryId = city.CountryId
                };

                var response = await Http.PutAsJsonAsync($"api/v1/City/{Id}", updateDto);
                response.EnsureSuccessStatusCode();

                Navigation.NavigateTo("/cities");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating city: {ex.Message}");
            }
        }

        private void GoBack()
        {
            Navigation.NavigateTo("/cities");
        }
    }
}