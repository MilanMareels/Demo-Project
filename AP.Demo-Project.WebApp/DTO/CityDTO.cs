namespace AP.Demo_Project.WebApp.DTO{
    public class CityDTO {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Population { get; set; }
        public int CountryId { get; set; }
        public CountryDTO Country { get; set; }
    }
}
