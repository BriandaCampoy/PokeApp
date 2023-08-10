using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PokeApp.Models;
using Newtonsoft.Json;

namespace PokeApp.Services
{
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

        public async Task<IEnumerable<PokemonResult>> getPokemon()
        {
            try
            {
                HttpClient client = await GetClient();
                var response = await client.GetAsync($"{url}/pokemon");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                PokemonList pokeList = JsonConvert.DeserializeObject<PokemonList>(content);
                next = pokeList.Next;
                return pokeList.Results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<PokemonResult>> getNextPokemon()
        {
            try
            {
                HttpClient client = await GetClient();
                var response = await client.GetAsync(next);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                PokemonList pokeList = JsonConvert.DeserializeObject<PokemonList>(content);
                next = pokeList.Next;
                return pokeList.Results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PokemonDetails> getPokemonByUrl(string pokemonUrl)
        {
            try
            {
                HttpClient client = await GetClient();
                var response = await client.GetAsync(pokemonUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PokemonDetails>(content);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        
    }

    public interface IPokemonService
    {
        Task<IEnumerable<PokemonResult>> getPokemon();

        Task<PokemonDetails> getPokemonByUrl(string pokemonUrl);

        Task<IEnumerable<PokemonResult>> getNextPokemon();
    }
}
