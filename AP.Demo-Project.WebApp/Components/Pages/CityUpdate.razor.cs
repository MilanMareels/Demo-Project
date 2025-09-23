
using AP.Demo_Project.WebApp.DTO;
using AP.Demo_Project.Application.CQRS.City;
using Microsoft.AspNetCore.Components;


namespace AP.Demo_Project.WebApp.Components.Pages{
    public partial class CityUpdate
    {
        [Parameter] public int CityId { get; set; }
        private CityDTO? city;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadCity();
        }

        private async Task LoadCity()
        {
            try
            {
                city = await Http.GetFromJsonAsync<CityDTO>($"api/v1/City/{CityId}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading city: {ex.Message}");
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task UpdateCity()
        {
            try
            {
                var response = await Http.PutAsJsonAsync($"api/v1/City/{CityId}", city);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("City updated successfully.");
                    Navigation.NavigateTo("/cities");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error updating city: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception during update: {ex.Message}");
            }
        }

        private void GoBack()
        {
            Navigation.NavigateTo("/cities");
        }
    }
}
