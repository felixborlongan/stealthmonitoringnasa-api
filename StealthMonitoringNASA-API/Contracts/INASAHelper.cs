using StealthMonitoringNASA_API.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StealthMonitoringNASA_API.Contracts
{
    public interface INASAHelper
    {
        Task<NASAApiVM> GetPhotosByEarthDateAsync(string earthDate);
    }
}
