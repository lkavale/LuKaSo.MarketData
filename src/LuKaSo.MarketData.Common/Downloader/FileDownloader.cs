using LuKaSo.MarketData.Common.Helpers;
using LuKaSo.MarketData.Infrastructure.Downloader;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LuKaSo.MarketData.Common.Downloader
{
    /// <summary>
    /// Ducascopy downloader
    /// </summary>
    public class FileDownloader : IFileDownloader
    {
        /// <summary>
        /// Log
        /// </summary>
        private readonly ILogger<FileDownloader> _log;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="log">Log (IOC)</param>
        public FileDownloader(ILogger<FileDownloader> log)
        {
            _log = log;
        }

        /// <summary>
        /// Download file
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        /// <returns></returns>
        public async Task DownloadFileAync(string sourceFile, string destinationFile)
        {
            DirectoryHelpers.DirectoryCreateIfNotExist(destinationFile);

            using (var webClient = new WebClient())
            {
                try
                {
                    await webClient.DownloadFileTaskAsync(sourceFile, destinationFile);
                    _log.LogTrace($"Finished download of source file {sourceFile} {destinationFile}.");
                }
                catch (Exception exp)
                {
                    _log.LogError($"Download of source file failed {sourceFile} failed.", exp);
                    throw;
                }
            }
        }
    }
}
