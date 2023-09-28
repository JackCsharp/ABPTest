using ABP.ua.Models;
using ABP.ua.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace ABP.ua.Controllers
{
    [Route("api/experiment")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("button-color")]
        public async Task<ActionResult<ButtonColorDto>> getButtonColor([FromQuery(Name = "device-token")] string devicetoken)
        {
            var response = await _userService.getButtonColor(devicetoken);
            //Якщо виникає помилка - сервіс повертає null
            if (response == null) return BadRequest("Something went wrong. Check your data");
            return Ok(response);
        }

        [HttpGet("price")]
        public async Task<ActionResult<PriceDto>> getPrice([FromQuery(Name = "device-token")] string devicetoken)
        {
            var response = await _userService.getPrice(devicetoken);
            //Якщо виникає помилка - сервіс повертає null
            if (response == null) return BadRequest("Something went wrong. Check your data");
            return Ok(response);
        }

        [HttpGet("get-statistic")]
        public Task<StatisticData> getStatistic()
        {
            //Повертаємо просто json оскільки передбачається, що ця статистика для перегляду тільки робітниками компанії
            return _userService.getStatistic();
        }
        
    }
}
