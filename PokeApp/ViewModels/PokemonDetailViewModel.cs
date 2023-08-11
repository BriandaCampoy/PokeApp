using CommunityToolkit.Mvvm.ComponentModel;
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
    /// <summary>
    /// View model for displaying detailed information about a Pokémon.
    /// </summary>
    public partial class PokemonDetailViewModel : ObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public IPokemonService pokemonService = DependencyService.Resolve<IPokemonService>();

        /// <summary>
        /// Gets or sets a value indicating whether the view model is currently loading data.
        /// </summary>
        [ObservableProperty]
        private bool _isLoading = false;


        public PokemonDetailViewModel()
        {
        }


        private PokemonResult _pokemonResult;

        /// <summary>
        /// Gets or sets the selected Pokémon result to display details for.
        /// </summary>
        public PokemonResult PokemonResult
        {
            get => _pokemonResult;
            set
            {
                if (_pokemonResult == value)
                    return;

                _pokemonResult = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PokemonResult)));
                LoadPokemonDetails();
            }
        }
        

        [ObservableProperty]
        private PokemonDetails _pokemonDetail;


        /// <summary>
        /// Loads the detailed information for the selected Pokémon.
        /// </summary>
        private void LoadPokemonDetails()
        {
            IsLoading = true;

            Task.Run(async () =>
            {
                var resPokemonDetails = await pokemonService.getPokemonByUrl(PokemonResult.Url);
                App.Current.Dispatcher.Dispatch(() =>
                {
                    PokemonDetail = resPokemonDetails;
                    IsLoading = false;
                });
            });
        }

    }
}
