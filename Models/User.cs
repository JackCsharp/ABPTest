namespace ABP.ua.Models
{
    public class User
    {
        public int Id { get; set; }
        public string DeviceToken { get; set; } = string.Empty;
        public string ButtonColor { get; set; } = string.Empty;
        public int Price { get; set; } = 0;
    }
}
