using PokeApp.Pages;

namespace PokeApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("pokemonDetails", typeof(PokemonDetailsPage));
        }
    }
}