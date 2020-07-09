using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader
{
    public class FileDownloadManager : IFileDownloader
    {
        private readonly IDictionary<string, DownloadHelper> cache =
            new ConcurrentDictionary<string, DownloadHelper>();

        private readonly IFileDownloader downloader;

        public FileDownloadManager(IFileDownloader downloader) =>
            this.downloader = downloader;

        public FileResult Download(string file) =>
            DownloadAsync(file).Result;

        public async Task<FileResult> DownloadAsync(string file)
        {
            if (cache.TryGetValue(file, out var helper))
                helper.AddClient();
            else
                helper = (cache[file] = new DownloadHelper(downloader.DownloadAsync(file)));

            var result = await helper.FileResult;

            helper.RemoveClient();
            if (!helper.HasClients())
                cache.Remove(file);

            return result;
        }

        private class DownloadHelper
        {
            private int clients = 1;
            private readonly object sync = new object();

            public Task<FileResult> FileResult { get; }

            public DownloadHelper(Task<FileResult> fileResult) =>
                FileResult = fileResult;

            public void AddClient()
            {
                lock (sync)
                    clients++;
            }

            public bool HasClients()
            {
                lock (sync)
                    return clients > 0;
            }

            public void RemoveClient()
            {
                lock (sync)
                    clients--;
            }
        }
    }
}