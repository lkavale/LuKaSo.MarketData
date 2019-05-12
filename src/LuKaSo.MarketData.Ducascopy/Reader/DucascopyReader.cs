using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Data;
using LuKaSo.MarketData.Infrastructure.Downloader;
using LuKaSo.MarketData.Infrastructure.FileSystem;
using LuKaSo.MarketData.Infrastructure.Reader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.Reader
{
    public class DucascopyReader : ITickDataReader<TickAskBidVolumeData>, IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IFileManager<DucascopySymbol> _fileManager;

        private LinkedList<IFile> _files;

        /// <summary>
        /// Time current
        /// </summary>
        private LinkedListNode<IFile> _fileCurrent;

        /// <summary>
        /// Reader
        /// </summary>
        private DucascopyFileStreamReader _reader;

        /// <summary>
        /// Stream
        /// </summary>
        private DucascopyFileSteam _stream;

        /// <summary>
        /// Ducascopy symbol
        /// </summary>
        private readonly DucascopySymbol _symbol;

        /// <summary>
        /// Can report progress
        /// </summary>
        public bool CanProgressReport
        {
            get { return false; }
        }

        /// <summary>
        /// Is end of stream
        /// </summary>
        /// <returns>End of stream?</returns>
        public bool IsEndOfStream
        {
            get
            {
                while (_reader.IsEndOfStream)
                {
                    if (_fileCurrent.Next != null)
                    {
                        ConnectNextReader();
                    }
                    else
                    {
                        return true;
                    }
                }

                return _reader.IsEndOfStream;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataDirectory"></param>
        /// <param name="symbol"></param>
        /// <param name="timeFrom"></param>
        /// <param name="timeTo"></param>
        public DucascopyReader(IConfiguration configuration, IFileManager<DucascopySymbol> fileManager, DucascopySymbol symbol, DateTime? timeFrom, DateTime timeTo)
        {
            _symbol = symbol ?? throw new ArgumentNullException("symbol", "Symbol must be specified");
            _configuration = configuration;
            _fileManager = fileManager;

            _files = new LinkedList<IFile>(fileManager.GetExistingFiles(symbol, timeFrom ?? symbol.StartDate, timeTo).Where(f => f.Time >= timeFrom && f.Time < timeTo));
            _fileCurrent = _files.First;

            _stream = new DucascopyFileSteam(Path.Combine(configuration.DataPath, _fileCurrent.Value.DestinationFile));
            _reader = new DucascopyFileStreamReader(symbol, _fileCurrent.Value.Time, _stream);
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <returns></returns>
        public TickAskBidVolumeData Read()
        {
            if (!IsEndOfStream)
            {
                return _reader.Read();
            }

            return null;
        }

        /// <summary>
        /// Connect next reader
        /// </summary>
        private void ConnectNextReader()
        {
            _fileCurrent = _fileCurrent.Next;

            try
            {
                _stream = new DucascopyFileSteam(Path.Combine(_configuration.DataPath, _fileCurrent.Value.DestinationFile));
            }
            catch (Exception exp)
            {
                Console.Write(exp.ToString());
                ConnectNextReader();
            }

            _reader = new DucascopyFileStreamReader(_symbol, _fileCurrent.Value.Time, _stream);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">Disposing</param>
        public void Dispose(bool disposing)
        {
            if (_stream != null)
            {
                _stream.Dispose();
                _stream = null;
            }

            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
    }
}
