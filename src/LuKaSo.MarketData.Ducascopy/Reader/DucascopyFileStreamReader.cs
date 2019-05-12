using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Data;
using LuKaSo.MarketData.Infrastructure.Reader;
using System;
using System.IO;

namespace LuKaSo.MarketData.Ducascopy.Reader
{
    /// <summary>
    /// Ducascopy stream reader
    /// </summary>
    public class DucascopyFileStreamReader : ITickDataReader<TickAskBidVolumeData>
    {
        /// <summary>
        /// Stream
        /// </summary>
        private readonly Stream _stream;

        /// <summary>
        /// Symbol
        /// </summary>
        private readonly DucascopySymbol _symbol;

        /// <summary>
        /// File date time
        /// </summary>
        private readonly DateTime _fileDateTime;

        /// <summary>
        /// Can report progress
        /// </summary>
        /// <returns></returns>
        public bool CanProgressReport
        {
            get { return true; }
        }

        /// <summary>
        /// Is end of stream
        /// </summary>
        /// <returns>Is end</returns>
        public bool IsEndOfStream
        {
            get { return _stream.Position + 20 > _stream.Length; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileDateTime"></param>
        /// <param name="symbol"></param>
        public DucascopyFileStreamReader(DucascopySymbol symbol, DateTime fileDateTime, Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException("stream", "Stream must be specified and initialized.");
            }

            if (stream.CanSeek)
            {
                stream.Seek(0, SeekOrigin.Begin);
            }

            _stream = stream;
            _symbol = symbol;
            _fileDateTime = fileDateTime;
        }

        /// <summary>
        /// Read data
        /// </summary>
        /// <returns></returns>
        public TickAskBidVolumeData Read()
        {
            return new TickAskBidVolumeData()
            {
                Time = ReadDateTime(),
                Ask = (decimal)ReadPrice(),
                Bid = (decimal)ReadPrice(),
                AskVolume = (decimal)ReadSingle(),
                BidVolume = (decimal)ReadSingle()
            };
        }

        /// <summary>
        /// Read byte array from stream
        /// </summary>
        /// <param name="numBytes">Number of bytes to read</param>
        /// <returns>Byte array</returns>
        protected byte[] ReadBytes(int numBytes)
        {
            var bytes = new byte[numBytes];

            if (numBytes < 0)
            {
                throw new ArgumentException("There must be some bytes for reading.", "numBytes");
            }

            int bytesRead = 0;
            int n = 0;

            if (numBytes == 1)
            {
                n = _stream.ReadByte();
                if (n == -1)
                {
                    throw new EndOfStreamException("Stream is already readed to end.");
                }

                bytes[0] = (byte)n;
                return bytes;
            }

            do
            {
                n = _stream.Read(bytes, bytesRead, numBytes - bytesRead);
                if (n == 0)
                {
                    throw new EndOfStreamException("Stream is already readed to end.");
                }

                bytesRead += n;
            } while (bytesRead < numBytes);

            Array.Reverse(bytes);
            return bytes;
        }

        /// <summary>
        /// Read date time
        /// </summary>
        /// <returns></returns>
        public DateTime ReadDateTime()
        {
            return DateTime.SpecifyKind(_fileDateTime, DateTimeKind.Unspecified).AddMilliseconds(ReadInt32());
        }

        /// <summary>
        /// Read price
        /// </summary>
        /// <returns></returns>
        public double ReadPrice()
        {
            return ReadInt32() / Math.Pow(10, _symbol.Digits);
        }

        /// <summary>
        /// Read 4B Int32
        /// </summary>
        /// <returns></returns>
        private int ReadInt32()
        {
            return BitConverter.ToInt32(ReadBytes(4), 0);
        }

        /// <summary>
        /// Read 4B single
        /// </summary>
        /// <returns></returns>
        private float ReadSingle()
        {
            return BitConverter.ToSingle(ReadBytes(4), 0);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Disposing
        /// </summary>
        /// <param name="disposing">Disposing</param>
        protected virtual void Dispose(bool disposing)
        {
            if (_stream != null)
            {
                _stream.Dispose();
            }
        }
    }
}
