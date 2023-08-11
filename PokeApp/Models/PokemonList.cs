using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.Models
{
    /// <summary>
    /// Represents a list of Pokemon retrieved from an API.
    /// </summary>
    public class PokemonList
    {
        /// <summary>
        /// Gets or sets the total count of Pokemon in the list.
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets the URL to the next page of Pokemon results, if available.
        /// </summary>
        public string Next { get; set; }

        /// <summary>
        /// Gets or sets the URL to the previous page of Pokemon results, if available.
        /// </summary>
        public string Previous { get; set; }

        /// <summary>
        /// Gets or sets the collection of Pokemon results in the list.
        /// </summary>
        public List<PokemonResult> Results { get; set; }

    }
}
