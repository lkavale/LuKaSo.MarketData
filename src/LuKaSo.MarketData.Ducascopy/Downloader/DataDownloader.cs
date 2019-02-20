using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Types.Downloader;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Ducascopy.Downloader
{
    /// <summary>
    /// Ducascopy downloader
    /// </summary>
    public class DataDownloader : IDataDownloader
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<DataDownloader> _log;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">Log (IOC)</param>
        public DataDownloader(ILogger<DataDownloader> log)
        {
            _log = log;
        }

        /// <summary>
        /// Download async
        /// </summary>
        /// <param name="downloaderItem">Downloader items</param>
        /// <param name="cancellationToken">Cancelation token</param>
        /// <returns>Is error occurred</returns>
        public async Task<bool> DownloadAsync(DownloaderItem downloaderItem, CancellationToken cancellationToken)
        {
            bool isOk = true;
            int fileCount = 0;

            _log.LogTrace("Starting download of " + downloaderItem.Files.Count.ToString() + " files.");

            downloaderItem.Indicator.Start(downloaderItem.Files.Count);
            downloaderItem.Status = DownloaderItemStatus.Downloading;

            foreach (var file in downloaderItem.Files)
            {
                _log.LogTrace("Starting download file " + file.Url + " to " + file.File + ".");

                // If operation cancellation is in process
                if (cancellationToken.IsCancellationRequested)
                {
                    _log.LogDebug("Downloading has been aborted.");
                    break;
                }

                DirectoryCreateIfNotExist(file.File);

                using (var webClient = new WebClient())
                {
                    try
                    {
                        await webClient.DownloadFileTaskAsync(file.Url, file.File);
                        _log.LogTrace("Finished download of " + file.Url + " to " + file.File + ".");
                    }
                    catch (Exception exp)
                    {
                        isOk = false;
                        _log.LogError("Downloading of " + file.Url + " failed.", exp);
                    }

                    fileCount++;
                    downloaderItem.Indicator.Report(fileCount);
                }
            }

            if (isOk)
            {
                if (fileCount == downloaderItem.Files.Count)
                {
                    downloaderItem.Status = DownloaderItemStatus.Completed;
                }
                else
                {
                    downloaderItem.Status = DownloaderItemStatus.Incomplete;
                }

                downloaderItem.Indicator.Finish();
            }
            else
            {
                downloaderItem.Status = DownloaderItemStatus.Failed;
                downloaderItem.Indicator.Failed();
            }

            _log.LogTrace("Downloading of " + downloaderItem.Files.Count.ToString() + " files finished.");

            return isOk;
        }

        /// <summary>
        /// Create directory if not exists
        /// </summary>
        /// <param name="fileName">File name</param>
        protected void DirectoryCreateIfNotExist(string fileName)
        {
            var path = Path.GetDirectoryName(fileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
