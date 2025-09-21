using AP.Demo_Project.WebApp.DTO;
using Microsoft.AspNetCore.Components;

namespace AP.Demo_Project.WebApp.Components.Pages{
    partial class Cities {
        private List<CityDTO> cities;
        private bool isLoading = true;
        private int pageNr = 1;
        private int pageSize = 5;
        private bool sortAscending = true;
        private readonly int[] pageSizes = new[] { 5, 10, 20 };

        protected override async Task OnInitializedAsync()
        {
            await LoadCities();
        }

        private async Task LoadCities()
        {
            isLoading = true;
            try
            {
                var sortOrder = sortAscending ? "asc" : "desc";
                // API Call
                var response = await Http.GetFromJsonAsync<List<CityDTO>>(
                    $"api/v1/City?pageNr={pageNr}&pageSize={pageSize}&sortBy=Population&sortOrder={sortOrder}");

                cities = response ?? new List<CityDTO>();
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching cities: {ex.Message}");
                cities = new List<CityDTO>();
            }
            finally
            {
                isLoading = false;
            }
        }

        private async Task SortByPopulation(bool ascending)
        {
            sortAscending = ascending;
            pageNr = 1;
            await LoadCities();
        }

        private async Task OnPageSizeChanged(ChangeEventArgs e)
        {
            if (int.TryParse(e.Value.ToString(), out var newSize))
            {
                pageSize = newSize;
                pageNr = 1;
                await LoadCities();
            }
        }

        private async Task NextPage()
        {
            pageNr++;
            await LoadCities();
        }

        private async Task PreviousPage()
        {
            if (pageNr > 1)
            {
                pageNr--;
                await LoadCities();
            }
        }

        private bool CanPrevious => pageNr > 1;
        private bool CanNext => cities.Count == pageSize; // Next page check
    }
}
