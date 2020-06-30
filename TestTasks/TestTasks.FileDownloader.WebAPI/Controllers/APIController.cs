using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using TestTasks.FileDownloader.WebAPI.Model;

namespace TestTasks.FileDownloader.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class APIController : ControllerBase
    {
        private readonly IDownloadManager downloader;

        public APIController(IDownloadManager downloader) =>
            this.downloader = downloader;

        [HttpGet]
        [ResponseCache(Duration = 0)] // prevent default caching for it conflicts with custom implementation in use
        public async Task<FileResult> DownloadAsync([Required] string fileName) =>
            await downloader.DownloadAsync(fileName);
    }
}