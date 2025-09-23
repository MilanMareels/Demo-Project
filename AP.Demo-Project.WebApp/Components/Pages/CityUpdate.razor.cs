
using AP.Demo_Project.WebApp.DTO;
using Microsoft.AspNetCore.Components;


namespace AP.Demo_Project.WebApp.Components.Pages{
    public partial class CityUpdate
    {
        [Parameter] public int CityId { get; set; }
        private CityUpdateDTO? city;
        private bool isLoading = true;

        protected override async Task OnInitializedAsync()
        {
            await LoadCity();
        }

        private async Task LoadCity()
        {
            try
            {
                var cityFromApi = await Http.GetFromJsonAsync<CityUpdateDTO>($"api/v1/City/{CityId}");
                if (cityFromApi != null)
                {
                    city = cityFromApi;
                }
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
            if (city == null) return;

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
