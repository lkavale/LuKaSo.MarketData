using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using LuKaSo.MarketData.Infrastructure.Instruments;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed
{
    public class DucacopyDataFeedConfiguration : IDataFeedConfiguration<DucascopySymbol, DucascopyGroup>
    {
        /// <summary>
        /// Symbols
        /// </summary>
        public IEnumerable<DucascopySymbol> Symbols { get; private set; }

        /// <summary>
        /// Groups
        /// </summary>
        public IEnumerable<DucascopyGroup> Groups { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurationReader"></param>
        public DucacopyDataFeedConfiguration(IConfigurationReader<Configuration> configurationReader)
        {
            PrepareConfiguration(configurationReader.Read());
        }

        /// <summary>
        /// Extract symbols from configuration
        /// </summary>
        /// <param name="dataFeedConfiguration"></param>
        /// <returns></returns>
        protected IEnumerable<DucascopySymbol> ExtractSymbols(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Symbols
                .Select(s => new DucascopySymbol()
                {
                    Id = s.Key,
                    Name = s.Value.DataFeedName,
                    Description = s.Value.Description,
                    DirectoryName = s.Value.DataFeedName,
                    BaseCurrency = s.Value.BaseCurrency,
                    QuotaCurrency = s.Value.QuoteCurrency,
                    Digits = s.Value.GetDigits(),
                    StartDate = s.Value.GetStartDateByResolution(DataResolution.TickData)
                });
        }

        /// <summary>
        /// Extract groups from configuration
        /// </summary>
        /// <param name="dataFeedConfiguration"></param>
        /// <returns></returns>
        protected IEnumerable<DucascopyGroup> ExtractGroups(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Groups
                 .Select(g => new DucascopyGroup()
                 {
                     Id = g.Key,
                     Name = g.Value.Id,
                     Description = g.Value.Title
                 });
        }

        /// <summary>
        /// Prepare data feed configuration
        /// </summary>
        /// <param name="dataFeedConfiguration"></param>
        protected void PrepareConfiguration(Configuration dataFeedConfiguration)
        {
            Symbols = ExtractSymbols(dataFeedConfiguration).ToList();
            Groups = ExtractGroups(dataFeedConfiguration).ToList();

            dataFeedConfiguration.Groups
                .ForEach(g =>
                {
                    var group = Groups.SingleOrDefault(x => x.Id == g.Key);

                    if (!string.IsNullOrEmpty(g.Value.Parent))
                    {
                        var groupParent = Groups.SingleOrDefault(x => x.Id == g.Value.Parent);

                        group.Parent = groupParent;
                        groupParent.Groups.Add(group);
                    }

                    if (g.Value.Instruments.Any())
                    {
                        group.Symbols = Symbols
                            .Join(g.Value.Instruments,
                                l => l.Id,
                                r => r,
                                (l, r) => l)
                            .Select(s =>
                            {
                                s.Groups.Add(group);
                                return (ISymbol)s;
                            })
                            .ToList();
                    }
                });
        }
    }
}
