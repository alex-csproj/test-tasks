using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using TestTasks.FileDownloader;
using Xunit;

namespace TestTask.Tests
{
    public class DownloadManagerTests
    {
        #region Helpers

        private IFileDownloader CreateFileDownloader(int delay)
        {
            IFileDownloader downloader = A.Fake<IFileDownloader>();
            A.CallTo(() => downloader.DownloadAsync(A<string>._))
                .ReturnsLazily(async file =>
                {
                    await Task.Delay(delay);
                    FileResult result = A.Fake<FileResult>();
                    result.FileDownloadName = file.Arguments.Get<string>("file");
                    return result;
                });

            return downloader;
        }

        private int GetTimeRation(Stopwatch stopwatch, int delay) =>
            (int)Math.Round((double)stopwatch.ElapsedMilliseconds / delay, 0);

        #endregion Helpers

        [Fact]
        public void DownloadAsync_WhenRequestsFileBeingDownloadedAtTheMoment_WaitsForExistingRequestToFinishAndReturnsItsResult()
        {
            // Arrange
            int delay = 2000;
            IFileDownloader downloader = CreateFileDownloader(delay);
            FileDownloadManager manager = new FileDownloadManager(downloader);

            string file = "a file";
            Task<FileResult> request1 = manager.DownloadAsync(file);
            Task<FileResult> request2 = manager.DownloadAsync(file);

            // Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task.WaitAll(Task.Run(async () => await request1),
                         Task.Run(async () => await request2));
            stopwatch.Stop();

            //Assert
            FileResult result1 = request1.Result;
            FileResult result2 = request2.Result;
            int timeRatio = GetTimeRation(stopwatch, delay);

            A.CallTo(() => downloader.DownloadAsync(A<string>.Ignored)).MustHaveHappenedOnceExactly();
            result1.FileDownloadName.Should().Be(file);
            result2.FileDownloadName.Should().Be(file);
            result1.Should().BeSameAs(result2);
            timeRatio.Should().Be(1);
        }

        [Fact]
        public async void DownloadAsync_WhenRequestsFileAlreadyDownloaded_DownloadsIndependently()
        {
            // Arrange
            int delay = 2000;
            IFileDownloader downloader = CreateFileDownloader(delay);
            FileDownloadManager manager = new FileDownloadManager(downloader);

            string file = "a file";
            Task<FileResult> request2 = manager.DownloadAsync(file);

            // Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            FileResult result1 = await manager.DownloadAsync(file);
            FileResult result2 = await manager.DownloadAsync(file);
            stopwatch.Stop();

            //Assert
            int timeRatio = GetTimeRation(stopwatch, delay);

            A.CallTo(() => downloader.DownloadAsync(A<string>.Ignored)).MustHaveHappenedTwiceExactly();
            result1.FileDownloadName.Should().Be(file);
            result2.FileDownloadName.Should().Be(file);
            result1.Should().NotBeSameAs(result2);
            timeRatio.Should().Be(2);
        }

        [Fact]
        public void DownloadAsync_WhenRequestsTwoDifferentFilesSimultaneously_DownloadsConcurrently()
        {
            // Arrange
            int delay = 2000;
            IFileDownloader downloader = CreateFileDownloader(delay);
            FileDownloadManager manager = new FileDownloadManager(downloader);

            string file1 = "a file";
            Task<FileResult> request1 = manager.DownloadAsync(file1);
            string file2 = "another file";
            Task<FileResult> request2 = manager.DownloadAsync(file2);

            // Act
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            Task.WaitAll(Task.Run(async () => await request1),
                         Task.Run(async () => await request2));
            stopwatch.Stop();

            //Assert
            FileResult result1 = request1.Result;
            FileResult result2 = request2.Result;
            int timeRatio = GetTimeRation(stopwatch, delay);

            A.CallTo(() => downloader.DownloadAsync(A<string>.Ignored)).MustHaveHappenedTwiceExactly();
            result1.FileDownloadName.Should().Be(file1);
            result2.FileDownloadName.Should().Be(file2);
            result1.Should().NotBeSameAs(result2);
            timeRatio.Should().Be(1);
        }
    }
}