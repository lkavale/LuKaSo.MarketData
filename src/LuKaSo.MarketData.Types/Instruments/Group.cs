using LuKaSo.MarketData.Infrastructure.Instruments;
using System;
using System.Collections.Generic;

namespace LuKaSo.MarketData.Types.Instruments
{
    public class Group : IGroup, IEquatable<Group>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Group()
        {
            Symbols = new List<ISymbol>();
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison groups</param>
        /// <returns>Is equal</returns>
        public bool Equals(Group other)
        {
            return Equals((IGroup)other);
        }

        /// <summary>
        /// Equality
        /// </summary>
        /// <param name="other">Comparison groups</param>
        /// <returns>Is equal</returns>
        public bool Equals(IGroup other)
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
        public virtual IList<ISymbol> Symbols { get; set; }

        /// <summary>
        /// Groups
        /// </summary>
        public virtual IList<IGroup> Groups { get; set; }

        /// <summary>
        /// Parrent
        /// </summary>
        public virtual IGroup Parent { get; set; }

        #endregion
    }
}
