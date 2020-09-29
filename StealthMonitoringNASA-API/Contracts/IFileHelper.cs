using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Contracts
{
    public interface IFileHelper
    {
        IEnumerable<string> ReadFile(IFormFile formFile);
        Task DownloadAsync(IEnumerable<string> dates);
        IEnumerable<string> GetImages();
    }
}
