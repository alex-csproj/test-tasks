using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader
{
    public class FileDownloadManager : IFileDownloader
    {
        private static readonly Dictionary<string, FileResult> cache = new Dictionary<string, FileResult>();
        private static readonly Dictionary<string, int> counters = new Dictionary<string, int>();
        private static readonly Dictionary<string, SemaphoreSlim> semaphores = new Dictionary<string, SemaphoreSlim>();
        private static readonly object sync = new object();

        private readonly IFileDownloader downloader;

        public FileDownloadManager(IFileDownloader downloader) =>
            this.downloader = downloader;

        public FileResult Download(string file) =>
            DownloadAsync(file).Result;

        public async Task<FileResult> DownloadAsync(string file)
        {
            AddClient(file);

            var semaphore = GetSemaphore(file);

            FileResult result;
            await semaphore.WaitAsync();
            try
            {
                if (!cache.ContainsKey(file))
                    cache[file] = await downloader.DownloadAsync(file);

                result = cache[file];

                RemoveClient(file);
            }
            finally
            {
                semaphore.Release(1);
                if (!semaphores.ContainsValue(semaphore))
                    semaphore.Dispose();
            }

            return result;
        }

        private static void AddClient(string file)
        {
            lock (sync)
            {
                if (counters.ContainsKey(file))
                    counters[file]++;
                else
                    counters[file] = 1;
            }
        }

        private static SemaphoreSlim GetSemaphore(string file)
        {
            if (!semaphores.ContainsKey(file))
                lock (sync)
                    if (!semaphores.ContainsKey(file))
                        semaphores[file] = new SemaphoreSlim(1, 1); // consider using semaphore pool

            return semaphores[file];
        }

        private static void RemoveClient(string file)
        {
            lock (sync)
            {
                if (!counters.ContainsKey(file))
                    throw new InvalidOperationException("You are not supposed to get here");

                counters[file]--;

                if (counters[file] > 0)
                    return;

                counters.Remove(file);
                cache.Remove(file);
                semaphores.Remove(file);
            }
        }
    }
}