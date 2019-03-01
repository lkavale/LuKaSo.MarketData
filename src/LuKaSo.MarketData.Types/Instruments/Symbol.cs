using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Types.Instruments
{
    /// <summary>
    /// Symbol
    /// </summary>
    public class Symbol : ISymbol, IEquatable<Symbol>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Symbol()
        {
            Groups = new List<IGroup>();
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison Symbol</param>
        /// <returns>Is equal</returns>
        public bool Equals(Symbol other)
        {
            return Equals((ISymbol)other);
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison Symbol</param>
        /// <returns>Is equal</returns>
        public bool Equals(ISymbol other)
        {
            return Name == other.Name;
        }

        #region Properties

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Directory name
        /// </summary>
        public string DirectoryName { get { return Name; } set { } }

        /// <summary>
        /// Digits
        /// </summary>
        public int Digits { get; set; }


        #endregion

        #region Navigation properties 

        /// <summary>
        /// Group
        /// </summary>
        public virtual IList<IGroup> Groups { get; set; }

        /// <summary>
        /// Data avalability
        /// </summary>
        public virtual DataAvalability DataAvalability { get; set; }

        #endregion
    }
}
