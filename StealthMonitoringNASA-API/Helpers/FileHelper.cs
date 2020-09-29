using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using StealthMonitoringNASA_API.Contracts;
using StealthMonitoringNASA_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly INASAHelper _nasaHelper;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public FileHelper(INASAHelper nasaHelper, IWebHostEnvironment webHostEnvironment)
        {
            _nasaHelper = nasaHelper;
            _webHostEnvironment = webHostEnvironment;
        }
        public IEnumerable<string> ReadFile(IFormFile formFile)
        {
            StreamReader streamReader = new StreamReader(formFile.OpenReadStream());
            string line = streamReader.ReadLine();

            string[] formats = new string[]
            {
                "MM/dd/yy", "MMMM d, yyyy", "MMM-d-yyyy"
            };

            while (line != null)
            {
                DateTime dateTime;

                if (DateTime.TryParseExact(line, formats, null, DateTimeStyles.None, out dateTime))
                {
                    yield return dateTime.ToString("yyyy-MM-dd");
                }
                line = streamReader.ReadLine();
            }
        }
        public async Task DownloadAsync(IEnumerable<string> dates)
        {
            string[] splitted;
            string fileName, folderPath = string.Empty;
            WebClient webClient = new WebClient();

            foreach (string date in dates)
            {
                NASAApiVM nASAApiVM = await _nasaHelper.GetPhotosByEarthDateAsync(date);

                if (nASAApiVM.Photos.Count > 0)
                {
                    string folder = $"{_webHostEnvironment.ContentRootPath}/DownloadedImages/{date}";

                    if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                    foreach (Photo photo in nASAApiVM.Photos)
                    {
                        splitted = photo.img_src.Split('/');

                        fileName = splitted[splitted.Length - 1];

                        folderPath = $"{folder}/{fileName.Replace(".JPG", ".jpg")}";

                        if (!File.Exists(folderPath))   webClient.DownloadFile(photo.img_src, folderPath);
                    }
                }            
            }
        }
        public IEnumerable<string> GetImages()
        {
            string[] splitted;
            IEnumerable<string> folders = Directory.GetDirectories($"{_webHostEnvironment.ContentRootPath}/DownloadedImages");

            foreach (string folder in folders)
            {
                foreach (string imagePath in Directory.GetFiles(folder, "*.jpg", SearchOption.AllDirectories))
                {
                    splitted = imagePath.Split(@"\");

                    yield return $@"{splitted[splitted.Length - 2]}/{splitted[splitted.Length - 1]}"; 
                }
            }
        }
    }
}
