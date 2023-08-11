using PokeApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PokeApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace PokeApp.ViewModels
{
    /// <summary>
    /// ViewModel for displaying a list of Pokémon and handling user interactions.
    /// </summary>
    public partial class PokeListViewModel : ObservableObject
    {
        public ICommand SelectPokemonCommand { get; private set; }
        public ICommand LoadMorePokemonCommad { get; private set; }

        public IPokemonService pokemonService = DependencyService.Resolve<IPokemonService>();


        /// <summary>
        /// Gets or sets a value indicating whether the ViewModel is currently busy.
        /// </summary>
        [ObservableProperty]
        private bool _isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether the ViewModel is currently loading data.
        /// </summary>
        [ObservableProperty]
        private bool _isLoading;

        /// <summary>
        /// Gets or sets the collection of Pokémon results to display.
        /// </summary>
        [ObservableProperty]
        private ObservableCollection<PokemonResult> _pokemonResults;

        /// <summary>
        /// Gets or sets the currently selected Pokémon.
        /// </summary>
        [ObservableProperty]
        private PokemonResult _selectedPokemon;

        /// <summary>
        /// Initializes a new instance of the PokeListViewModel class.
        /// </summary>
        public PokeListViewModel()
        {
            SelectPokemonCommand = new Command(async () => await PokemonSelected());
            LoadMorePokemonCommad = new Command(async () => await LoadMorePokemon());

            _pokemonResults = new ObservableCollection<PokemonResult>();

            LoadPokemon();
        }

        /// <summary>
        /// Loads a collection of Pokémon data from the PokemonService and populates the PokemonResults collection.
        /// </summary>
        private async Task PokemonSelected()
        {
            if (SelectedPokemon == null)
                return;

            var navigationParameter = new Dictionary<string, object>()
            {
                {"pokemonResult", SelectedPokemon }
            };

            await Shell.Current.GoToAsync("pokemonDetails", navigationParameter);

            MainThread.BeginInvokeOnMainThread(() => SelectedPokemon = null);
        }


        /// <summary>
        /// Loads more Pokémon data when the "Load More" action is triggered.
        /// </summary>
        private void LoadPokemon()
        {
           PokemonResults.Clear();
            IsBusy = true;

            Task.Run(async () =>
            {
                var pokemonCollection = await pokemonService.getPokemon();
                App.Current.Dispatcher.Dispatch(() =>
                {
                    foreach(var pokemon in pokemonCollection)
                    {
                        PokemonResults.Add(pokemon);
                    }
                    IsBusy = false;
                });

            });
        }

        public async Task LoadMorePokemon()
        {
            if(IsLoading) return;

            if(PokemonResults?.Count> 0)
            {
                IsLoading = true;
                var morePokemonCollection = await pokemonService.getNextPokemon();
                foreach (PokemonResult pokeRes in morePokemonCollection)
                {
                    PokemonResults.Add(pokeRes);
                }
                IsLoading = false;
            }
        }

    }
}
