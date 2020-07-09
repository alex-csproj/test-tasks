using Microsoft.AspNetCore.Mvc;

namespace TestTasks.FileDownloader.WebAPI.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("[controller]")]
        public string Error() => "Something went wrong";
    }
}