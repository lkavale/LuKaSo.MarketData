using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Types.Instruments
{
    public class Group : IEquatable<Group>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Group()
        {
            Symbols = new List<Symbol>();
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison groups</param>
        /// <returns>Is equal</returns>
        public bool Equals(Group other)
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

        #endregion

        #region Navigation properties 

        /// <summary>
        /// Symbols
        /// </summary>
        public virtual IList<Symbol> Symbols { get; set; }

        #endregion
    }
}
