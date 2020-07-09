using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TestTasks.FileDownloader
{
    public interface IFileDownloader
    {
        FileResult Download(string file);

        Task<FileResult> DownloadAsync(string file);
    }
}