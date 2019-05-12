# Lukaso Marketdata
### About the project
Main goal of this project is provide tooling for:
* downloading financial market historical data (FOREX, stocks, futures, etc.) from different sources
* conversion between data formats CSV, Metatrader (HST and FXT) for ATS backtesting
* CLI API for downloading and converting data
* UI for downloading and converting data such as TickStory, StrategyQuant, etc.
### What is done
* Library for downloading Ducascopy data
* Library for downloading PSE (Prague stock exchange) data
* CLI for downloading data
### Usage
* Clone repository
* Build solution
* Publish LuKaSo.Market.Data.Cli as you want
* Default path for data download is C:\Data\Ducascopy
#### LuKaSo.Market.Data.Cli (Windows)
``` Lukaso.MarketData.Cli.exe download -s Ducascopy -i EURUSD -f 2010-01-01 -t 2018-01-01 ```
``` Lukaso.MarketData.Cli.exe download -s Pse -i BAACEZ -f 2010-01-01 -t 2018-01-01 ```
* download - command for downloading data
* -s Data source
* -i Instrument/symbol name (eg EURUSD)
* -f Start date and time of the data (YYYY-MM-DD)
* -t End date and time of the data (YYYY-MM-DD)

 
