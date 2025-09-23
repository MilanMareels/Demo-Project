using AP.Demo_Project.WebApp.DTO;

namespace AP.Demo_Project.WebApp.Components.Pages{
    partial class AddCity{
        private CityCreateDTO city = new();

        // Hardcoded landenlijst
    
    private readonly List<CountryDTO> countries = new()
    {
        new CountryDTO { Id = 1, Name = "United States" },
        new CountryDTO { Id = 2, Name = "United Kingdom" },
        new CountryDTO { Id = 3, Name = "Belgium" },
        new CountryDTO { Id = 4, Name = "France" },
        new CountryDTO { Id = 5, Name = "Netherlands" }
    };

        private async Task CreateCity()
        {
            try
            {
                var response = await Http.PostAsJsonAsync("api/v1/City", city);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Stad succesvol aangemaakt.");
                    Navigation.NavigateTo("/cities");
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Server-validatiefout: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception tijdens aanmaken: {ex.Message}");
            }
        }

        private void GoBack() => Navigation.NavigateTo("/cities");
    }
}
