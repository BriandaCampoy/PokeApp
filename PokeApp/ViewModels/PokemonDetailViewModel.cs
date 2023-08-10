using PokeApp.Models;
using PokeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokeApp.ViewModels
{
    class PokemonDetailViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IPokemonService pokemonService = DependencyService.Resolve<IPokemonService>();



        public PokemonDetailViewModel()
        {
        }

        private PokemonResult _pokemonResult;

        public PokemonResult PokemonResult
        {
            get => _pokemonResult;
            set
            {
                if (_pokemonResult == value)
                    return;

                _pokemonResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PokemonResult)));
                Task.Run(LoadPokemonDetails);
            }
        }

        private PokemonDetails _pokemonDetails { get; set; }

        public PokemonDetails pokemonDetails
        {
            get => _pokemonDetails;
            set 
            { 
                _pokemonDetails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(pokemonDetails)));
            } 
        }

        public async Task LoadPokemonDetails()
        {
            var resPokemonDetails = await pokemonService.getPokemonByUrl(PokemonResult.Url);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                pokemonDetails = resPokemonDetails;
                
            });

        }

    }
}
