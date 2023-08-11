using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeApp.Models;
using Newtonsoft.Json;

namespace PokeApp.Services
{
    /// <summary>
    /// Service responsible for fetching Pokémon data from a remote API.
    /// </summary>
    public class PokemonService:IPokemonService
    {
        static HttpClient client;
        static string url = Environment.GetEnvironmentVariable("URL_BASE");

        private string next;

        private static async Task<HttpClient> GetClient()
        {
            if (client != null)
                return client;

            client = new HttpClient();
            return client;
        }

        /// <summary>
        /// Retrieves a collection of Pokémon from the remote API.
        /// </summary>
        /// <returns>An IEnumerable of PokemonResult containing the fetched Pokémon data.</returns>
        public async Task<IEnumerable<PokemonResult>> getPokemon()
        {
            try
            {
                var content = await ApiRequest($"{url}/pokemon");
                PokemonList pokeList = JsonConvert.DeserializeObject<PokemonList>(content);
                next = pokeList.Next;
                return pokeList.Results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Retrieves the next batch of Pokémon data from the remote API based on the 'next' URL.
        /// </summary>
        /// <returns>An IEnumerable of PokemonResult containing the fetched Pokémon data.</returns>
        public async Task<IEnumerable<PokemonResult>> getNextPokemon()
        {
            try
            {
                var content = await ApiRequest(next);
                PokemonList pokeList = JsonConvert.DeserializeObject<PokemonList>(content);
                next = pokeList.Next;
                return pokeList.Results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Performs an asynchronous HTTP GET request to the specified URL and returns the response content as a string.
        /// </summary>
        /// <param name="urlRequest">The URL to send the GET request to.</param>
        /// <returns>A string containing the response content from the specified URL.</returns>
        private async Task<string> ApiRequest(string urlRequest)
        {
            HttpClient client = await GetClient();
            var response = await client.GetAsync(urlRequest);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        /// <summary>
        /// Retrieves detailed information about a specific Pokémon using its URL.
        /// </summary>
        /// <param name="pokemonUrl">The URL of the Pokémon to retrieve.</param>
        /// <returns>A PokemonDetails object containing the detailed Pokémon information.</returns>
        public async Task<PokemonDetails> getPokemonByUrl(string pokemonUrl)
        {
            try
            {
                var content = await ApiRequest(pokemonUrl);
                return JsonConvert.DeserializeObject<PokemonDetails>(content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
    }

    /// <summary>
    /// Interface defining methods to retrieve Pokémon data from a remote API.
    /// </summary>
    public interface IPokemonService
    {
        Task<IEnumerable<PokemonResult>> getPokemon();

        Task<PokemonDetails> getPokemonByUrl(string pokemonUrl);

        Task<IEnumerable<PokemonResult>> getNextPokemon();
    }
}
