namespace Domain.Responses
{
    public class WerfWerknemerInsertedResponse
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public string Voornaam { get; set; }
        public string Achternaam { get; set; }
        public int WerfId { get; set; }
        public string WerfNaam { get; set; }
    }
}
