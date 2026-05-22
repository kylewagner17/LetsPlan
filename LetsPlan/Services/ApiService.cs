using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LetsPlan.Models;
using LetsPlan.Services.Interfaces;

namespace LetsPlan.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _httpClient;

        public async Task<List<Event>> GetEventsAsync()
        {
            var response = await _httpClient.GetStringAsync("api/events");
            return JsonSerializer.Deserialize<List<Event>>(response);
        }
    }
}
