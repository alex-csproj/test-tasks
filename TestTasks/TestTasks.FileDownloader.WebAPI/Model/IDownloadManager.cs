namespace TestTasks.FileDownloader.WebAPI.Model
{
    // to resolve IFileDownloader implementations independently
    public interface IDownloadManager : IFileDownloader
    {
    }
}