using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Ducascopy.Instruments;
using LuKaSo.MarketData.Infrastructure.Instruments;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.Downloader.DataFeed
{
    public class DucacopyDataFeedConfiguration : IDataFeedConfiguration
    {
        /// <summary>
        /// Symbols
        /// </summary>
        private IEnumerable<DucascopySymbol> _symbols;

        /// <summary>
        /// Groups
        /// </summary>
        private IEnumerable<DucascopyGroup> _groups;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configurationReader"></param>
        public DucacopyDataFeedConfiguration(IConfigurationReader configurationReader)
        {
            PrepareConfiguration(configurationReader.Read());
        }

        /// <summary>
        /// Gets all symbols
        /// </summary>
        public IEnumerable<ISymbol> Symbols
        {
            get
            {
                return _symbols;
            }
        }

        /// <summary>
        /// Gets all groups
        /// </summary>
        public IEnumerable<IGroup> Groups
        {
            get
            {
                return _groups;
            }
        }

        /// <summary>
        /// Gets top level groups
        /// </summary>
        public IEnumerable<IGroup> TopLevelGroups
        {
            get
            {
                return _groups.Where(g => g.Parent == null);
            }
        }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns></returns>
        public bool IsSymbolExists(ISymbol symbol)
        {
            return _symbols.Contains(symbol);
        }

        /// <summary>
        /// Contains symbol
        /// </summary>
        /// <param name="symbolName"></param>
        /// <returns></returns>
        public bool IsSymbolExists(string symbolName)
        {
            return _symbols.Any(s => s.Id == symbolName);
        }

        private IList<DucascopySymbol> ExtractSymbols(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Symbols
                .Select(s => new DucascopySymbol()
                {
                    Id = s.Key,
                    Name = s.Value.Name,
                    Description = s.Value.Description,
                    DirectoryName = s.Value.DataFeedName,
                    BaseCurrency = s.Value.BaseCurrency,
                    QuotaCurrency = s.Value.QuoteCurrency,
                    Digits = s.Value.GetDigits(),
                    StartDate = s.Value.GetStartDateByResolution(DataResolution.TickData)
                })
                .ToList();
        }

        private IList<DucascopyGroup> ExtractGroups(Configuration dataFeedConfiguration)
        {
            return dataFeedConfiguration.Groups
                 .Select(g => new DucascopyGroup()
                 {
                     Id = g.Key,
                     Name = g.Value.Id,
                     Description = g.Value.Title
                 })
                 .ToList();
        }

        private void PrepareConfiguration(Configuration dataFeedConfiguration)
        {
            _symbols = ExtractSymbols(dataFeedConfiguration);
            _groups = ExtractGroups(dataFeedConfiguration);

            dataFeedConfiguration.Groups
                .ForEach(g =>
                {
                    var group = _groups.SingleOrDefault(x => x.Id == g.Key);

                    if (!string.IsNullOrEmpty(g.Value.Parent))
                    {
                        var groupParent = _groups.SingleOrDefault(x => x.Id == g.Value.Parent);

                        group.Parent = groupParent;
                        groupParent.Groups.Add(group);
                    }

                    if (g.Value.Instruments.Any())
                    {
                        group.Symbols = _symbols
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
