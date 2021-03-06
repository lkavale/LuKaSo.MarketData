﻿using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed;
using LuKaSo.MarketData.Ducascopy.Downloader.DataFeed.Models;
using LuKaSo.MarketData.Ducascopy.Infrastructure;
using LuKaSo.MarketData.Infrastructure.Downloader.DataFeed;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.MarketData.Ducascopy.Tests
{
    [TestClass]
    public class DucascopyDataFeedConfigurationTests
    {
        private DucacopyDataFeedConfiguration _configuration;
        private Configuration _dataFeedConfiguration;

        [TestInitialize]
        public void Init()
        {
            _dataFeedConfiguration = new Configuration()
            {
                Groups = new Dictionary<string, Group>()
                {
                    { "G1", new Group() { Id = "G1", Instruments = new List<string>{ }, Title = "G1" } },
                    { "G2", new Group() { Id = "G2", Instruments = new List<string>{ "I1", "I2"}, Parent = "G1", Title = "G2" } },
                    { "G3", new Group() { Id = "G3", Instruments = new List<string>{ "I3", "I4"}, Parent = "G1", Title = "G3" } },
                    { "G4", new Group() { Id = "G4", Instruments = new List<string>{ "I3"}, Parent = "G2", Title = "G4" } }
                },
                Symbols = new Dictionary<string, Symbol>()
                {
                    { "I1", new Symbol(){ Name = "I1", Title = "I1", BaseCurrency = "I", QuoteCurrency = "1", DataFeedName = "I1", Description = "I1", PipValue = 0.1 } },
                    { "I2", new Symbol(){ Name = "I2", Title = "I2", BaseCurrency = "I", QuoteCurrency = "2", DataFeedName = "I2", Description = "I2", PipValue = 0.01 } },
                    { "I3", new Symbol(){ Name = "I3", Title = "I3", BaseCurrency = "I", QuoteCurrency = "3", DataFeedName = "I3", Description = "I3", PipValue = 0.001 } },
                    { "I4", new Symbol(){ Name = "I4", Title = "I4", BaseCurrency = "I", QuoteCurrency = "4", DataFeedName = "I4", Description = "I4", PipValue = 0.0001 } }
                }
            };

            var configReader = new Mock<IConfigurationReader<Configuration>>();

            configReader
                .Setup(mk => mk.Read())
                .Returns(_dataFeedConfiguration);

            _configuration = new DucacopyDataFeedConfiguration(configReader.Object);
        }

        [TestMethod]
        public void Symbols()
        {
            Assert.AreEqual(_dataFeedConfiguration.Symbols.Count(), _configuration.Symbols.Count());

            var extractedSymbols = _configuration.Symbols
                .Select(s => s.Name)
                .Distinct()
                .ToList();

            var symbols = _dataFeedConfiguration.Symbols
                .Keys
                .ToList();

            Assert.IsTrue(Enumerable.SequenceEqual(extractedSymbols, symbols));
        }

        [TestMethod]
        public void Groups()
        {
            Assert.AreEqual(_dataFeedConfiguration.Groups.Count(), _configuration.Groups.Count());

            var extractedGroups = _configuration.Groups
                .Select(s => s.Name)
                .Distinct()
                .ToList();

            var groups = _dataFeedConfiguration.Groups
                .Keys
                .ToList();

            Assert.IsTrue(Enumerable.SequenceEqual(extractedGroups, groups));
        }
    }
}
