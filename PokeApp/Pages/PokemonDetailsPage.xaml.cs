using PokeApp.Models;
using PokeApp.ViewModels;

namespace PokeApp.Pages;

[QueryProperty("PokemonResult", "pokemonResult")]
public partial class PokemonDetailsPage : ContentPage
{
	PokemonDetailViewModel pokemonDetailsViewModel;
    public PokemonDetailsPage()
	{
		InitializeComponent();
		pokemonDetailsViewModel = new PokemonDetailViewModel();
		BindingContext = pokemonDetailsViewModel;
	}

	PokemonResult _pokemonResult {  get; set; }

	public PokemonResult PokemonResult
	{
		get => _pokemonResult;
		set
		{
			if (_pokemonResult== value)
				return;

			_pokemonResult = value;
			pokemonDetailsViewModel.PokemonResult = value;
		}
	}
}