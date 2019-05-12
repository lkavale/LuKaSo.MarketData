using LuKaSo.MarketData.Infrastructure.Downloader;
using System;
using System.IO;

namespace LuKaSo.MarketData.Ducascopy.Reader
{
    /// <summary>
    /// Ducascopy file stream
    /// </summary>
    public class DucascopyFileSteam : MemoryStream
    {
        /// <summary>
        /// File steam
        /// </summary>
        private readonly FileStream _stream;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="file"></param>
        public DucascopyFileSteam(string file)
        {
            _stream = new FileStream(file, FileMode.Open);

            var coder = new SevenZip.Compression.LZMA.Decoder();

            // Read data properties
            var properties = new byte[5];
            _stream.Read(properties, 0, 5);

            // Read in the decompress file size.
            var fileLengthBytes = new byte[8];
            _stream.Read(fileLengthBytes, 0, 8);
            var fileLength = BitConverter.ToInt64(fileLengthBytes, 0);

            coder.SetDecoderProperties(properties);
            coder.Code(_stream, this, _stream.Length, fileLength, null);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected override void Dispose(bool disposing)
        {
            if (_stream != null)
                _stream.Dispose();

            base.Dispose(disposing);
        }

        /// <summary>
        /// Close stream
        /// </summary>
        public override void Close()
        {
            if (_stream != null)
                _stream.Close();

            base.Close();
        }
    }
}
