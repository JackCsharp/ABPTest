using ABP.ua.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABP.ua.Services
{
    public interface IUserService
    {
        Task<PriceDto> getPrice(string devicetoken);
        Task<ButtonColorDto> getButtonColor(string devicetoken);
        Task<StatisticData> getStatistic();
    }
}
