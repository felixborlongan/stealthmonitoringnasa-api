using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StealthMonitoringNASA_API.Contracts;
using StealthMonitoringNASA_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Helpers
{
    public class NASAHelper : INASAHelper
    {
        private readonly IConfiguration _configuration;
        public NASAHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<NASAApiVM> GetPhotosByEarthDateAsync(string earthDate)
        {
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(string.Format(_configuration["NASAAPI"], earthDate, _configuration["NASAAPIKEY"]));

            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<NASAApiVM>(response);
        }
    }
}
