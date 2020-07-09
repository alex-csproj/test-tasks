using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader
{
    public class SlowFileDownloader : IFileDownloader
    {
        private static readonly IReadOnlyDictionary<string, string> mimeTypes = new Dictionary<string, string>
        {
            [".txt"] = "text/plain",
            [".pdf"] = "application/pdf",
            [".png"] = "image/png",
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".gif"] = "image/gif"
        };

        private readonly string root;

        public SlowFileDownloader(ILocationProvider locationProvider) =>
            root = Path.GetFullPath(locationProvider.Location);

        public FileResult Download(string file) =>
            DownloadAsync(file).Result;

        public async Task<FileResult> DownloadAsync(string file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));

            var path = Path.GetFullPath(Path.Combine(root, file));

            if (!path.StartsWith(root))
                throw new InvalidOperationException("Access to directory is denied");

            var extension = Path.GetExtension(path).ToLowerInvariant();

            if (!mimeTypes.TryGetValue(extension, out var contentType))
                throw new InvalidOperationException($"Requested file '{file}' has usupported extension '{extension}'");

            await Task.Delay(2000); // simulate long operation
            return new PhysicalFileResult(path, contentType) { FileDownloadName = file };
        }
    }
}