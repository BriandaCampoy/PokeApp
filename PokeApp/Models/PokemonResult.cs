using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.Models
{
    /// <summary>
    /// Represents a single Pokemon result obtained from an API.
    /// </summary>
    public class PokemonResult
    {

        /// <summary>
        /// Gets or sets the name of the Pokemon.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the URL to retrieve more details about the Pokemon.
        /// </summary>
        public string Url { get; set; }
    }
}
