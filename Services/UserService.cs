using ABP.ua.Data;
using ABP.ua.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ABP.ua.Services
{
    public class UserService : IUserService
    {

        private readonly DataContext _context;
        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<StatisticData> getStatistic()
        {
            //Створюємо об'єкт класу StatisticData і повертаємо його в контролер
            var statisticsData = new StatisticData
            {
                Experiments = new List<string> { "button-color", "price" },
                DeviceCount = await _context.Users.CountAsync(),
                ButtonColors = new Dictionary<string, int>
                {
                    { "#FF0000", _context.Users.Count((u) => u.ButtonColor == "#FF0000") 
                    },
                    { "#00FF00", _context.Users.Count((u) => u.ButtonColor == "#00FF00") },
                    { "#0000FF", _context.Users.Count((u) => u.ButtonColor == "#0000FF") }
                },
                Prices = new Dictionary<string, int>
                {
                    { "5", _context.Users.Count((u)=> u.Price==5) },
                    { "10", _context.Users.Count((u)=> u.Price==10) },
                    { "20", _context.Users.Count((u)=> u.Price==20) },
                    { "50", _context.Users.Count((u)=> u.Price==50) }

                }
            };
            return statisticsData;
        }
        public async Task<ButtonColorDto> getButtonColor(string devicetoken)
        {
            try { 
                var user = await _context.Users.SingleOrDefaultAsync( device => device.DeviceToken == devicetoken );

                if (user != null && user.ButtonColor!="")
                {
                    //Повертаємо результат, якщо такий девайс вже існує в базі даних
                    ButtonColorDto response = new ButtonColorDto();
                    response.value = user.ButtonColor;
                    return response;
                }
                else {
                    //Генеруємо випадкові значення для цін
                    Random random = new Random();
                    int randomNumber = random.Next(1, 4);

                    var response = new ButtonColorDto();
                    switch (randomNumber)
                    {
                        case 1:
                            response.value = "#FF0000";
                                break;
                        case 2:
                            response.value = "#00FF00";
                                    break;
                        case 3:
                            response.value = "#0000FF";
                                break;
                        default:
                            response.value = "#FF0000";
                                break;
                    }

                    var request = _context.Users.FirstOrDefault(token => token.DeviceToken == devicetoken);
                    //Перевіряємо чи є вже такий девайс в базі даних і або додаємо новий або оновлюємо існуючий
                    if (request == null)
                    {
                        User userToDb = new();
                        userToDb.DeviceToken = devicetoken;
                        userToDb.ButtonColor = response.value;

                        _context.Add(userToDb);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        request.ButtonColor = response.value;
                        await _context.SaveChangesAsync();
                    }
                    return response;
                    }
            }
            catch
            {
                //Повертаємо null якщо виникає помилка
                return null;
            }
        }

        public async Task<PriceDto> getPrice(string devicetoken)
        {
            try { 
                var user = await _context.Users.SingleOrDefaultAsync(device => device.DeviceToken == devicetoken);
                if (user != null && user.Price!=0)
                {
                    //Повертаємо результат, якщо такий девайс вже існує в базі даних
                    PriceDto response = new PriceDto();
                    response.Price = user.Price;
                    return response;
                }
                else
                {
                    //Генеруємо випадкові значення для цін
                    Random random = new Random();

                    int randomNumber = random.Next(1, 101);

                    var response = new PriceDto();
                    if (randomNumber <= 100)
                    {
                        response.Price = 5;
                    }
                    if (randomNumber <= 90)
                    {
                        response.Price = 50;
                    }
                    if (randomNumber <= 85)
                    {
                        response.Price = 20;
                    }

                    if (randomNumber <= 75)
                    {
                        response.Price = 10;
                    }
                
                
                    //Перевіряємо чи є вже такий девайс в базі даних і або додаємо новий або оновлюємо існуючий
                    var request = _context.Users.FirstOrDefault(token => token.DeviceToken == devicetoken);
                    if( request == null)
                    {
                        User userToDb = new();
                        userToDb.DeviceToken = devicetoken;
                        userToDb.Price = response.Price;

                        _context.Add(userToDb);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        request.Price = response.Price;
                        await _context.SaveChangesAsync();
                    }
                

                    return response;
                }
            }
            catch 
            {
                //Повертаємо null якщо виникає помилка
                return null;
            }
        }
    }
}
