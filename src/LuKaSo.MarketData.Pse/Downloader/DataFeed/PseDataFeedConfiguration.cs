using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using LuKaSo.MarketData.Infrastructure.Instruments;
using LuKaSo.MarketData.Pse.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Pse.Instruments;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Pse.Downloader.DataFeed
{
    public class PseDataFeedConfiguration : IDataFeedConfiguration<PseSymbol, PseGroup>
    {
        /// <summary>
        /// Symbols
        /// </summary>
        public IEnumerable<PseSymbol> Symbols { get; private set; }

        /// <summary>
        /// Groups
        /// </summary>
        public IEnumerable<PseGroup> Groups { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configurationReader"></param>
        public PseDataFeedConfiguration(IConfigurationReader<Configuration> configurationReader)
        {
            PrepareConfiguration(configurationReader.Read());
        }

        /// <summary>
        /// Extract symbols from configuration
        /// </summary>
        /// <param name="dataFeedConfiguration"></param>
        /// <returns></returns>
        protected IEnumerable<PseSymbol> ExtractSymbols(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Symbols
                .Select(s => new PseSymbol()
                {
                    Id = s.Key,
                    Title = s.Value.Title,
                    Name = s.Value.Name,
                    Isin = s.Value.Isin,
                    NotationId = s.Value.NotationId,
                    PipValue= s.Value.PipValue,
                    Description = s.Value.Description,
                    BaseCurrency = s.Value.BaseCurrency,
                    QuotaCurrency = s.Value.QuoteCurrency,
                });
        }

        /// <summary>
        /// Extract groups from configuration
        /// </summary>
        /// <param name="dataFeedConfiguration"></param>
        /// <returns></returns>
        protected IEnumerable<PseGroup> ExtractGroups(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Groups
                 .Select(g => new PseGroup()
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
