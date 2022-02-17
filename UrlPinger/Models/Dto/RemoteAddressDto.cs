namespace UrlPinger.Models.Dto
{
    public class RemoteAddressDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
