using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader.WebAPI.Model
{
    public class DownloadManager : IDownloadManager
    {
        private readonly IFileDownloader downloader;

        public DownloadManager(IFileDownloader downloader) =>
            this.downloader = new FileDownloadManager(downloader);

        public FileResult Download(string file) =>
            downloader.Download(file);

        public Task<FileResult> DownloadAsync(string file) =>
            downloader.DownloadAsync(file);
    }
}