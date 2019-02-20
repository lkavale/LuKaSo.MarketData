using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Types.Instruments
{
    /// <summary>
    /// Symbol
    /// </summary>
    public class Symbol : IEquatable<Symbol>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Symbol()
        {
            Groups = new List<Group>();
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison Symbol</param>
        /// <returns>Is equal</returns>
        public bool Equals(Symbol other)
        {
            if (other == null)
            {
                return false;
            }

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
        /// Digits
        /// </summary>
        public int Digits { get; set; }


        #endregion

        #region Navigation properties 

        /// <summary>
        /// Group
        /// </summary>
        public virtual IList<Group> Groups { get; set; }

        /// <summary>
        /// Data avalability
        /// </summary>
        public virtual DataAvalability DataAvalability { get; set; }

        #endregion
    }
}
