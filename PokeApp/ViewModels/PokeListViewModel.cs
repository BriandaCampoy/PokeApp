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
    public partial class PokeListViewModel : ObservableObject
    {
        public ICommand SelectPokemonCommand { get; private set; }
        public ICommand LoadMorePokemonCommad { get; private set; }

        public IPokemonService pokemonService = DependencyService.Resolve<IPokemonService>();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private ObservableCollection<PokemonResult> _pokemonResults;


        [ObservableProperty]
        private PokemonResult _selectedPokemon;


        public PokeListViewModel()
        {
            SelectPokemonCommand = new Command(async () => await PokemonSelected());
            LoadMorePokemonCommad = new Command(async () => await LoadMorePokemon());

            _pokemonResults = new ObservableCollection<PokemonResult>();

            LoadPokemon();
        }

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
