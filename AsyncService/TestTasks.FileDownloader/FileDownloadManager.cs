using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader
{
    public class FileDownloadManager : IFileDownloader
    {
        private readonly IDictionary<string, Task<FileResult>> cache =
            new ConcurrentDictionary<string, Task<FileResult>>();

        private readonly IFileDownloader downloader;

        public FileDownloadManager(IFileDownloader downloader) =>
            this.downloader = downloader;

        public FileResult Download(string file) =>
            DownloadAsync(file).Result;

        public Task<FileResult> DownloadAsync(string file)
        {
            if (cache.TryGetValue(file, out var result))
                return result;

            result = (cache[file] = downloader.DownloadAsync(file));
            result.ContinueWith(_ => cache.Remove(file));
            return result;
        }
    }
}