using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StealthMonitoringNASA_API.Contracts;

namespace StealthMonitoringNASA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IFileHelper _fileHelper;
        public TestController(IFileHelper fileHelper)
        {
            _fileHelper = fileHelper;
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile textFile)
        {
            IEnumerable<string> dates = _fileHelper.ReadFile(textFile);

            await _fileHelper.DownloadAsync(dates);

            return Ok(dates);
        }
        [HttpGet("GetImages")]
        public IActionResult GetImages()
        {        
            return Ok(_fileHelper.GetImages());
        }
    }
}