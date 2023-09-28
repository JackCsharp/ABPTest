namespace ABP.ua.Models
{
    public class StatisticData
    {
        public List<string> Experiments { get; set; }
        public int DeviceCount { get; set; }
        public Dictionary<string, int> ButtonColors { get; set; }
        public Dictionary<string, int> Prices { get; set; }
    }
}
