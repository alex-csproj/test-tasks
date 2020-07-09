using Microsoft.Extensions.Hosting;

namespace TestTasks.FileDownloader
{
    public class ContentRootProvider : ILocationProvider
    {
        public ContentRootProvider(IHostEnvironment environment) =>
            Location = environment.ContentRootPath;

        public string Location { get; }
    }
}